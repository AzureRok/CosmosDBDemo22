using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volitve2022
{
    public partial class RezultatDto
    {
        [JsonPropertyName("datum")]
        public DateTimeOffset Datum { get; set; }

        [JsonPropertyName("slovenija")]
        public RezultatSlovenijaDto[] Slovenija { get; set; }

        [JsonPropertyName("enote")]
        public RezultatEnoteDto[] Enote { get; set; }

        [JsonPropertyName("posebna_volisca")]
        public RezultatPosebnaVoliscaDto[] PosebnaVolisca { get; set; }

        [JsonPropertyName("glas")]
        public int Glas { get; set; }

        [JsonPropertyName("velj")]
        public int Velj { get; set; }

        [JsonPropertyName("nev")]
        public int Nev { get; set; }
    }

    public partial class RezultatEnoteDto
    {
        [JsonPropertyName("okraji")]
        public RezultatPosebnaVoliscaDto[] Okraji { get; set; }

        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("rez")]
        public RezultatRezDto[] Rez { get; set; }

        [JsonPropertyName("glas")]
        public int Glas { get; set; }

        [JsonPropertyName("velj")]
        public int Velj { get; set; }

        [JsonPropertyName("nev")]
        public int Nev { get; set; }
    }

    public partial class RezultatPosebnaVoliscaDto
    {
        [JsonPropertyName("st")]
        public int? St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("rez")]
        public RezultatRezDto[] Rez { get; set; }

        [JsonPropertyName("glas")]
        public int Glas { get; set; }

        [JsonPropertyName("velj")]
        public int Velj { get; set; }

        [JsonPropertyName("nev")]
        public int Nev { get; set; }

        [JsonPropertyName("tip")]
        public int? Tip { get; set; }
    }

    public partial class RezultatRezDto
    {
        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }

        [JsonPropertyName("man")]
        public int? Man { get; set; }
    }

    public partial class RezultatSlovenijaDto
    {
        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("knaz")]
        public string Knaz { get; set; }

        [JsonPropertyName("hcol")]
        public string Hcol { get; set; }

        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }

        [JsonPropertyName("man")]
        public int Man { get; set; }
    }
}
