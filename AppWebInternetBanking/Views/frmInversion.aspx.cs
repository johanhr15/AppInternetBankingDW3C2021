using AppWebInternetBanking.Controllers;
using AppWebInternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWebInternetBanking.Views
{
    public partial class frmInversion : System.Web.UI.Page
    {
        IEnumerable<Inversion> inversion = new ObservableCollection<Inversion>();
        InversionManager inversionManager = new InversionManager();
        IEnumerable<Cuenta> cuentas = new ObservableCollection<Cuenta>();
        CuentaManager cuentaManager = new CuentaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    InicializarControles();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                inversion = await inversionManager.ObtenerInversion(Session["Token"].ToString());
                gvInversion.DataSource = inversion.ToList();
                gvInversion.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios";
                lblStatus.Visible = true;
            }

            try
            {
                cuentas = await cuentaManager.ObtenerCuentas(Session["Token"].ToString());
                ddlCuentaOrigen.DataSource = cuentas.ToList();
                ddlCuentaOrigen.DataBind();
                ddlCuentaOrigen.DataTextField = "IBAN";
                ddlCuentaOrigen.DataValueField = "Codigo";
                ddlCuentaOrigen.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
                    {
                        Inversion inversiones = new Inversion()
                        {
                            Codigo = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                            FondosInversion = txtFondosInversion.Text,
                            Plazo = txtPlazo.Text,
                            CodigoMoneda = Convert.ToInt32(txtMoneda.Text),
                            Cantidad = Convert.ToInt32(txtMonto.Text)

                        };
                    }
                }

                else // MODIFICAR
                {
                    try
                    {
                        Inversion inversion = new Inversion()
                        {
                            Codigo = Convert.ToInt32(txtCodigoMant.Text),
                            CuentaOrigen = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                            FondosInversion = txtFondosInversion.Text,
                            Plazo = txtPlazo.Text,
                            CodigoMoneda = Convert.ToInt32(txtMoneda.Text),
                            Cantidad = Convert.ToInt32(txtMonto.Text)
                        };


                        Inversion respuestaInversion = await inversionManager.Actualizar(inversion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaInversion.FondosInversion))
                        {
                            lblResultado.Text = "Inversion modificada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                        }
                    }
                    catch (Exception exc)
                    {
                        lblStatus.Text = "Hubo un error en la operacion. " + exc.Message;
                        lblStatus.Visible = true;
                    }
                }

            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error en la operacion. " + exc.Message;
                lblStatus.Visible = true;
            }
        }





        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nueva Inversion";
            lblResultado.Text = String.Empty;

            LimpiarControles();

            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }
        private void LimpiarControles()
        {
            foreach (Control item in Page.FindControl("Content1").Controls)
            {
                foreach (Control hijo in item.Controls)
                {
                    if (hijo is TextBox)
                        ((TextBox)hijo).Text = string.Empty;

                }
            }
        }


        protected void gvInversion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvInversion.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Inversion";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCuentaOrigen.SelectedValue = fila.Cells[1].Text;
                    txtFondosInversion.Text = fila.Cells[2].Text;
                    txtPlazo.Text = fila.Cells[3].Text;
                    txtMoneda.Text = fila.Cells[4].Text;
                    txtMonto.Text = fila.Cells[5].Text;
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar la inversion " + fila.Cells[0].Text + "-" + fila.Cells[5].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }

            
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                                    "$(function() { CloseMantenimiento(); });", true);
        }
    }
}
