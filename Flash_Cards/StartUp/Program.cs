using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using MyAzureLib;


namespace StartUp
{
    public class Program
    {
        private static readonly string EndPointURL = "https://flashcards.documents.azure.com:443/";

        private static readonly string PrimaryKey = "GKzcAXRvFZ2T60l5n8zmX74l1gJTuvE0KxFGVp5yrYyQXGbp7dByuLwiWYXUyuyxkuTg0hjWQduOF5urkwtaYg==";

        //private CosmosClient cosmosClient;

        //private Database database;

        //private Container container;

        //private readonly string databaseID = "ListOfCardsDB";
        //private readonly string containerID = "Items";

        public AzureLibrary obj = new AzureLibrary();

        public static async Task Main(string[] args)
        {
            //obj.container;

            try
            {
                Console.WriteLine("Beginning operations...\n");
                Program p = new Program();
                await p.Go();

            }
            catch (CosmosException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }


        public async Task Go()
        {
            obj.cosmosClient = new CosmosClient(EndPointURL, PrimaryKey, new CosmosClientOptions()
            {
                ApplicationName = "ListOfCards"
            });


            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await obj.QueryItemsAsync();
            // await this.ScaleContainerAsync();
            /// await this.AddCardToContainerAsync();
            // await this.QueryItemsAsync();

            //await this.DeleteCatagoryItemAsync();

            //await this.DeleteDatabaseAndCleanUpAsync();
        }

        public async Task CreateDatabaseAsync()
        { 
            obj.database = await obj.cosmosClient.CreateDatabaseIfNotExistsAsync(obj.databaseID);
            Console.WriteLine("Created Database: {0}\n", obj.database.Id);
        }

        private async Task CreateContainerAsync()
        {
            obj.container = await obj.database.CreateContainerIfNotExistsAsync(obj.containerID, "/Catagory", 400);
            //Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }

        private async Task ScaleContainerAsync()
        {
            // Read the current throughput
            int? throughput = await obj.container.ReadThroughputAsync();
            if (throughput.HasValue)
            {
                Console.WriteLine("Current provisioned throughput : {0}\n", throughput.Value);
                int newThroughput = throughput.Value + 100;
                // Update throughput
                await obj.container.ReplaceThroughputAsync(newThroughput);
                Console.WriteLine("New provisioned throughput : {0}\n", newThroughput);
            }

        }

        //async Task AddCardIfDoesNotExist(CardCatagories c)
        //{
        //    try
        //    {
        //        ItemResponse<CardCatagories> cardResponse = await this.container.ReadItemAsync<CardCatagories>(c.Catagory, new PartitionKey(c.Cards));
        //        Console.WriteLine("Item in database with ID: {0} already exists\n", cardResponse.Resource.Catagory);
        //    } 
        //    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        //    {
        //        ItemResponse<CardCatagories> cardResponse = await this.container.CreateItemAsync<CardCatagories>(c, new PartitionKey(c.Cards));

        //        Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardResponse.Resource.Catagory, cardResponse.RequestCharge);
        //    }   
        //}

        private async Task AddCardToContainerAsync()
        {
            CardCatagories cardCatagory1 = new CardCatagories
            {
                Id = "catagory.1",
                Catagory = "SOFT262",
                CardCount = 2,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "SOFT262 question 1"},
                    new Question{CardQuestion = "SOFT262 question 2"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnwser = "SOFT262 answer 1"},
                    new Answer{CardAnwser = "SOFT262 answer 2"},
                },
            };

            try
            {
                ItemResponse<CardCatagories> cardCatagory1Response = await obj.container.ReadItemAsync<CardCatagories>(cardCatagory1.Id, new PartitionKey(cardCatagory1.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory1Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await obj.container.CreateItemAsync<CardCatagories>(cardCatagory1, new PartitionKey(cardCatagory1.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }

            CardCatagories cardCatagory2 = new CardCatagories
            {
                Id = "catagory.2",
                Catagory = "Dinosaur",
                CardCount = 5,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "Dinosaur question 1"},
                    new Question{CardQuestion = "Dinosaur question 2"},
                    new Question{CardQuestion = "Dinosaur question 3"},
                    new Question{CardQuestion = "Dinosaur question 4"},
                    new Question{CardQuestion = "Dinosaur question 5"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnwser = "Dinosaur answer 1"},
                    new Answer{CardAnwser = "Dinosaur answer 2"},
                    new Answer{CardAnwser = "Dinosaur answer 3"},
                    new Answer{CardAnwser = "Dinosaur answer 4"},
                    new Answer{CardAnwser = "Dinosaur answer 5"}
                },
            };

            try
            {
                ItemResponse<CardCatagories> cardCatagory2Response = await obj.container.ReadItemAsync<CardCatagories>(cardCatagory2.Id, new PartitionKey(cardCatagory2.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory2Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await obj.container.CreateItemAsync<CardCatagories>(cardCatagory2, new PartitionKey(cardCatagory2.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }

            CardCatagories cardCatagory3 = new CardCatagories
            {
                Id = "catagory.3",
                Catagory = "OOP",
                CardCount = 3,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "OOP question 1"},
                    new Question{CardQuestion = "OOP question 2"},
                    new Question{CardQuestion = "OOP question 3"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnwser = "OOP answer 1"},
                    new Answer{CardAnwser = "OOP answer 2"},
                    new Answer{CardAnwser = "OOP answer 3"}
                },
            };

            try
            {
                ItemResponse<CardCatagories> cardCatagory3Response = await obj.container.ReadItemAsync<CardCatagories>(cardCatagory3.Id, new PartitionKey(cardCatagory3.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory3Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await obj.container.CreateItemAsync<CardCatagories>(cardCatagory3, new PartitionKey(cardCatagory3.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }
        }

        private async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c ORDER BY c.Catagory";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<CardCatagories> queryResultSetIterator = obj.container.GetItemQueryIterator<CardCatagories>(queryDefinition);

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
        }

        private async Task DeleteDatabaseAndCleanUpAsync()
        {
            DatabaseResponse databaseResourse = await obj.database.DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", obj.databaseID);

            obj.cosmosClient.Dispose();
        }

        private async Task DeleteCatagoryItemAsync()
        {
            var partitionKeyValue = "SOFT262";
            var catagoryId = "catagory.1";

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<CardCatagories> itemResponse = await obj.container.DeleteItemAsync<CardCatagories>(catagoryId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Catagory [{0}, {1}\n", partitionKeyValue, catagoryId);
        }
    }
}
