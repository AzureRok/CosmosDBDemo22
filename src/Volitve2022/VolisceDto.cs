using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Volitve2022
{
    public partial class VolisceDto
    {
        [JsonPropertyName("en_st")]
        public int EnSt { get; set; }

        [JsonPropertyName("en_naz")]
        public string EnNaz { get; set; }

        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("vol")]
        public VolisceVolDto[] Vol { get; set; }

        [JsonPropertyName("liste")]
        public VolisceListeDto[] Liste { get; set; }

        [JsonPropertyName("nastavitve")]
        public VolisceNastavitveDto VolisceNastavitveDto { get; set; }
    }

    public partial class VolisceListeDto
    {
        [JsonPropertyName("ime")]
        public string Ime { get; set; }

        [JsonPropertyName("pri")]
        public string Pri { get; set; }

        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("knaz")]
        public string Knaz { get; set; }

        [JsonPropertyName("hcol")]
        public string Hcol { get; set; }
    }

    public partial class VolisceNastavitveDto
    {
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("datum")]
        public DateTimeOffset Datum { get; set; }

        [JsonPropertyName("cas_udelezba")]
        public string CasUdelezba { get; set; }

        [JsonPropertyName("has_udelezba")]
        public bool HasUdelezba { get; set; }

        [JsonPropertyName("has_rezultat")]
        public bool HasRezultat { get; set; }

        [JsonPropertyName("is_final")]
        public bool IsFinal { get; set; }
    }

    public partial class VolisceVolDto
    {
        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("naz")]
        public string Naz { get; set; }

        [JsonPropertyName("udel")]
        public VolisceUdelDto? VolisceUdelDto { get; set; }

        [JsonPropertyName("rez")]
        public VolisceVolRezDto? RezDto { get; set; }

        [JsonPropertyName("prestetih_glasov")]
        public double? PrestetihGlasov { get; set; }
    }

    public partial class VolisceVolRezDto
    {
        [JsonPropertyName("rez")]
        public VolisceRezRezDto[] Rez { get; set; }

        [JsonPropertyName("glas")]
        public int Glas { get; set; }

        [JsonPropertyName("velj")]
        public int Velj { get; set; }

        [JsonPropertyName("nev")]
        public int Nev { get; set; }
    }

    public partial class VolisceRezRezDto
    {
        [JsonPropertyName("st")]
        public int St { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }
    }

    public partial class VolisceUdelDto
    {
        [JsonPropertyName("upr")]
        public int? Upr { get; set; }

        [JsonPropertyName("gl")]
        public int Gl { get; set; }

        [JsonPropertyName("prc")]
        public double? Prc { get; set; }
    }

}
