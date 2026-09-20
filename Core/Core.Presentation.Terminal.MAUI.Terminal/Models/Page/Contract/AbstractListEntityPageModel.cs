namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Contract
{
    public abstract class AbstractListEntityPageModel<T> : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
    {
        private bool hasMoreItems;
        private string searchParameter;

        public System.Collections.ObjectModel.ObservableCollection<T> Items { get; set; } = [];

        public bool HasMoreItems { get => hasMoreItems; set => SetProperty(ref hasMoreItems, value); }

        public string SearchParameter { get => searchParameter; set => SetProperty(ref searchParameter, value); }
    }
}
