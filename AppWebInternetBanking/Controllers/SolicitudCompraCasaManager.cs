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
    public class SolicitudCompraCasaManager
    {
        string UrlBase = "http://localhost:49220/api/SolicitudCompraCasa/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<Solicitud_Compra_Casa> ObtenerSolicitudCompraCasa(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Solicitud_Compra_Casa>(response);
        }

        public async Task<IEnumerable<Solicitud_Compra_Casa>> ObtenerSolicitudesCompraCasa(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Solicitud_Compra_Casa>>(response);
        }

        public async Task<Solicitud_Compra_Casa> Ingresar(Solicitud_Compra_Casa solicitud_compra_casa, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(solicitud_compra_casa),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Solicitud_Compra_Casa>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<Solicitud_Compra_Casa> Actualizar(Solicitud_Compra_Casa solicitud_compra_casa, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(solicitud_compra_casa),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<Solicitud_Compra_Casa>(await response.
                Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string id, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, id));

            return JsonConvert.DeserializeObject<string>(await
                response.Content.ReadAsStringAsync());
        }
    }
}