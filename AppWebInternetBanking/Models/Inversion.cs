using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Inversion
    {
        public int INV_CODIGO { get; set; }
        public int INV_CUENTAORIGEN { get; set; }
        public string INV_FONDOSINV { get; set; }
        public string INV_PLAZO { get; set; }
        public string INV_MONEDA { get; set; }
        public int INV_MONTO { get; set; }
    }
}