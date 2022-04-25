using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volitve2022
{
    public partial class KandidatiRezultatDto
    {
        [JsonPropertyName("datum")]
        public DateTimeOffset Datum { get; set; }

        [JsonPropertyName("kandidati")]
        public KandidatiDto[] Kandidati { get; set; }
    }

    public partial class KandidatiDto
    {
        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("naziv")]
        public string Naziv { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }

        [JsonPropertyName("man")]
        public bool Man { get; set; }

        [JsonPropertyName("enota")]
        public int Enota { get; set; }

        [JsonPropertyName("okraji")]
        public int[] Okraji { get; set; }
    }
}
