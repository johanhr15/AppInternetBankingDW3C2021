//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sol_Tarjeta_Credito
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
    
        public virtual Usuario Usuario { get; set; }
    }
}
