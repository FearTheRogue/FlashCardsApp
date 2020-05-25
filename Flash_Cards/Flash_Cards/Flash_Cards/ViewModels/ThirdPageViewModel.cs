using System.Collections.Generic;
using System.Globalization;
using System.Security.AccessControl;
using System.Threading.Tasks;
using MVVMBase;
using MyAzureLib;
using Newtonsoft.Json.Schema;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private readonly IMainPageHelper _viewHelper;
        private CatagoryCell _categoryid;
        private string id;

        private AzureLibrary azure;
        private List<string> _questions = new List<string>();

        //private List<CardCatagories> cardCatagories = new List<CardCatagories>();
        private string[] cardQuestion;
        private string[] cardAnswer;
        private Question question = new Question();

        public CatagoryCell CategoryId
        {
            get => _categoryid;
            set
            {
                if  (_categoryid == value) { return; }
                _categoryid = value;

                ID = _categoryid.Id;

                CardQuestion = new string[_categoryid.Questions.Length];

                for (int i = 0; i < _categoryid.Questions.Length; i++)
                {
                    cardQuestion[i] = _categoryid.Questions[i].CardQuestion;
                    //cardAnswer[i] = _categoryid.Answers[i].CardAnwser;
                    ListQuestions.Add(cardQuestion[i].ToString());
                }

            }
        }

        public Question GetQuestions
        {
            get => question;
            set
            {
                if(question == value) { return; }
                question = value;
            }
        }

        public string[] CardQuestion
        {
            get => cardQuestion;
            set
            {
                if(cardQuestion == value) { return; }
                cardQuestion = value;
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

        public List<string> ListQuestions
        {
            get => _questions;
            set
            {
                if(_questions == value) { return; }
                _questions = value;
            }
        }

        public async Task Temp()
        {
            //for (int i = 0; i < _categoryid.Questions.Length; i++)
            //{
            //    cardQuestion[i] = _categoryid.Questions[i].CardQuestion;
            //    //cardAnswer[i] = _categoryid.Answers[i].CardAnwser;
            //    ListQuestions.Add(cardQuestion[i].ToString());
            //}

           

        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            azure = SingletonModel.SingletonInstance.Library;

            //foreach (var item in CategoryId.Questions)
            //{
            //    ListQuestions.Add(item.CardQuestion);
            //}
        

            //ListQuestions.Add("Test");

            //_questions = SingletonModel.SingletonInstance.Categories;
            
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
