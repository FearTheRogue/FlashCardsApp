using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace MyAzureLib
{
    public class CardCatagories
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
        public CardCatagories() { }
        public CardCatagories(string id, string catagory, int cards) => (Id, Catagory, CardCount) = (id, catagory, cards);
        public CardCatagories(string id, string catagory, int cards, Question[] question, Answer[] answer) => (Id, Catagory, CardCount, Questions, Answers) = (id, catagory, cards, question, answer);
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