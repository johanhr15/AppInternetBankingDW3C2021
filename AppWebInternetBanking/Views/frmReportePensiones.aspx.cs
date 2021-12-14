using AppWebInternetBanking.Controllers;
using AppWebInternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWebInternetBanking.Views
{
    public partial class frmReportePensiones : System.Web.UI.Page
    {
        IEnumerable<Traslado_Pensiones> Pensiones = new ObservableCollection<Traslado_Pensiones>();
        TrasladoPensionManager PensionManager = new TrasladoPensionManager();

        public string labelsGrafico = string.Empty;
        public string backgroundcolorsGrafico = string.Empty;
        public string dataGrafico = string.Empty;
        async protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    
                    InicializarControles();
                    ObtenerDatosgrafico();
                }
            }
        }

        private async void ObtenerDatosgrafico()
        {
            StringBuilder labels = new StringBuilder();
            StringBuilder data = new StringBuilder();
            StringBuilder backgroundColors = new StringBuilder();

            var random = new Random();
            List<string> colores = new List<string>();
            colores.Add("green");
            colores.Add("blue");
            colores.Add("orange");
            colores.Add("red");
            colores.Add("gray");
            colores.Add("purple");
            colores.Add("pink");
            colores.Add("yellow");
            colores.Add("black");
            colores.Add("white");

            int i = 0;
            Pensiones = await PensionManager.ObtenerTrasladoPensiones(Session["Token"].ToString());

            foreach (var Pension in Pensiones.GroupBy(e => e.TRAS_FCL_DESTINO).
                Select(group => new
                {
                    TRAS_FCL_DESTINO = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.TRAS_FCL_DESTINO))
            {
                /* string color = (string.Format("'{0}',", ahorros.TipoAhorro));*/
                labels.Append(string.Format("'{0}',", Pension.TRAS_FCL_DESTINO));
                data.Append(string.Format("'{0}',", Pension.Cantidad));
                backgroundColors.Append(string.Format("'{0}',", colores.ElementAt(i)));

                labelsGrafico = labels.ToString().Substring(0, labels.Length - 1);
                dataGrafico = data.ToString().Substring(0, data.Length - 1);
                backgroundcolorsGrafico = backgroundColors.ToString().Substring(0, backgroundColors.Length - 1);
                i++;
            }
        }


        private async void InicializarControles()
        {
            try
            {
                Pensiones = await PensionManager.ObtenerTrasladoPensiones(Session["Token"].ToString());
                gvPensiones.DataSource = Pensiones.ToList();
                gvPensiones.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}