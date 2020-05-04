using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Flash_Cards
{
    public partial class CardItemManager
    {
        static CardItemManager cardInstance = new CardItemManager();
        const string cosmosURL = "https://flashcards.documents.azure.com:443/";
        const string cosmosKey = "GKzcAXRvFZ2T60l5n8zmX74l1gJTuvE0KxFGVp5yrYyQXGbp7dByuLwiWYXUyuyxkuTg0hjWQduOF5urkwtaYg==";
        const string databaseID = "ListOfCardsDB";
        const string collectionID = "Items";

        private readonly Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseID, collectionID);

        private readonly DocumentClient client;

        private CardItemManager()
        {
            client = new DocumentClient(new System.Uri(cosmosURL), cosmosKey);
        }

        public static CardItemManager CardManager
        {
            get
            {
                return cardInstance;
            }
            private set
            {
                cardInstance = value;
            }
        }

        public List<CatagoryCell> Items { get;  private set; }

        public async Task<List<CatagoryCell>> GetCatagoryItemsAsync()
        {
            try
            {
                var query = client.CreateDocumentQuery<CatagoryCell>(collectionLink, new FeedOptions { MaxItemCount = -1 }).OrderBy(Items => Items.Catagory).AsDocumentQuery();

                Items = new List<CatagoryCell>();
                while (query.HasMoreResults)
                {
                    Items.AddRange(await query.ExecuteNextAsync<CatagoryCell>());
                }
            } catch (Exception e)
            {
                Console.Error.WriteLine("ERROR {0}", e.Message);
                return null;
            }
            return Items;
        }

        public async Task<CatagoryCell> InsertCardItemAsync(CatagoryCell cardItem)
        {
            try
            {
                var result = await client.CreateDocumentAsync(collectionLink, cardItem);
                cardItem.Catagory = result.Resource.Id;
                Items.Add(cardItem);
            } 
            catch (Exception e)
            {
                Console.Error.WriteLine("ERROR {0}", e.Message);
            }
            return cardItem;
        }
    }
}
