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
    public class CertificadoDepositoManager
    {
        string UrlBase = "http://localhost:49220/api/CertificadoDeposito/";

        HttpClient GetClient(string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }

        public async Task<CertificadoDeposito> ObtenerCertificadoDeposito(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(string.Concat(UrlBase, codigo));

            return JsonConvert.DeserializeObject<CertificadoDeposito>(response);
        }

        public async Task<IEnumerable<CertificadoDeposito>> ObtenerCertificadosDepositos(string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.GetStringAsync(UrlBase);

            return JsonConvert.DeserializeObject<IEnumerable<CertificadoDeposito>>(response);
        }

        public async Task<CertificadoDeposito> Ingresar(CertificadoDeposito certificado, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(certificado),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<CertificadoDeposito>(await
                response.Content.ReadAsStringAsync());
        }

        public async Task<CertificadoDeposito> Actualizar(CertificadoDeposito certificado, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(UrlBase,
                new StringContent(JsonConvert.SerializeObject(certificado),
                Encoding.UTF8,
                "application/json"));

            return JsonConvert.DeserializeObject<CertificadoDeposito>(await response.
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