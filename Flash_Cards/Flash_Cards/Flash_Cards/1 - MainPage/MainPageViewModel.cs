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
        public ObservableCollection<CatagoryCell> _catagory;
        private string _titleString = "Nothing Selected";
        private CatagoryCell _selectedString;
        private int _selectedRow = 0;

        public AzureLibrary AzureLibrary { get; }

        public ObservableCollection<CatagoryCell> CatagoryCards
        {
            get => _catagory;
            set
            {
                if (_catagory == value)  { return;  }
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

        public void DeleteItem(CatagoryCell c)  { CatagoryCards.Remove(c); }

        public async Task Temp()
        {
            List<MyAzureLib.CardCatagories> list = new List<MyAzureLib.CardCatagories>();

            await AzureLibrary.QueryItemsAsync();

        }

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            AzureLibrary = new AzureLibrary();


            Temp();
            /*
            CatagoryCell catagoryCell = new CatagoryCell()
            {
                Catagory = "SOFT262",
                CardCount = 2,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "SOFT262 question 1"},
                    new Question{CardQuestion = "SOFT262 question 2"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnswer = "SOFT262 answer 1"},
                    new Answer{CardAnswer = "SOFT262 answer 2"},
                },
            };
            */

            //CatagoryCards = new ObservableCollection<CatagoryCell>()
            //{ 
            //    /*new CatagoryCell("SOFT262", 4),
            //    new CatagoryCell("AINT255", 4),
            //    new CatagoryCell("Dinosaurs", 4),
            //    new CatagoryCell("Food ", 4),
            //    new CatagoryCell("Netflix", 4),
            //    new CatagoryCell("Sport", 4),
            //    new CatagoryCell("Oop", 4)
            //    */
            //    //new CatagoryCell("SOFT262", "question 1","answer 1"),
            //    //new CatagoryCell("AINT255", "question 2", "answer 2"),
            //    //new CatagoryCell("Dinosaurs", "question 3", "answer 3"),
            //    //new CatagoryCell("Food", "question 4", "answer 4"),
            //    new CatagoryCell("Netflix", "question 5", "answer 5"),
            //    new CatagoryCell("",0)
            //};

            DeleteCommand = new Command<CatagoryCell>(execute: (c) => DeleteItem(c));
        }
    }
}
