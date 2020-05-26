using MVVMBase;
using MyAzureLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Flash_Cards
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMainPageHelper _viewHelper;
        private CatagoryCell _selectedString;
        private int _selectedRow = 0;

        public AzureLibrary azure;
        public List<CardCatagories> _list = new List<CardCatagories>();
        public ObservableCollection<CatagoryCell> _appCategories = new ObservableCollection<CatagoryCell>();

        public List<CardCatagories> Categories
        {
            get => _list;
            set
            {
                if(_list == value) { return; }
                _list = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CatagoryCell> AppCategories
        {
            get => _appCategories;
            set
            {
                if(_appCategories == value) { return; }
                _appCategories = value;
                OnPropertyChanged();
            }
        }

        public CatagoryCell SelectedString 
        {
            get => _selectedString;
            set
            {
                if (_selectedString == value) return;
                _selectedString = value;
            }
        }

        public int SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (_selectedRow == value) return;
                _selectedRow = value;
                OnPropertyChanged();
            }
        }

        public async Task ItemSelectionChangedAsync (CatagoryCell card)
        {
            var nextPage = new ThirdPage();
            await Navigation.PushAsync(nextPage);
            nextPage.Title = card.Catagory + " Catagory Page";
 
            MessagingCenter.Send(this, "new", card);
        }

        public async Task AddCardButtonAsync()
        {
            var addPage = new AddCardPage();
            await Navigation.PushAsync(addPage);
        }
        
        public ICommand DeleteCommand { get; private set; }

        public Command RefreshCommand { get; private set; }

        public async Task GetCategoriesFromDB()
        {
            await azure.QueryItemsAsync(Categories);
        }

        public void InputDataToCollection()
        {
            foreach (CardCatagories catagories in Categories)
            {
                AppCategories.Add(new CatagoryCell(catagories.Id, catagories.Catagory, catagories.CardCount, catagories.Questions, catagories.Answers));

                SingletonModel.SingletonInstance.Categories = AppCategories;
            }
        }

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            azure = SingletonModel.SingletonInstance.Library;

            Task.Run(async () =>
            {
                await GetCategoriesFromDB();
            }).Wait();

            InputDataToCollection();

            DeleteCommand = new Command<CatagoryCell>(execute: async (c) => await azure.DeleteCategoryItemAsync(c.Catagory, c.Id));
        }
    }
}
