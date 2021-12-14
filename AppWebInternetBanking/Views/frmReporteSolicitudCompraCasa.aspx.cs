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
    public partial class frmReporteSolicitudCompraCasa : System.Web.UI.Page
    {
        IEnumerable<Solicitud_Compra_Casa> Solicitudes = new ObservableCollection<Solicitud_Compra_Casa>();
        SolicitudCompraCasaManager SolicitudesManager = new SolicitudCompraCasaManager();

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
            Solicitudes = await SolicitudesManager.ObtenerSolicitudesCompraCasa(Session["Token"].ToString());

            foreach (var Solicitudes in Solicitudes.GroupBy(e => e.TipoCasa).
                Select(group => new
                {
                    TipoCasa = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.TipoCasa))
            {

                labels.Append(string.Format("'{0}',", Solicitudes.TipoCasa));
                data.Append(string.Format("'{0}',", Solicitudes.Cantidad));
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
                Solicitudes = await SolicitudesManager.ObtenerSolicitudesCompraCasa(Session["Token"].ToString());
                gvSolicitudes.DataSource = Solicitudes.ToList();
                gvSolicitudes.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}