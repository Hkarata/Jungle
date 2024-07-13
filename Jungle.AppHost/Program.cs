var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Jungle_Api>("Jungle-api");

builder.AddProject<Projects.Jungle>("Jungle-web")
    .WithReference(apiService);

builder.Build().Run();
