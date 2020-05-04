namespace Flash_Cards
{
    public class CatagoryCell
    {
        public string Id { get; set; }
        public string Catagory { get; set; }

        public int CardCount { get; set; }

        public CatagoryCell(string _catagory, int _Count) => (Catagory, CardCount) = (_catagory, _Count);

        public string Questions { get; set; }
        public string Answers { get; set; }

        public CatagoryCell(string _catagory, string _question, string _answer) => (Catagory, Questions, Answers) = (_catagory, _question, _answer);
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