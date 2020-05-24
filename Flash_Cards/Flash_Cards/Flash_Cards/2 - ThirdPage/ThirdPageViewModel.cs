using System.Collections.ObjectModel;
using System.Collections.Generic;
using MVVMBase;
using MyAzureLib;
using Xamarin.Forms;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private ObservableCollection<string> _cards;
        private readonly IMainPageHelper _viewHelper;
        private string _categoryid = "";

        private AzureLibrary azure;
        private List<CatagoryCell> _questions = new List<CatagoryCell>();

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

        public string CategoryId
        {
            get => _categoryid;
            set
            {
                if  (_categoryid == value) { return; }
                _categoryid = value;
                OnPropertyChanged();
            }
        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            azure = SingletonModel.SingletonInstance.Library;
            _questions = SingletonModel.SingletonInstance.Categories;

            

            /* ButtonCommand = new Command(execute: async () =>
             {
                 bool Question = await viewHelper.ShowQuestion("Question", quetsion);
                 if (name == null) return;

                 bool Answer = await viewHelper.ShowAnswer("Answer", awnser);
                 if (save)
                 {
                     Name = name;
                 }

             });*/

            //MessagingCenter.Subscribe<MainPageViewModel, string>(this, "new", (sender, e) =>
            //{
            //    //CatagoryCell newCard = new CatagoryCell(e, 0);
            //    //vm.AddNewCard(newCard);

            //    string passedId = e;
            //});


            Temp = new ObservableCollection<string>()
            {
                //new Temp("hi")
            };
        }
    }
}
