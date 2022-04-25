using System.Text.Json.Serialization;

namespace Volitve2022.CosmosDB;

public class Volisce
{

    [JsonPropertyName("naziv")]
    public string Naziv { get; set; }

    [JsonPropertyName("upravicencev")]
    public int? Upravicencev { get; set; }

    [JsonPropertyName("glasov")]
    public int Glasov { get; set; }

    [JsonPropertyName("neveljavnih")]
    public int Neveljavnih { get; set; }

    [JsonPropertyName("kandidati")]
    public IEnumerable<Kandidat> Kandidati { get; set; }
}