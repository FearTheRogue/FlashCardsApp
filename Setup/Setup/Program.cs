using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace Setup
{
    class Program
    {
        private static readonly string EndPointURL = "https://flashcards.documents.azure.com:443/";

        private static readonly string PrimaryKey = "GKzcAXRvFZ2T60l5n8zmX74l1gJTuvE0KxFGVp5yrYyQXGbp7dByuLwiWYXUyuyxkuTg0hjWQduOF5urkwtaYg==";

        private CosmosClient cosmosClient;

        private Database database;

        private Container container;

        private readonly string databaseID = "ListOfCardsDB";
        private readonly string containerID = "Items";

        public static async Task Main(string[] args)
        {
            var c = new Program();
            await c.Go();
        }

        public async Task Go()
        {
            this.cosmosClient = new CosmosClient(EndPointURL, PrimaryKey, new CosmosClientOptions()
            {
                ApplicationName = "ListOfCards"
            });

            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseID);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);

            this.container = await this.database.CreateContainerIfNotExistsAsync(containerID, "/Cards", 400);

            await AddCardIfDoesNotExist(new CardCatagories("SOFT262", 4));
            await AddCardIfDoesNotExist(new CardCatagories("AINT255", 4));
            await AddCardIfDoesNotExist(new CardCatagories("Dinosaurs", 4));
            await AddCardIfDoesNotExist(new CardCatagories("Food ", 4));
            await AddCardIfDoesNotExist(new CardCatagories("Netflix", 4));
            await AddCardIfDoesNotExist(new CardCatagories("Sport", 4));
            await AddCardIfDoesNotExist(new CardCatagories("Oop", 4));

            //await QueryAllRecords(true);
            //await QueryAllRecords(false);
        }

        async Task AddCardIfDoesNotExist(CardCatagories c)
        {
            try
            {
                ItemResponse<CardCatagories> cardResponse = await this.container.ReadItemAsync<CardCatagories>(c.Catagory, new PartitionKey(c.Cards));
                Console.WriteLine("Item in database with ID: {0} already exists\n", cardResponse.Resource.Catagory);
            } 
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardResponse = await this.container.CreateItemAsync<CardCatagories>(c, new PartitionKey(c.Cards));

                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardResponse.Resource.Catagory, cardResponse.RequestCharge);
            }   
        }

        async Task QueryAllRecords(bool exp)
        {
        }

        private async Task DeleteItemAsync(string name)
        {
            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<CardCatagories> resp = await this.container.DeleteItemAsync<CardCatagories>(name, new PartitionKey(""));
            Console.WriteLine($"Deleted {name} - response {resp}");
        }
    }
}
