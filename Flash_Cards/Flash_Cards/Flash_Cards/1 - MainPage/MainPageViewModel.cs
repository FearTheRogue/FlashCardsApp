using System.Collections.Generic;
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
        private ObservableCollection<CustomCell> _cards;
        private string _titleString = "Nothing Selected";
        private CustomCell _selectedString;
        private int _selectedRow = 0;
     

        public ObservableCollection<CustomCell> Cards
        {
            get => _cards;
            set
            {
                if (_cards == value) return;
                _cards = value;
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

        public CustomCell SelectedString 
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

        public async Task ItemSelectionChangedAsync (int row, CustomCell card)
        {
            //SelectedRow = row;
            //await _viewHelper.TextPopup("Selected Card: ", $"{card.Catagory} on row {row}");
            
            var nextPage = new ThirdPage();
            await Navigation.PushAsync(nextPage);
            nextPage.Title = card.Catagory + " " + $"on row {row}";
        }

        public void AddNewCard(CustomCell newCardString)
        {
            Cards.Add(newCardString);
        }

        public async Task AddCardButtonAsync()
        {
            //Cards.Add("New");

            var addPage = new AddCardPage();
            await Navigation.PushAsync(addPage);

            //await _viewHelper.TextPopup("Nice", "Button Clicked");
        }

        public ICommand DeleteCommand { get; private set; }

        public void DeleteItem(CustomCell c) => Cards.Remove(c);

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            Cards = new ObservableCollection<CustomCell>()
            {
                new CustomCell("SOFT262", 4),
                new CustomCell("AINT255", 4),
                new CustomCell("Dinosaurs", 4),
                new CustomCell("Food ", 4),
                new CustomCell("Netflix", 4),
                new CustomCell("Sport", 4),
                new CustomCell("Oop", 4)

            };

            DeleteCommand = new Command<CustomCell>(execute: (c) => DeleteItem(c));
        }
    }
}
