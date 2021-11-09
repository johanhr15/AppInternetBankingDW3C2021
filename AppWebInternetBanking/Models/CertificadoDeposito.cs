using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class CertificadoDeposito
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoCuenta { get; set; }
        public int CodigoMoneda { get; set; }
        public decimal Monto { get; set; }
        public string Interes { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }

    }
}