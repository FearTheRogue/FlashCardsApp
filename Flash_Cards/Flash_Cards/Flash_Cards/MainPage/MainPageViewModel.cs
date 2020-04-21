using System.Collections.Generic;
using System.Threading.Tasks;
using MVVMBase;

namespace Flash_Cards
{
    public class MainPageViewModel : ViewModelBase
    {
        private IMainPageHelper _viewHelper;
        private List<string> _cards;
        private string _titleString = "Nothing Selected";
        private string _selectedString = "Nothing Selected";
        private int _selectedRow = 0;

        public List<string> Cards
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
            get => _titleString;
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        public string SelectedString 
        {
            get => _selectedString;
            set
            {
                if (_selectedString == value) return;
                _selectedString = value;

                TitleString = _selectedString ?? "Nothing Selected";
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

        public async Task ItemSelectionChangedAsync (int row, string cardString)
        {
            SelectedRow = row;
            await _viewHelper.TextPopup("Selected Card: ", $"{cardString} on row {row}");
        }

        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            Cards = new List<string>
            {
                "SOFT262",
                "AINT255",
                "Dinosaurs",
                "Food ",
                "Netflix",
                "Sport",
                "Oop"
            };
        }
    }
}
