using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volitve2022.CosmosDB
{
    public class Lista
    {
        [JsonPropertyName("naziv")]
        public string Naziv { get; set; }

        [JsonPropertyName("nazivKratek")]
        public string NazivKratek { get; set; }

        [JsonPropertyName("stGlasov")]
        public int StGlasov { get; set; }

    }
}
