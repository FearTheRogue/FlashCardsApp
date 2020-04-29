using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using MVVMBase;

namespace Flash_Cards
{
    public class MainPageViewModel : ViewModelBase
    {
        private IMainPageHelper _viewHelper;
        private ObservableCollection<CatagoryCell> _catagory;
        private string _titleString = "Nothing Selected";
        private CatagoryCell _selectedString;
        private int _selectedRow = 0;
     
        public ObservableCollection<CatagoryCell> CatagoryCards
        {
            get => _catagory;
            set
            {
                if (_catagory == value) return;
                _catagory = value;
                OnPropertyChanged();
            }
        }

        public string TitleString
        {
            get => _titleString ?? "Nothing Selected";
            set
            {
                if (_titleString == value) return;
                _titleString = value;
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

                TitleString = _selectedString.Catagory ?? "Nothing Selected";
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
        }

        public void AddNewCard(CatagoryCell newCardString)
        {
            CatagoryCards.Add(newCardString);
            _viewHelper.ScrollToObject(newCardString);
        }

        public async Task AddCardButtonAsync()
        {
            var addPage = new AddCardPage();
            await Navigation.PushAsync(addPage);
        }
        
        public ICommand DeleteCommand { get; private set; }

        public void DeleteItem(CatagoryCell c) => CatagoryCards.Remove(c);  

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            CatagoryCards = new ObservableCollection<CatagoryCell>()
            {
                new CatagoryCell("SOFT262", 4),
                new CatagoryCell("AINT255", 4),
                new CatagoryCell("Dinosaurs", 4),
                new CatagoryCell("Food ", 4),
                new CatagoryCell("Netflix", 4),
                new CatagoryCell("Sport", 4),
                new CatagoryCell("Oop", 4)

            };

            DeleteCommand = new Command<CatagoryCell>(execute: (c) => DeleteItem(c));
        }
    }
}
