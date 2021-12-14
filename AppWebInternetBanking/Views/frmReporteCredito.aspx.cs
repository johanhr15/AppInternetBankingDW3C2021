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
    public partial class frmReporteCredito : System.Web.UI.Page
    {
        IEnumerable<Credito> Creditos = new ObservableCollection<Credito>();
        CreditoManager CreditoManager = new CreditoManager();

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
            Creditos = await CreditoManager.ObtenerCreditos(Session["Token"].ToString());

            foreach (var Credito in Creditos.GroupBy(e => e.CRE_BANCO).
                Select(group => new
                {
                    CRE_BANCO = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.CRE_BANCO))
            {
                /* string color = (string.Format("'{0}',", ahorros.TipoAhorro));*/
                labels.Append(string.Format("'{0}',", Credito.CRE_BANCO));
                data.Append(string.Format("'{0}',", Credito.Cantidad));
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
                Creditos = await CreditoManager.ObtenerCreditos(Session["Token"].ToString());
                gvCredito.DataSource = Creditos.ToList();
                gvCredito.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}