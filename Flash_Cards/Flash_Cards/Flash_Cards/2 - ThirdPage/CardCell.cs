namespace Flash_Cards
{
    public class CardCell
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Catagory { get; set; }
        public int CardCount { get; set; }
        public CardCell(string _question, string _answer) 
        {
            (Question, Answer) = (_question, _answer);
        }
        public CardCell(string _catagory, int _cardCount)
        {
            (Catagory, CardCount) = (_catagory, _cardCount);
        
        }


    }
}
