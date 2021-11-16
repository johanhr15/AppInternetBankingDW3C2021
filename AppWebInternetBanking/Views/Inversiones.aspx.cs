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
    public partial class Inversiones : System.Web.UI.Page
    {
        IEnumerable<Inversion> inversion = new ObservableCollection<Inversion>();
        InversionManager inversionManager = new InversionManager();
        protected void Page_Load(object sender, EventArgs e)
            
            {
                if (!IsPostBack)
                {
                    if (Session["CodigoUsuario"] == null)
                        Response.Redirect("~/Login.aspx");
                    else
                    IncializarControles();
                }
            }
        private async void IncializarControles()
        {
            try
            {
                inversion = await inversionManager.ObtenerInversion(Session["Token"].ToString());
                gvInversiones.DataSource = inversion.ToList();
                gvInversiones.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar los ahorros. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigoMant.Text = string.Empty;
            txtCuentaOrigen.Text = string.Empty;
            txtFondosInversion.Text = string.Empty;
            txtPlazo.Text = string.Empty;
            txtCodigoMoneda.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            ltrTituloMantenimiento.Text = "Nueva Inversion";
            lblResultado.Text = String.Empty;

            LimpiarControles();


            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvInversiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvInversiones.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Inversion";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtCuentaOrigen.Text = fila.Cells[1].Text;
                    txtFondosInversion.Text = fila.Cells[2].Text;
                    txtPlazo.Text = fila.Cells[3].Text;
                    txtCodigoMoneda.Text = fila.Cells[4].Text;
                    txtCantidad.Text = fila.Cells[5].Text;
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el ahorro " + fila.Cells[0].Text + "-" + fila.Cells[4].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
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
                        Inversion inversion = new Inversion()
                        {
                            CuentaOrigen = Convert.ToInt32(txtCuentaOrigen.Text),
                            FondosInversion = txtFondosInversion.Text,
                            Plazo = txtPlazo.Text,
                            CodigoMoneda = Convert.ToInt32(txtCodigoMoneda.Text),
                            Cantidad = Convert.ToInt32(txtCantidad.Text)
                            
                        };

                        Inversion respuestaInversion = await inversionManager.Ingresar(inversion, Session["Token"].ToString());



                        if (!string.IsNullOrEmpty(respuestaInversion.FondosInversion))
                        {
                            lblResultado.Text = "Inversion ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            IncializarControles();
                        }
                    }
                    else // modificar
                    {

                        Inversion inversion = new Inversion()
                        {
                            Codigo = Convert.ToInt32(txtCodigoMant.Text),
                            CuentaOrigen = Convert.ToInt32(txtCuentaOrigen.Text),
                            FondosInversion = txtFondosInversion.Text,
                            Plazo = txtPlazo.Text,
                            CodigoMoneda = Convert.ToInt32(txtCodigoMoneda.Text),
                            Cantidad = Convert.ToInt32(txtCantidad.Text)
                        };


                        Inversion respuestaInversion = await inversionManager.Actualizar(inversion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaInversion.FondosInversion))
                        {
                            lblResultado.Text = "Inversion modificada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            IncializarControles();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error en la operacion. " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                            "$(function() { CloseMantenimiento(); });", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await inversionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Solicitud de Ahorro eliminada";
                    btnAceptarModal.Visible = false;
                    //.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    IncializarControles();
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar el ahorro. " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        private void LimpiarControles()
        {

            foreach (Control item in Page.Controls)
            {
                foreach (Control hijo in item.Controls)
                {
                    if (hijo is TextBox)
                        ((TextBox)hijo).Text = string.Empty;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }
    }
}
