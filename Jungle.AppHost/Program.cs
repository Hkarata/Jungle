var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Jungle>("Jungle-web");

builder.Build().Run();
