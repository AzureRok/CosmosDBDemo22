using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volitve2022;
using Volitve2022.CosmosDB;


const string fileName = "volitve2022data.json";

IConfiguration config;
var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureHostConfiguration(app => {
    app.SetBasePath(Directory.GetCurrentDirectory());
    app.AddJsonFile("appsettings.json");
    app.AddUserSecrets<Program>();
});
builder.ConfigureServices((context, services) =>
{
    config = context.Configuration;
    services.Configure<CosmosDbSettings>(config.GetSection(CosmosDbSettings.CosmosDbSettingsSection));


    services.AddHttpClient();
    services.AddTransient<Volitve2022NetworkService>();
    services.AddScoped<CosmosDbService>();
});

using IHost host = builder.Build();


using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //await SaveDataToFile(services.GetRequiredService<Volitve2022NetworkService>());

    var cosmosDbSrvice = services.GetRequiredService<CosmosDbService>();
    var data = await ReadDataFromFile();
    var requestCharge = await cosmosDbSrvice.Init(data);
    Console.WriteLine($"Inserting data cost: {requestCharge} RUs");



    //var reportQuery1 = await cosmosDbSrvice.PartitionQueryDemo(1);
    //Console.WriteLine($"Partition Query data cost: {reportQuery1.TotalCostRUs} RUs");

    //var reportQuery2 = await cosmosDbSrvice.NonPartitionQueryDemo(1);
    //Console.WriteLine($"NonPartition Query data cost: {reportQuery2.TotalCostRUs} RUs");

    //var reportQuery3 = await cosmosDbSrvice.ReadDemo(50);
    //Console.WriteLine($"Read data cost: {reportQuery3.TotalCostRUs} RUs");

    //var reportQuery4 = await cosmosDbSrvice.WriteDemo(50);
    //Console.WriteLine($"Write data cost: {reportQuery4.TotalCostRUs} RUs");
}

static async Task SaveDataToFile(Volitve2022NetworkService volitveService)
{
    var result = await volitveService.GetVolisca();
    var results = Okraj.FromResults(result).ToArray();
    JsonSerializerOptions jso = new JsonSerializerOptions();
    jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    var serializedData = JsonSerializer.Serialize(results, jso);
    await File.WriteAllTextAsync(fileName, serializedData);
}

static async Task<IEnumerable<Okraj>> ReadDataFromFile()
{
    await using Stream fileStream = File.OpenRead(fileName);
    return await JsonSerializer.DeserializeAsync<IEnumerable<Okraj>>(fileStream);
}


internal static class OssRaffle
{
    public static IEnumerable<string> GetResults()
    {
        return new [] {
                "Uroš Mrak",
                "Darko Lacen",
                "Žiga Vajdič",
                "Pavel Maslov",
                "Biserka Cvetkovska",
                "Matej"
            }
            .OrderBy(_=>Guid.NewGuid())
            .Select((x, i) => $"{i+1} - {x}");
    }
}