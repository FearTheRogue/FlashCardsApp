using Newtonsoft.Json;

namespace Setup
{
    class CardCatagories
    {
        [JsonProperty(PropertyName = "id")]
        public string Catagory { get; set; }
        public double CardCount { get; set; }

        // public bool IsExplored { get; set; }

        public string Cards { get; set; } = "cards";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public CardCatagories(string Cata, double Count) => (Catagory, CardCount) = (Cata, Count);
    }
}
