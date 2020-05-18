using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;

namespace MyAzureLib
{
    public class AzureLibrary
    {
        public static readonly string EndPointURL = "https://flashcards.documents.azure.com:443/";

        public static readonly string PrimaryKey = "GKzcAXRvFZ2T60l5n8zmX74l1gJTuvE0KxFGVp5yrYyQXGbp7dByuLwiWYXUyuyxkuTg0hjWQduOF5urkwtaYg==";

        public CosmosClient cosmosClient;

        public Database database;

        public Container container;

        public readonly string databaseID = "ListOfCardsDB";
        public readonly string containerID = "Items";

        public AzureLibrary()
        {
            //ReadDataIn();
        }

        public async Task ReadDataIn()
        {
            await this.CheckDatabaseAsync();
            await this.CheckContainerAsync();
            //await this.QueryItemsAsync();
        }

        private async Task CheckDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(this.databaseID);
        }

        public async Task CheckContainerAsync()
        {
            this.container = await this.database.CreateContainerIfNotExistsAsync(this.containerID, "/Catagory", 400);
           
        }

        public async Task QueryItemsAsync(List<CardCatagories> temp)
        {
            var sqlQueryText = "SELECT * FROM c ORDER BY c.Catagory";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

            FeedIterator<CardCatagories> queryResultSetIterator = this.container.GetItemQueryIterator<CardCatagories>(queryDefinition);

            List<CardCatagories> cardCatagories = new List<CardCatagories>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<CardCatagories> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (CardCatagories catagories in currentResultSet)
                {
                    //cardCatagories.Add(catagories);
                    temp.Add(catagories);
                    //Console.WriteLine("\tRead {0}\n", catagories);
                }
            }
            //return cards;
        }
    }
}
