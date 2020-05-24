using System.Collections.Generic;
using System.Security.AccessControl;
using MVVMBase;
using MyAzureLib;


namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private readonly IMainPageHelper _viewHelper;
        private CatagoryCell _categoryid;
        private string id;

        private AzureLibrary azure;
        private List<CatagoryCell> _questions;
        //private List<CardCatagories> cardCatagories = new List<CardCatagories>();

        public CatagoryCell CategoryId
        {
            get => _categoryid;
            set
            {
                if  (_categoryid == value) { return; }
                _categoryid = value;

                ID = _categoryid.Id;
            }
        }

        public string ID
        {
            get => id;
            set
            {
                if (id == value) { return; }
                id = value;
                OnPropertyChanged();
            }
        }

        public List<CatagoryCell> ListQuestions
        {
            get => _questions;
            set
            {
                if(_questions == value) { return; }
                _questions = value;
            }
        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            azure = SingletonModel.SingletonInstance.Library;
            //_questions = SingletonModel.SingletonInstance.Categories;

            ListQuestions.Add(new CatagoryCell(_categoryid.questions, _categoryid.answers));
            /* ButtonCommand = new Command(execute: async () =>
             {
                 bool Question = await viewHelper.ShowQuestion("Question", quetsion);
                 if (Question == null) return;

                 bool Answer = await viewHelper.ShowAnswer("Answer", awnser);
                 if (Answer == null) return;

             });*/

        }
    }
}
