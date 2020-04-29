namespace Flash_Cards
{
    public class CatagoryCell
    {
        public string Catagory { get; set; }

        public int CardCount { get; set; }

        public CatagoryCell(string Cata, int _Count) => (Catagory, CardCount) = (Cata, _Count);
    }
}