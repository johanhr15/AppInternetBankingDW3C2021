using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Solicitud_Compra_Casa
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoMoneda { get; set; }
        public string TipoCasa { get; set; }
        public int TasaInteres { get; set; }
        public int ValorCasa { get; set; }
        public int Prima { get; set; }
        public int PlazoMeses { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
    }
}