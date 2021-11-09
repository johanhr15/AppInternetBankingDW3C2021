using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Ahorro
    {
        public int AH_CODIGO { get; set; }
        public int AH_CUENTAORIGEN { get; set; }
        public int AH_MONTO { get; set; }
        public string AH_PLAZO { get; set; }
        public string AH_TIPOAHO { get; set; }
    }
}