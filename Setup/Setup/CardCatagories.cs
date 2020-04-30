using Newtonsoft.Json;

namespace Setup
{
    class CardCatagories
    {
        [JsonProperty(PropertyName = "id")]

        public string Id { get; set; }
        public string Catagory { get; set; }
        public int CardCount { get; set; }

        public Question[] Questions { get; set; }

        public Answer[] Answers { get; set; }


        public string Cards { get; set; } = "cards";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        //public CardCatagories(string _catagory, int _count, string _question, string _answer) => (Catagory, CardCount, Questions, Answers) = (_catagory, _count, _question,_answer);
    }

    public class Question 
    {
        public string CardQuestion { get; set; }
    }

    public class Answer
    {
        public string CardAnwser { get; set; }
    }

}
