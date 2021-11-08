using AppWebInternetBanking.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppWebInternetBanking.Controllers
{
    public class MarchamoManager
    {
        string UrlBase = "http://localhost:49220/api/Marchamo/";


        private HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }


        public async Task<Marchamo> GetMarchamo(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);
            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));
            return JsonConvert.DeserializeObject<Marchamo>(response);
        }


        public async Task<IEnumerable<Marchamo>> GetMarchamos(string token)
        {
            HttpClient httpClient = GetClient(token);
            var response = await httpClient.GetStringAsync(UrlBase);
            return JsonConvert.DeserializeObject<IEnumerable<Marchamo>>(response);
        }

        public async Task<Marchamo> Insert(Marchamo marchamo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(marchamo), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Marchamo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Marchamo> Update(Marchamo marchamo, string token)
        {
            HttpClient httpClient = GetClient(token);
            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(marchamo), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Marchamo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<int> Delete(int codigo, string token)
        {
            HttpClient httpClient = GetClient(token);
            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, codigo));
            return JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
        }


 
    }
}