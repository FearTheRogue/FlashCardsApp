namespace Flash_Cards
{
    public class CatagoryCell
    {
        public string Id { get; set; }
        public string Catagory { get; set; }

        public int CardCount { get; set; }

        public MyAzureLib.Question[] Questions { get; set; }
        public MyAzureLib.Answer[] Answers { get; set; }
        
        //public CatagoryCell(string _id, string _catagory, int _Count, Question[] _questions, Answer[] _answers) => (Id, Catagory, CardCount, Questions, Answers) = (_id, _catagory, _Count, _questions, _answers);

       // public Question[] Questions { get; set; }
   
        //public Answer[] Answers { get; set; }

        public CatagoryCell(string id, string catagory, int cardCount, MyAzureLib.Question[] questions, MyAzureLib.Answer[] answers)
        {
            Id = id;
            Catagory = catagory;
            CardCount = cardCount;
            Questions = questions;
            Answers = answers;
        }

        public CatagoryCell(Question[] questions,Answer[] answers)
        {
            //Questions = questions;
            //Answers = answers;
        }

        
        //public Question[] Questions { get; set; }
        //public Answer[] Answers { get; set; }

        //public CatagoryCell(string _catagory, string _question, string _answer) => (Catagory, Questions, Answers) = (_catagory, _question, _answer);
    }

    public class Question
    {
        public string CardQuestion { get; set; }
    }

    public class Answer
    {
        public string CardAnswer { get; set; }
    }

    //public class Question
    //{
    //    public string CardQuestion { get; set; }
    //}

    //public class Answer
    //{
    //    public string CardAnswer { get; set; }
    //}
}