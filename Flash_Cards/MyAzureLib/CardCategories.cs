using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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