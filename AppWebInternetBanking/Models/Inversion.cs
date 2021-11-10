using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Inversion
    {
        public int Codigo { get; set; }
        public int CuentaOrigen { get; set; }
        public int FondosInversion { get; set; }
        public string Plazo { get; set; }
        public int CodigoMoneda { get; set; }
        public int Cantidad { get; set; }
    }
}