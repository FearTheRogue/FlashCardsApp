﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace MyAzureLib
{
    public class AzureLibrary
    {
        public static readonly string EndPointURL = "https://flashcards.documents.azure.com:443/";

        public static readonly string PrimaryKey = "GKzcAXRvFZ2T60l5n8zmX74l1gJTuvE0KxFGVp5yrYyQXGbp7dByuLwiWYXUyuyxkuTg0hjWQduOF5urkwtaYg==";

        private CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public readonly string databaseID = "ListOfCardsDB";

        public readonly string containerID = "Items";


        // Creating DB if does not exist
        public CosmosClient CosmosClientRef
        {
            get
            {
                if (_cosmosClient == null)
                {
                    _cosmosClient = new CosmosClient(EndPointURL, PrimaryKey, new CosmosClientOptions()
                    {
                        ApplicationName = "ListOfCards"
                    });
                }
                return _cosmosClient;
            }
        }

        public async Task<Database> GetDatabase()
        {
            if (_database == null)
            {
                _database = await CosmosClientRef.CreateDatabaseIfNotExistsAsync(databaseID);
            }
            return _database;
        }

        public async Task<Container> GetContainer()
        {
            if (_container == null)
            {
                Database d = await this.GetDatabase();
                _container = await d.CreateContainerIfNotExistsAsync(containerID, "/Catagory", 400);
            }
            return _container;
        }

        public AzureLibrary()
        {

        }

        // Create new card
        public async Task AddCardToCategory(string id, string newTitle)
        {
            CardCatagories newCardCategory = new CardCatagories
            {
                Id = id,
                Catagory = newTitle,
                CardCount = 0,
                Questions = new Question[]
                {
                    new Question{CardQuestion = "new Category Created"}
                },
                Answers = new Answer[]
                {
                    new Answer{CardAnwser = "new Category Created"}
                }
            };

            try
            {
                ItemResponse<CardCatagories> newCardResponse = await this._container.ReadItemAsync<CardCatagories>(newCardCategory.Id, new PartitionKey(newCardCategory.Catagory));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<CardCatagories> newCardResponse = await this._container.CreateItemAsync<CardCatagories>(newCardCategory, new PartitionKey(newCardCategory.Catagory));
            }
        }


        // Reading in categories from DB
        public async Task QueryItemsAsync(List<CardCatagories> temp)
        {
            var sqlQueryText = "SELECT * FROM c ORDER BY c.Catagory";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

            FeedIterator<CardCatagories> queryResultSetIterator = this._container.GetItemQueryIterator<CardCatagories>(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<CardCatagories> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (CardCatagories catagories in currentResultSet)
                {
                    temp.Add(catagories);
                }
            }
        }

        // Deleting a selected Category
        public async Task DeleteCategoryItemAsync(string categoryTitle, string id)
        {
            var partitionKeyValue = categoryTitle;
            var categoryId = id;

            ItemResponse<CardCatagories> itemResponse = await _container.DeleteItemAsync<CardCatagories>(categoryId, new PartitionKey(partitionKeyValue));
        }
    }
}