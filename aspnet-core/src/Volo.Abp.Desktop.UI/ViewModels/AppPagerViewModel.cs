using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.Ui.Dialogs;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public abstract class AppPagerViewModel : AppViewModel
    {
        protected AppPagerViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
            AddCommand = new RelayCommand(Add);
            pageSize = 10;
            gridModelList = new ObservableCollection<object>();
        }

        protected AppPagerViewModel()
        {
        }

        /// <summary>
        /// 页面索引改变事件
        /// </summary>
        public IRelayCommand PageChangedCommand { get; protected set; }

        /// <summary>
        /// 页面数据显示大小改变事件
        /// </summary>
        public IRelayCommand PageSizeChangedCommand { get; protected set; }

        /// <summary>
        /// 添加
        /// </summary>
        public IRelayCommand AddCommand { get; private set; }


        /// <summary>
        /// 当前页索引
        /// </summary>
        private int pageIndex;
        public int PageIndex
        {
            get => pageIndex;
            set => SetProperty(ref pageIndex, value);
        }

        /// <summary>
        /// 页面大小
        /// </summary>
        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set => SetProperty(ref pageSize, value);
        }

        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount;
        public int PageCount
        {
            get => pageCount;
            set => SetProperty(ref pageCount, value);
        }

        /// <summary>
        /// 选中项
        /// </summary>
        private object selectedItem;
        public object SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        /// <summary>
        /// 分页按钮数量
        /// </summary>
        private int numericButtonCount;
        public int NumericButtonCount
        {
            get => numericButtonCount;
            set => SetProperty(ref numericButtonCount, value);
        }

        /// <summary>
        /// 数据源
        /// </summary>
        private ObservableCollection<object> gridModelList;
        public ObservableCollection<object> GridModelList
        {
            get => gridModelList;
            set => SetProperty(ref gridModelList, value);
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public void SetPagerList<T>(IPagedResult<T> pagedResult)
        {
            GridModelList.Clear();

            foreach (var item in ObjectMapper.Map<IReadOnlyList<T>, List<T>>(pagedResult.Items))
                GridModelList.Add(item);

            if (pagedResult.TotalCount == 0)
                PageCount = 1;
            else
                PageCount = (int)Math.Ceiling(pagedResult.TotalCount / (double)PageSize);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public async void Add()
        {
            //var dialogResult = await dialog.ShowDialogAsync(GetPageName("Details"));
            //if (dialogResult.Result == ButtonResult.OK)
            //    await RefreshAsync();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public async void Edit()
        {
            //DialogParameters param = new DialogParameters();
            //param.Add("Value", dataPager.SelectedItem);

            //var dialogResult = await dialog.ShowDialogAsync(GetPageName("Details"), param);
            //if (dialogResult.Result == ButtonResult.OK)
            //    await RefreshAsync();
        }


        /// <summary>
        /// 获取弹出页名称
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private string GetPageName(string methodName) => this.GetType().Name.Replace("ViewModel", $"{methodName}View");
    }

}
