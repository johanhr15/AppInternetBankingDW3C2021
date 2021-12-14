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
    public partial class frmReporteCertificadoDeposito : System.Web.UI.Page
    {
        IEnumerable<CertificadoDeposito> Certificados = new ObservableCollection<CertificadoDeposito>();
        CertificadoDepositoManager CertificadoManager = new CertificadoDepositoManager();

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
            Certificados = await CertificadoManager.ObtenerCertificadosDepositos(Session["Token"].ToString());

            foreach (var Certificado in Certificados.GroupBy(e => e.CodigoMoneda).
                Select(group => new
                {
                    CodigoMoneda = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.CodigoMoneda))
            {
                
                labels.Append(string.Format("'{0}',", Certificado.CodigoMoneda));
                data.Append(string.Format("'{0}',", Certificado.Cantidad));
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
                Certificados = await CertificadoManager.ObtenerCertificadosDepositos(Session["Token"].ToString());
                gvCertificados.DataSource = Certificados.ToList();
                gvCertificados.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}