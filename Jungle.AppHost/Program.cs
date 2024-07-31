var builder = DistributedApplication.CreateBuilder(args);

var dynamoDb = builder.AddContainer("Jungle-DynamoDb", "amazon/dynamodb-local")
    .WithEntrypoint("java")
    .WithArgs("-jar", "DynamoDBLocal.jar", "-inMemory", "-sharedDb")
    .WithEndpoint(8000, 8000)
    .WithOtlpExporter();

var seq = builder.AddSeq("seq")
    .WithEnvironment("ACCEPT_EULA", "Y");

var apiService = builder.AddProject<Projects.Jungle_Api>("Jungle-api")
    .WithReference(seq);

builder.AddProject<Projects.Jungle>("Jungle-web")
    .WithReference(apiService)
    .WithReference(seq);

builder.Build().Run();