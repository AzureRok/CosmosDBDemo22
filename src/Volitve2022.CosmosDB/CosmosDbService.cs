using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Volitve2022.CosmosDB
{
    public class CosmosDbService : IDisposable
    {
        private readonly CosmosDbSettings _settings;
        private readonly CosmosClient _cosmosClient;
        private Container _container;

        public CosmosDbService(IOptions<CosmosDbSettings> options)
        {
            this._settings = options.Value;

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            CosmosSystemTextJsonSerializer cosmosSystemTextJsonSerializer = new CosmosSystemTextJsonSerializer(jsonSerializerOptions);


            //Regions.NorthEurope
            //Regions.JapanEast
            CosmosClientOptions settings = new CosmosClientOptions()
            {
                Serializer = cosmosSystemTextJsonSerializer,
                ApplicationRegion = this._settings?.PreferredRegions?.FirstOrDefault(),
                //ApplicationPreferredRegions = this._settings.PreferredRegions,
                AllowBulkExecution = true,
            };

            this._cosmosClient = new CosmosClient(this._settings.Endpoint, this._settings.MasterKey, settings);
            this._container = this._cosmosClient.GetContainer(this._settings.Database, this._settings.Container);
        }

        public async Task<double> Init(IEnumerable<Okraj> itemsToInsert)
        {
            try
            {
                await this._container.DeleteContainerAsync();
            }
            catch
            {
                // Container ne obstaja
            }

            var database = _cosmosClient.GetDatabase(_settings.Database);
            this._container = await database
                .DefineContainer(this._settings.Container, "/enota/naziv")
                .WithIndexingPolicy()
                .WithIndexingMode(IndexingMode.Consistent)
                .WithIncludedPaths()
                //.Path("/*")
                .Attach()
                .WithExcludedPaths()
                //.Path("/\"_etag\"/?")
                .Path("/*")
                .Attach()
                .Attach()
                .CreateAsync(GenerateThroughputProperties(false, 400));
            double requestCharge = 0;
            List<Task> tasks = new List<Task>(itemsToInsert.Count());
            foreach (var item in itemsToInsert)
            {
                tasks.Add(this._container.CreateItemAsync(item, new PartitionKey(item.Enota.Naziv))
                    .ContinueWith(itemResponse =>
                    {
                        requestCharge += itemResponse.Result.RequestCharge;
                        if (!itemResponse.IsCompletedSuccessfully)
                        {
                            AggregateException innerExceptions = itemResponse.Exception.Flatten();
                            if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                            {
                                Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                            }
                            else
                            {
                                Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                            }
                        }
                    }));
            }

            await Task.WhenAll(tasks);
            return requestCharge;
        }

        public async Task<CosmosDbRequestReport> PartitionQueryDemo(int count = 100)
        {
            CosmosDbRequestReport report = new CosmosDbRequestReport();

            var totalStartedAt = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var iterator = this._container.GetItemQueryIterator<Okraj>(
                    queryText: "SELECT TOP 1 * FROM c WHERE c.enota.naziv = 'Kranj' AND c.id = '1'",
                    requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey("Kranj") }
                );
                var startedAt = DateTime.Now;
                var result = await iterator.ReadNextAsync();

                var elapsed = DateTime.Now.Subtract(startedAt).TotalMilliseconds;
                var cost = result.RequestCharge;
                report.Details.Add(new CosmosDbRequestReport.CosmosDbRequestReportDetail
                {
                    TimeElapsedInMs = elapsed,
                    CostRUs = cost,
                    Text = $"Query {i + 1}. {result.First().Enota}. {elapsed} ms; {cost} RUs"

                });

            }
            report.TotalTimeElapsedInMs = DateTime.Now.Subtract(totalStartedAt).TotalMilliseconds;

            return report;
        }

        public async Task<CosmosDbRequestReport> NonPartitionQueryDemo(int count = 1000)
        {
            CosmosDbRequestReport report = new CosmosDbRequestReport();

            var totalStartedAt = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                var iterator = this._container.GetItemQueryIterator<dynamic>(
                    queryText: "SELECT * FROM c WHERE c.naziv='Jesenice' AND c.id = '1'"
                );
                var startedAt = DateTime.Now;
                var result = await iterator.ReadNextAsync();

                var elapsed = DateTime.Now.Subtract(startedAt).TotalMilliseconds;
                var cost = result.RequestCharge;

                report.Details.Add(new CosmosDbRequestReport.CosmosDbRequestReportDetail
                {
                    TimeElapsedInMs = elapsed,
                    CostRUs = cost,
                    Text = $"Query {i + 1}. Number rows:  {result.Count()}. {elapsed} ms; {cost} RUs"

                });

            }
            report.TotalTimeElapsedInMs = DateTime.Now.Subtract(totalStartedAt).TotalMilliseconds;

            return report;
        }


        public async Task<CosmosDbRequestReport> ReadDemo(int count = 1000)
        {
            CosmosDbRequestReport report = new CosmosDbRequestReport();

            var totalStartedAt = DateTime.Now;

            for (var i = 0; i < count; i++)
            {
                var startedAt = DateTime.Now;


                var result = await this._container.GetItemQueryIterator<Okraj>("SELECT TOP 1 * FROM c WHERE c.enota.naziv = 'Kranj' AND c.id = '1'").ReadNextAsync();
                var regions = result.Diagnostics.GetContactedRegions();

                var elapsed = DateTime.Now.Subtract(startedAt).TotalMilliseconds;
                var cost = result.RequestCharge;

                report.Details.Add(new CosmosDbRequestReport.CosmosDbRequestReportDetail
                {
                    TimeElapsedInMs = elapsed,
                    CostRUs = cost,
                    Text = $"Read {i + 1} from {string.Join(", ", regions)}. {elapsed} ms; {cost} RUs"
                });
            }
            report.TotalTimeElapsedInMs = DateTime.Now.Subtract(totalStartedAt).TotalMilliseconds;
            return report;

        }

        public async Task<CosmosDbRequestReport> WriteDemo(int count = 1000)
        {
            CosmosDbRequestReport report = new CosmosDbRequestReport();

            var readResult = await this._container.ReadItemAsync<Okraj>(
                    "1", new PartitionKey("Kranj"));

            Okraj doc = readResult.Resource;

            var totalStartedAt = DateTime.Now;
            for (var i = 0; i < count; i++)
            {
                doc.Naziv = $"{doc.Naziv} {Guid.NewGuid()}";
                doc.DemoProperty = $"{doc.Enota.Naziv} {Guid.NewGuid()}";
                var startedAt = DateTime.Now;
                var result = await this._container.ReplaceItemAsync(doc, "1");
                var elapsed = DateTime.Now.Subtract(startedAt).TotalMilliseconds;
                var cost = result.RequestCharge;

                report.Details.Add(new CosmosDbRequestReport.CosmosDbRequestReportDetail
                {
                    TimeElapsedInMs = elapsed,
                    CostRUs = cost,
                    Text = $"Write {i + 1}. {doc.DemoProperty}. {elapsed} ms; {cost} RUs"
                });
            }
            var totalElapsed = DateTime.Now.Subtract(totalStartedAt).TotalMilliseconds;
            report.TotalTimeElapsedInMs = DateTime.Now.Subtract(totalStartedAt).TotalMilliseconds;
            return report;
        }

        private static ThroughputProperties GenerateThroughputProperties(bool autoScale, int? maxThroughput = null)
        {
            if (!autoScale)
            {
                if (!maxThroughput.HasValue || maxThroughput < 400)
                    maxThroughput = 400;
                return ThroughputProperties.CreateManualThroughput(maxThroughput.Value);
            }
            else
            {
                if (!maxThroughput.HasValue || maxThroughput < 4000)
                    maxThroughput = 4000;
                return ThroughputProperties.CreateAutoscaleThroughput(maxThroughput.Value);
            }
        }

        public void Dispose()
        {
            _cosmosClient.Dispose();
        }
    }

    public class CosmosDbRequestReport
    {
        public double TotalTimeElapsedInMs { get; set; }
        public double TotalCostRUs
        {
            get { return this.Details.Sum(p => p.CostRUs); }
        }

        public List<CosmosDbRequestReportDetail> Details { get; set; } = new List<CosmosDbRequestReportDetail>();

        public class CosmosDbRequestReportDetail
        {
            public string Text { get; set; }
            public double TimeElapsedInMs { get; set; }
            public double CostRUs { get; set; }
        }
    }
}
