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
                    Creditos = await CreditoManager.ObtenerCreditos(Session["Token"].ToString());
                    InicializarControles();
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

            foreach (var Credito in Creditos.GroupBy(e => e.CRE_BANCO).
                Select(group => new
                {
                    CRE_BANCO = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.CRE_BANCO))
            {
                string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labels.Append(string.Format("'{0}',", Credito.CRE_BANCO));
                data.Append(string.Format("'{0}',", Credito.Cantidad));
                backgroundColors.Append(string.Format("'{0}',", color));

                labelsGrafico = labels.ToString().Substring(0, labels.Length - 1);
                dataGrafico = data.ToString().Substring(0, data.Length - 1);
                backgroundcolorsGrafico = backgroundColors.ToString().Substring(backgroundColors.Length - 1);
            }

        }

        private void InicializarControles()
        {
            try
            {
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