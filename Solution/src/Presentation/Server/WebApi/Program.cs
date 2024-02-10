using Microsoft.AspNetCore.Builder;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureAppLogging();
builder.ConfigureAppServices();

var app = builder.Build();
app.ConfigureAppPipeline();
app.Run();
