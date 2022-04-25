using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volitve2022
{
    public partial class UdelezbaDto
    {
        [JsonPropertyName("datum")]
        public DateTimeOffset Datum { get; set; }

        [JsonPropertyName("cas_udelezba")]
        public string CasUdelezba { get; set; }

        [JsonPropertyName("slovenija")]
        public Dictionary<string, double?> Slovenija { get; set; }

        [JsonPropertyName("enote")]
        public UdelezbaEnoteDto[] Enote { get; set; }

        [JsonPropertyName("posebna_volisca")]
        public UdelezbaEnoteDto[] PosebnaVolisca { get; set; }
    }

    public partial class UdelezbaEnoteDto
    {
        [JsonPropertyName("okraji")]
        public UdelezbaEnoteDto[] Okraji { get; set; }

        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("upr")]
        public int? Upr { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }

        [JsonPropertyName("tip")]
        public int? Tip { get; set; }
    }
}
