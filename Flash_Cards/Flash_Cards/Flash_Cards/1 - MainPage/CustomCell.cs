namespace Flash_Cards
{
    public class CustomCell 
    {
        public string Catagory { get; set; }
        public int CardCount { get; set; }
        public CustomCell(string Cata, int _Count) => (Catagory, CardCount) = (Cata, _Count);
    }
}