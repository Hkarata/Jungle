using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Jungle.Api.Events;
using System.Text.Json;

namespace Jungle.Api.Data
{
    public class EventDatabase
    {
        private readonly AmazonDynamoDBClient _dynamoDbClient;

        public EventDatabase()
        {
            var accessKey = "DUMMYIDEXAMPLE";
            var secretKey = "DUMMYEXAMPLEKEY";

            var credentials = new BasicAWSCredentials(accessKey, secretKey);

            AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000"
            };

            _dynamoDbClient = new AmazonDynamoDBClient(credentials, clientConfig);
        }

        public async Task AppendAsync<T>(T @event, string tableName) where T : Event
        {
            @event.CreatedAtUtc = DateTime.UtcNow;
            var eventAsJson = JsonSerializer.Serialize<Event>(@event);
            var itemAsDocument = Document.FromJson(eventAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = tableName,
                Item = itemAsAttributes
            };

            await _dynamoDbClient.PutItemAsync(createItemRequest);
        }
    }
}
