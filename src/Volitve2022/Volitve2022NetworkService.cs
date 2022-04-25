using System.Net.Http.Json;

namespace Volitve2022
{
    public class Volitve2022NetworkService
    {
        private readonly HttpClient _httpClient;
        public Volitve2022NetworkService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<UdelezbaDto> GetUdelezba()
        {
            return await this.GetFromJsonAsync<UdelezbaDto>("https://volitve.dvk-rs.si/data/udelezba.json");
        }

        public async Task<RezultatDto> GetRezultat()
        {
            return await this.GetFromJsonAsync<RezultatDto>("https://volitve.dvk-rs.si/data/rezultati.json");
        }

        public async Task<RezultatDto> GetKandidatiRezultat()
        {
            return await this.GetFromJsonAsync<RezultatDto>("https://volitve.dvk-rs.si/data/kandidati_rezultati.json");
        }


        public async Task<VolisceDto> GetVolisce(int stEnote, int stOkraja)
        {
            return await this.GetFromJsonAsync<VolisceDto>($"https://volitve.dvk-rs.si/data/volisca_{stEnote:00}_{stOkraja:00}.json");
        }

        public async Task<IEnumerable<VolisceDto>> GetVolisca()
        {
            List<VolisceDto> volisca = new List<VolisceDto>();

            var udelezba = await this.GetUdelezba();
            if (udelezba.Enote is not null && udelezba.Enote.Any())
            {
                foreach (var enota in udelezba.Enote)
                {
                    foreach (var okraj in enota.Okraji)
                    {
                        var volisce = await this.GetVolisce(enota.St, okraj.St);
                        volisca.Add(volisce);
                    }
                }
            }

            return volisca;
        }



        protected async Task<T> GetFromJsonAsync<T>(string url) where T : class
        {
            try
            {
                //var data = await this._httpClient.GetFromJsonAsync<T>(url);

                var httpResponse = await this._httpClient.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadFromJsonAsync<T>();
                    return data;
                }
                else
                {
                    var error = await httpResponse.Content.ReadAsStringAsync();
                    string errorMessage = $"Error getting data from {url}.";
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception e)
            {
                string errorMessage = $"Cant get data from {url}. : {e.Message}";
                throw new Exception(errorMessage, e);
            }
        }


    }
}