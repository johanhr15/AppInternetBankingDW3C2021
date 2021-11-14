using AppWebInternetBanking.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppWebInternetBanking.Controllers
{
    public class InversionManager
    {
        string Url = "http://localhost:49220/api/inversion/";


        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "aplication/json");

            return httpClient;
        }

        public async Task<IEnumerable<Inversion>> ObtenerInversion(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Inversion>>(resultado);
        }

        public async Task<Inversion> ObtenerInversiones(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Inversion>(resultado);
        }

        public async Task<Inversion> Ingresar(Inversion inversion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(inversion), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Inversion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Inversion> Actualizar(Inversion inversion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(inversion), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Inversion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }

}