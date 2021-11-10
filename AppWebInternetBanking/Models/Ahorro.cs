using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Ahorro
    {
        public int Codigo { get; set; }
        public int CuentaOrigen { get; set; }
        public int Monto { get; set; }
        public decimal Plazo { get; set; }
        public string TipoAhorro { get; set; }
    }
}