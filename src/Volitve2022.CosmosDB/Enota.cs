using System.Text.Json.Serialization;

namespace Volitve2022.CosmosDB;

public class Enota
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("naziv")]
    public string Naziv { get; set; }

    //[JsonPropertyName("okraji")]
    //public IEnumerable<Okraj> Okraji { get; set; }

    //[JsonPropertyName("kandidati")]
    //public IEnumerable<Kandidat> Kandidati { get; set; }

    //[JsonPropertyName("liste")]
    //public IEnumerable<Lista> Liste { get; set; }

    //public static IEnumerable<Enota> FromResults(IEnumerable<VolisceDto> volisca)
    //{
    //    var uniqueEnoteIds = volisca.Select(ue => ue.EnSt).Distinct();
    //    return uniqueEnoteIds.Select(enotaId =>
    //    {
    //        var enoteDtos = volisca.Where(p => p.EnSt == enotaId);

    //        var okraji = enoteDtos.Select(p =>
    //        {
    //            var volisca = p.Vol.Select(v => new Volisce
    //            {
    //                Naziv = v.Naz,
    //                Upravicencev = v.VolisceUdelDto.Upr,
    //                Glasov = v.VolisceUdelDto.Gl,
    //                Neveljavnih = v.RezDto.Nev,
    //                Kandidati = v.RezDto.Rez.Join(p.Liste, rez => rez.St, list => list.St, (rez, list) =>
    //                    new Kandidat
    //                    {
    //                        Ime = list.Ime,
    //                        Priimek = list.Pri,
    //                        Lista = list.Naz,
    //                        ListaKratek = list.Knaz,
    //                        StGlasov = rez.Gl,
    //                    })
    //            });
    //            return new Okraj
    //            {
    //                Naziv = p.Naz,
    //                Volisca = volisca,
    //                Kandidati = p.Liste.Select(ok => new Kandidat()
    //                {
    //                    Lista = ok.Naz,
    //                    Ime = ok.Ime,
    //                    Priimek = ok.Pri,
    //                    StGlasov = volisca.SelectMany(vol => vol.Kandidati).Where( vol => vol.Lista == ok.Naz).Sum( vol => vol.StGlasov)
    //                }),
    //                Liste = p.Liste.Select(ok => new Lista()
    //                {
    //                    Naziv = ok.Naz,
    //                    NazivKratek = ok.Knaz,
    //                    StGlasov = volisca.SelectMany(vol => vol.Kandidati).Where(vol => vol.Lista == ok.Naz).Sum(vol => vol.StGlasov)
    //                }),
    //            };
    //        });

    //        var kandidati = okraji.SelectMany(okk => okk.Kandidati).Where(okk=> string.IsNullOrEmpty(okk.Ime)).Select(okk => new Kandidat
    //        {
    //            Ime = okk.Ime,
    //            Priimek = okk.Priimek,
    //            Lista = okk.Lista,
    //        }).Distinct();

    //        foreach (var kandidat in kandidati)
    //        {
    //            kandidat.StGlasov = okraji.SelectMany(kk => kk.Kandidati).Where(kk => kk.Lista == kandidat.Lista)
    //                .Sum(kk => kk.StGlasov);
    //        }

    //        var liste = okraji.SelectMany(okk => okk.Liste).Select(okk => new Lista
    //        {
    //            Naziv = okk.Naziv,
    //            NazivKratek = okk.NazivKratek,
    //        }).Distinct();

    //        foreach (var lista in liste)
    //        {
    //            lista.StGlasov = okraji.SelectMany(kk => kk.Liste).Where(kk => kk.Naziv == lista.Naziv)
    //                .Sum(kk => kk.StGlasov);
    //        }

    //        var enotaDto = enoteDtos.First();
    //        return new Enota
    //        {
    //            Id = enotaDto.EnSt,
    //            Naziv = enotaDto.EnNaz,
    //            Okraji = okraji,
    //            Kandidati = kandidati,
    //            Liste = liste,
    //        };

    //   });
    //}

}