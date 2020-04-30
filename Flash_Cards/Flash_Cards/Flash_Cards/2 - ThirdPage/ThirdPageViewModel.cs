using System.Collections.ObjectModel;
using MVVMBase;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private ObservableCollection<CatagoryCell> _cards;
        private readonly IMainPageHelper _viewHelper;

        //public ObservableCollection<CatagoryCell> CatagoryCards
        //{
        //    get => _cards;
        //    set
        //    {
        //        if (_cards == value) return;
        //        _cards = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            //CatagoryCards = new ObservableCollection<CatagoryCell>()
            //{
            //    new CatagoryCell("SOFT262", "What is the name of a thing that walks","Humans")
            //};
        }
    }
}
