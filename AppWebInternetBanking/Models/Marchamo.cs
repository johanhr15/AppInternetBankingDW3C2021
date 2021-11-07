using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Marchamo
    {
        public int idMarchamo { get; set; }
        public string idPlaca { get; set; }
        public decimal monto { get; set; }
        public decimal seguroVehiculo { get; set; }
        public decimal aporteCSV { get; set; }
        public decimal impuestoPropiedad { get; set; }
        public decimal impuestoMunicipalidad { get; set; }
        public decimal timbreFS { get; set; }
        public decimal iva { get; set; }
    }
}