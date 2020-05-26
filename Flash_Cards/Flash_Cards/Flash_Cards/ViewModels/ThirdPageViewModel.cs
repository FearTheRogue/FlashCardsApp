using System.Collections.Generic;
using System.Threading.Tasks;
using MVVMBase;
using MyAzureLib;

namespace Flash_Cards
{
    public class ThirdPageViewModel : ViewModelBase
    {
        private readonly IMainPageHelper _viewHelper;
        private CatagoryCell _categoryid;
        private string id;

        private List<string> _questions = new List<string>();

        //private List<CardCatagories> cardCatagories = new List<CardCatagories>();
        private string[] cardQuestion;
        private Question question = new Question();

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

        // Was not used
        public async Task GetQuestionsFromCategory()
        {
            CardQuestion = new string[_categoryid.Questions.Length];

            for (int i = 0; i < _categoryid.Questions.Length; i++)
            {
                cardQuestion[i] = _categoryid.Questions[i].CardQuestion;
                //cardAnswer[i] = _categoryid.Answers[i].CardAnwser;
                ListQuestions.Add(cardQuestion[i].ToString());
            }
        }

        public ThirdPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;
        }
    }
}
