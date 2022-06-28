using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Desktop.Ui.Dialogs;
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

            await Task.Factory.StartNew(async () =>
            {
                Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

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
                        _abpApplication.Services.GetRequiredService<LoginView>()?.Show();

                    splashScreen.Close();
                });
            }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        private void OnInitialized()
        {
            if (!Authorization())
            {
                Environment.Exit(0);
            }
        }

        private static bool Authorization()
        {
            var validationResult = Validation();
            if (validationResult == ButtonResult.Retry)
                return Authorization();

            return validationResult == ButtonResult.OK;

            static ButtonResult Validation()
            {
                //  var dialogService = ContainerLocator.Container.Resolve<IHostDialogService>();
                // return dialogService.ShowWindow(AppViewManager.Login).Result;

                return ButtonResult.OK;
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
    }
}