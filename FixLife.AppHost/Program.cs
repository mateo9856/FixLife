var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FixLife_Admin_Server>("fixlife-admin-server");

builder.Build().Run();
