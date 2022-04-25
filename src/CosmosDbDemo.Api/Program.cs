using Volitve2022;
using Volitve2022.CosmosDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection(CosmosDbSettings.CosmosDbSettingsSection));
builder.Services.AddHttpClient();
builder.Services.AddScoped<CosmosDbService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(o=>o.EnableTryItOutByDefault());

app.MapGet("/", () => Results.Redirect("swagger")).ExcludeFromDescription();
app.MapPost("/read", async (CosmosDbService cosmosDbService, int? count) => await cosmosDbService.ReadDemo(count??50));
app.MapPost("/write", async (CosmosDbService cosmosDbService, int? count) => await cosmosDbService.WriteDemo(count??50));

app.Run();
