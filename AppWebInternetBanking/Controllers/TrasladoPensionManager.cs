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
    /// <summary>
    /// Esta clase conecta con el controlador de Traslado_Pensiones en el API REST
    /// </summary>
    public class TrasladoPensionManager
    {
        string UrlBase = "http://localhost:49220/api/trasladoPension/";

        /// <summary>
        /// Metodo que inicializa el objeto HttpClient
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Objeto HttpClient con los headers inicializados</returns>
        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        /// <summary>
        /// Este metodo obtiene un Traslado_Pensiones proveniente del API
        /// </summary>
        /// <param name="token"></param>
        /// <param name="codigo"></param>
        /// <returns>Objeto Traslado_Pensiones</returns>
        public async Task<Traslado_Pensiones> ObtenerTrasladoPension(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Traslado_Pensiones>(response);
        }

        /// <summary>
        /// Este metodo obtiene la lista de Traslado_Pensioness del API
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Lista IEnumerable de objetos Traslado_Pensiones</returns>
        public async Task<IEnumerable<Traslado_Pensiones>> ObtenerTrasladoPensiones(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Traslado_Pensiones>>(response);
        }

        public async Task<Traslado_Pensiones> Ingresar(Traslado_Pensiones traslado_Pensiones, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(traslado_Pensiones), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Traslado_Pensiones>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Traslado_Pensiones> Actualizar(Traslado_Pensiones traslado_Pensiones, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(traslado_Pensiones), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Traslado_Pensiones>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}