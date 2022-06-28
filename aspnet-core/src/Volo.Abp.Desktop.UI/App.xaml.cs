using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Desktop.UI.Dialogs;
using Volo.Abp.Desktop.UI.Services;
using Volo.Abp.Desktop.UI.Views;

namespace Volo.Abp.Desktop.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private IAbpApplicationWithInternalServiceProvider _abpApplication;

        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// App Start
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var splashScreen = new SplashWindow();
            this.MainWindow = splashScreen;
            splashScreen.Show();

            await Task.Factory.StartNew(() => OnInitializedAync(splashScreen), CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        /// <summary>
        ///
        /// </summary>
        private async Task OnInitializedAync(SplashWindow splashScreen)
        {
            ConfigurSerilog();

            var shouldContinue = true;
            try
            {
                Log.Information("Starting WPF host.");

                _abpApplication = await AbpApplicationFactory.CreateAsync<DesktopWpfModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                });

                await _abpApplication.InitializeAsync();
            }
            catch (Exception ex)
            {
                shouldContinue = false;
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }

            this.Dispatcher.Invoke(() =>
            {
                if (shouldContinue)
                {
                    if (!Authorization())
                    {
                        Environment.Exit(0);
                    }
                    _abpApplication.Services.GetRequiredService<MainWindow>()?.Show();
                }

                splashScreen.Close();
            });
        }

        /// <summary>
        /// Initializ Configur Serilog
        /// </summary>
        private void ConfigurSerilog()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
              .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.Async(c =>
              {
                  static string LogFilePath(string LogEvent) => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", LogEvent, "log.log");
                  string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 50);

                  c.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                        .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                        .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                        .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
                        .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate));
              })
              .CreateLogger();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private bool Authorization()
        {
            var validationResult = Validation();
            if (validationResult == ButtonResult.Retry)
                return Authorization();

            return validationResult == ButtonResult.OK;

            ButtonResult Validation()
            {
                var dialogService = _abpApplication.Services.GetRequiredService<IHostDialogService>();
                return dialogService.ShowWindow(typeof(LoginView)).Result;
            }
        }

        /// <summary>
        /// App Exit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            _abpApplication.Shutdown();
            Log.CloseAndFlush();
        }

        #region Exception Handler

        /// <summary>
        /// Non-UI thread exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                //ignore
            }
        }

        /// <summary>
        /// Exception in Task thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(() => HandleException(e.Exception));
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.SetObserved();
            }
        }

        /// <summary>
        /// Exception in UI thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                HandleException(e.Exception);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Exception Handle
        /// </summary>
        /// <param name="e"></param>
        private static void HandleException(Exception e)
        {
            MessageBox.Show(e.Message);
            Log.Error(e, "Unhandled exception!");
        }

        #endregion Exception Handler
    }
}