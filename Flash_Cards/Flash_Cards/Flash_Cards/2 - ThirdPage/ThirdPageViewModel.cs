using System.Collections.ObjectModel;
using MVVMBase;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private ObservableCollection<string> _cards;
        private readonly IMainPageHelper _viewHelper;

        public ObservableCollection<string> Temp
        {
            get => _cards;
            set
            {
                if (_cards == value) return;
                _cards = value;
                OnPropertyChanged();
            }
        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

           /* ButtonCommand = new Command(execute: async () =>
            {
                bool Question = await viewHelper.ShowQuestion("Question", "What is your name?");
                if (name == null) return;

                bool Answer = await viewHelper.ShowAnswer("Answer", $"Are you sure you want to set the name to {name}?");
                if (save)
                {
                    Name = name;
                }

            });*/

            Temp = new ObservableCollection<string>()
            {
                //new Temp("hi")
            };
        }
    }
}
