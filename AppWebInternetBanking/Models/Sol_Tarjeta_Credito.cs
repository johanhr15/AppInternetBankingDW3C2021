using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebInternetBanking.Models
{
    public class Sol_Tarjeta_Credito

    {
        public int idSolTarjeta { get; set; }
        public int cedula { get; set; }
        public System.DateTime fechaNacimiento { get; set; }
        public decimal ingresoMensual { get; set; }
        public string condicionLaboral { get; set; }
        public int idTipoTarjeta { get; set; }
        public string nombreEmpresa { get; set; }
        public int telefonoTrabajo { get; set; }
        public string puesto { get; set; }
        public string tiempoLaborar { get; set; }
        public int telefonoContacto { get; set; }

    }
}