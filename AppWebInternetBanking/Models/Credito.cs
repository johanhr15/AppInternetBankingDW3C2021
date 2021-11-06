
namespace AppWebInternetBanking.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Credito
    {

        public int CRE_CODIGO { get; set; }
        public int CRE_COD_CLIENTE { get; set; }
        public int CRE_COD_MONEDA { get; set; }
        public string CRE_BANCO { get; set; }
        public decimal CRE_PLAZO { get; set; }
        public System.DateTime CRE_INICIO { get; set; }
        public decimal CRE_MONTO { get; set; }
        public decimal CRE_INGRESOS { get; set; }

    }
}