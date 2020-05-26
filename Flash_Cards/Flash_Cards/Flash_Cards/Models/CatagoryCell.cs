namespace Flash_Cards
{
    public class CatagoryCell
    {
        public string Id { get; set; }
        public string Catagory { get; set; }

        public int CardCount { get; set; }

        public MyAzureLib.Question[] Questions { get; set; }
        public MyAzureLib.Answer[] Answers { get; set; }

        public CatagoryCell(string id, string catagory, int cardCount, MyAzureLib.Question[] questions, MyAzureLib.Answer[] answers)
        {
            Id = id;
            Catagory = catagory;
            CardCount = cardCount;
            Questions = questions;
            Answers = answers;
        }

    }

    public class Question
    {
        public string CardQuestion { get; set; }
    }

    public class Answer
    {
        public string CardAnswer { get; set; }
    }
}