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
            this.cosmosClient = new CosmosClient(EndPointURL, PrimaryKey, new CosmosClientOptions()
            {
                ApplicationName = "ListOfCards"
            });

            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await this.ScaleContainerAsync();
            await this.AddCardToContainerAsync();
            await this.QueryItemsAsync();

            //await this.DeleteCatagoryItemAsync();

            //await this.DeleteDatabaseAndCleanUpAsync();


            //await AddCardIfDoesNotExist(new CardCatagories("SOFT262", 4, "question 1", "answer 1"));
            //await AddCardIfDoesNotExist(new CardCatagories("AINT255", 4, "question 2", "answer 2"));
            //await AddCardIfDoesNotExist(new CardCatagories("Dinosaurs", 4, "question 3", "answer 3"));
            //await AddCardIfDoesNotExist(new CardCatagories("Food", 4, "question 4", "answer 4"));
            //await AddCardIfDoesNotExist(new CardCatagories("Netflix", 4, "question 5", "answer 5"));
            //await AddCardIfDoesNotExist(new CardCatagories("Sport", 4, "question 6", "answer 6"));
            //await AddCardIfDoesNotExist(new CardCatagories("Oop", 4, "question 7", "answer 7"));


            //await QueryAllRecords(true);
            //await QueryAllRecords(false);

            // await DeleteItemAsync("Food");
        }

        private async Task CreateDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseID);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        private async Task CreateContainerAsync()
        {
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerID, "/Catagory", 400);
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }

        private async Task ScaleContainerAsync()
        {
            // Read the current throughput
            int? throughput = await this.container.ReadThroughputAsync();
            if (throughput.HasValue)
            {
                Console.WriteLine("Current provisioned throughput : {0}\n", throughput.Value);
                int newThroughput = throughput.Value + 100;
                // Update throughput
                await this.container.ReplaceThroughputAsync(newThroughput);
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
                ItemResponse<CardCatagories> cardCatagory1Response = await this.container.ReadItemAsync<CardCatagories>(cardCatagory1.Id, new PartitionKey(cardCatagory1.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory1Response.Resource.Id);
            } 
            catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await this.container.CreateItemAsync<CardCatagories>(cardCatagory1, new PartitionKey(cardCatagory1.Catagory));
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
                ItemResponse<CardCatagories> cardCatagory2Response = await this.container.ReadItemAsync<CardCatagories>(cardCatagory2.Id, new PartitionKey(cardCatagory2.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory2Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await this.container.CreateItemAsync<CardCatagories>(cardCatagory2, new PartitionKey(cardCatagory2.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }

            CardCatagories cardCatagory3 = new CardCatagories
            {
                Id = "catagory.3",
                Catagory = "Artificial Intelligence",
                CardCount = 3,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "AI question 1"},
                    new Question{CardQuestion = "AI question 2"},
                    new Question{CardQuestion = "AI question 3"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnwser = "AI answer 1"},
                    new Answer{CardAnwser = "AI answer 2"},
                    new Answer{CardAnwser = "AI answer 3"}
                },
            };

            try
            {
                ItemResponse<CardCatagories> cardCatagory3Response = await this.container.ReadItemAsync<CardCatagories>(cardCatagory3.Id, new PartitionKey(cardCatagory3.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory3Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await this.container.CreateItemAsync<CardCatagories>(cardCatagory3, new PartitionKey(cardCatagory3.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }

            CardCatagories cardCatagory4 = new CardCatagories
            {
                Id = "catagory.4",
                Catagory = "Maths",
                CardCount = 2,
                Questions = new Question[]
               {
                    new Question{CardQuestion = "Maths question 1"},
                    new Question{CardQuestion = "Maths question 2"}
               },
                Answers = new Answer[]
               {
                    new Answer{CardAnwser = "Maths answer 1"},
                    new Answer{CardAnwser = "Maths answer 2"}
               },
            };

            try
            {
                ItemResponse<CardCatagories> cardCatagory4Response = await this.container.ReadItemAsync<CardCatagories>(cardCatagory4.Id, new PartitionKey(cardCatagory4.Catagory));
                Console.WriteLine("Item in database with id: {0} already exists\n", cardCatagory4Response.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> cardCatagoryResponse = await this.container.CreateItemAsync<CardCatagories>(cardCatagory4, new PartitionKey(cardCatagory4.Catagory));
                Console.WriteLine("Created item in database with ID: {0} Operation consumed {1} RUs.\n", cardCatagoryResponse.Resource.Id, cardCatagoryResponse.RequestCharge);
            }
        }

        private async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.Catagory = 'SOFT262'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

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
        }

        private async Task DeleteDatabaseAndCleanUpAsync()
        {
            DatabaseResponse databaseResourse = await this.database.DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", this.databaseID);

            this.cosmosClient.Dispose();
        }

        private async Task DeleteCatagoryItemAsync()
        {
            var partitionKeyValue = "SOFT262";
            var catagoryId = "catagory.1";

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<CardCatagories> itemResponse = await this.container.DeleteItemAsync<CardCatagories>(catagoryId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Catagory [{0}, {1}\n", partitionKeyValue, catagoryId);
        }
    }
}
