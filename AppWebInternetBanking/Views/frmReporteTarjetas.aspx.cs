using AppWebInternetBanking.Controllers;
using AppWebInternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWebInternetBanking.Views
{
    public partial class frmReporteTarjetas : System.Web.UI.Page
    {
        IEnumerable<Sol_Tarjeta_Credito> tarjetas = new ObservableCollection<Sol_Tarjeta_Credito>();
        SolTarjetaCreditoManager tarjetaManager = new SolTarjetaCreditoManager();


        public string etiquetasGrafico = string.Empty;
        public string coloresGrafico = string.Empty;
        public string informacionGrafico = string.Empty;

        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    tarjetas = await tarjetaManager.GetSolTarjetasCredito(Session["Token"].ToString());
                    await InicializarControlesAsync();
                    ObtenerDatosgrafico();
                }
            }
        }

        private void ObtenerDatosgrafico()
        {
            StringBuilder labels = new StringBuilder();
            StringBuilder data = new StringBuilder();
            StringBuilder backgroundColors = new StringBuilder();

            var random = new Random();


            foreach (var tarjeta in tarjetas.GroupBy(e => e.idTipoTarjeta).
                Select(group => new
                {
                    TipoTarjeta = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.TipoTarjeta))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));


                    labels.Append(string.Format("'{0}',", tarjeta.TipoTarjeta));
                    data.Append(string.Format("'{0}',", tarjeta.Cantidad));
                backgroundColors.Append(string.Format("'{0}',", color));

                etiquetasGrafico = labels.ToString().Substring(0, labels.Length - 1);
                informacionGrafico = data.ToString().Substring(0, data.Length - 1);
                coloresGrafico = backgroundColors.ToString().Substring(backgroundColors.Length - 1);
            }

        }

        private async Task InicializarControlesAsync()
        {
            try
            {
                gvTarjetas.DataSource = tarjetas.ToList();
                gvTarjetas.DataBind();
            }
            catch (Exception ex)
            {

                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmReporteTarjetas",
                    Accion = "InicializarControles",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };

                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }
    }
}