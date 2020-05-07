using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;

namespace MyAzureLib
{
    public class AzureLibrary
    {

        public CosmosClient cosmosClient;

        public Database database;

        public Container container;

        public readonly string databaseID = "ListOfCardsDB";
        public readonly string containerID = "Items";

        public AzureLibrary()
        {
            Console.WriteLine("greetings from the library");
        }

        public async Task ReadDataIn()
        {
            await this.CheckDatabaseAsync();
            await this.CheckContainerAsync();
            await this.QueryItemsAsync();
        }

        private async Task CheckDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(this.databaseID);
        }

        private async Task CheckContainerAsync()
        {
            this.container = await this.database.CreateContainerIfNotExistsAsync(this.containerID, "/Catagory", 400); ;
        }

        public async Task<List<CardCatagories>> QueryItemsAsync(List<CardCatagories> cards)
        {
            var sqlQueryText = "SELECT * FROM c ORDER BY c.Catagory";

            //Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<CardCatagories> queryResultSetIterator = this.container.GetItemQueryIterator<CardCatagories>(queryDefinition);

            List<CardCatagories> cardCatagories = new List<CardCatagories>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<CardCatagories> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (CardCatagories catagories in currentResultSet)
                {
                    cardCatagories.Add(catagories);
                    Console.WriteLine("\tRead {0}\n", catagories);
                }
            }
            return cards;
        }
    }
}
