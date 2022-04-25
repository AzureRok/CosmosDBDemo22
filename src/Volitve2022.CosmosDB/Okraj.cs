using System.Text.Json.Serialization;

namespace Volitve2022.CosmosDB;

public class Okraj
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("enota")]
    public Enota Enota { get; set; }

    [JsonPropertyName("demoProperty")]
    public string DemoProperty { get; set; }

    [JsonPropertyName("naziv")]
    public string Naziv { get; set; }

    [JsonPropertyName("volisca")]
    public IEnumerable<Volisce> Volisca { get; set; }

    [JsonPropertyName("kandidati")]
    public IEnumerable<Kandidat> Kandidati { get; set; }

    [JsonPropertyName("liste")]
    public IEnumerable<Lista> Liste { get; set; }

    public static IEnumerable<Okraj> FromResults(IEnumerable<VolisceDto> volisca)
    {
        return volisca.Select(p =>
        {
            var volisca = p.Vol.Select(v => new Volisce
            {
                Naziv = v.Naz,
                Upravicencev = v.VolisceUdelDto.Upr,
                Glasov = v.VolisceUdelDto.Gl,
                Neveljavnih = v.RezDto.Nev,
                Kandidati = v.RezDto.Rez.Join(p.Liste, rez => rez.St, list => list.St, (rez, list) =>
                    new Kandidat
                    {
                        Ime = list.Ime,
                        Priimek = list.Pri,
                        Lista = list.Naz,
                        ListaKratek = list.Knaz,
                        StGlasov = rez.Gl,
                    })
            });
            return new Okraj
            {
                Id = p.St.ToString(),
                Enota = new Enota{ Id = p.EnSt, Naziv = p.EnNaz } ,
                DemoProperty = p.EnNaz,
                Naziv = p.Naz,
                Volisca = volisca,
                Kandidati = p.Liste.Select(ok => new Kandidat()
                {
                    Lista = ok.Naz,
                    Ime = ok.Ime,
                    Priimek = ok.Pri,
                    StGlasov = volisca.SelectMany(vol => vol.Kandidati).Where(vol => vol.Lista == ok.Naz).Sum(vol => vol.StGlasov)
                }),
                Liste = p.Liste.Select(ok => new Lista()
                {
                    Naziv = ok.Naz,
                    NazivKratek = ok.Knaz,
                    StGlasov = volisca.SelectMany(vol => vol.Kandidati).Where(vol => vol.Lista == ok.Naz).Sum(vol => vol.StGlasov)
                }),
            };
        });
    }
}