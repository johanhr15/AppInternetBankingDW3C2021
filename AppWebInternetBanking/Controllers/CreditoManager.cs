﻿using AppWebInternetBanking.Models;
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
    /// Esta clase conecta con el controlador de Creditos en el API REST
    /// </summary>
    public class CreditoManager
    {
        string UrlBase = "http://localhost:49220/api/Credito/";

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
        /// Este metodo obtiene un Credito proveniente del API
        /// </summary>
        /// <param name="token"></param>
        /// <param name="codigo"></param>
        /// <returns>Objeto Credito</returns>
        public async Task<Credito> ObtenerCredito(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<Credito>(response);
        }

        /// <summary>
        /// Este metodo obtiene la lista de Creditos del API
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Lista IEnumerable de objetos Credito</returns>
        public async Task<IEnumerable<Credito>> ObtenerCreditos(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<Credito>>(response);
        }

        public async Task<Credito> Ingresar(Credito credito, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(credito), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Credito>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Credito> Actualizar(Credito credito, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(credito), Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Credito>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}