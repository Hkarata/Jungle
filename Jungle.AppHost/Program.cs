var builder = DistributedApplication.CreateBuilder(args);

var dynamoDb = builder.AddContainer("DynamoDb", "amazon/dynamodb-local")
    .WithHttpEndpoint(8000, 8002)
    .WithHttpsEndpoint(8001, 8003)
    .WithOtlpExporter();

var apiService = builder.AddProject<Projects.Jungle_Api>("Jungle-api");

builder.AddProject<Projects.Jungle>("Jungle-web")
    .WithReference(apiService);

builder.Build().Run();
