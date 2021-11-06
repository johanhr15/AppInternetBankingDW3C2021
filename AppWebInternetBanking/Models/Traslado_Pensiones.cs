
namespace AppWebInternetBanking.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Traslado_Pensiones
    {
        public int TRAS_CODIGO { get; set; }
        public int TRAS_CLIENTE { get; set; }
        public string TRAS_CLIENTE_CORREO { get; set; }
        public string TRAS_CLIENTE_TELEFONO { get; set; }
        public string TRAS_ROP_DESTINO { get; set; }
        public string TRAS_FCL_DESTINO { get; set; }
    }
}