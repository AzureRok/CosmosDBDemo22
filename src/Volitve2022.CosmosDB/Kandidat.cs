using System.Text.Json.Serialization;

namespace Volitve2022.CosmosDB;

public class Kandidat
{
    [JsonPropertyName("ime")]
    public string Ime { get; set; }

    [JsonPropertyName("priimek")]
    public string Priimek { get; set; }

    [JsonPropertyName("lista")]
    public string Lista { get; set; }

    [JsonPropertyName("listaKratek")]
    public string ListaKratek { get; set; }



    [JsonPropertyName("stGlasov")]
    public int StGlasov { get; set; }

}