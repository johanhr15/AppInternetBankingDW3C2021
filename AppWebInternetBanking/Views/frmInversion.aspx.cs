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
        IEnumerable<Inversion> inversiones = new ObservableCollection<Inversion>();
        InversionManager inversionesManager = new InversionManager();


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
                inversiones = await InversionManager.ObtenerInversiones(Session["Token"].ToString());
                gvInversion.DataSource = inversiones.ToList();
                gvInversion.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios";
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
                            INV_CODIGO = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                            INV_FONDOSINV = txtFondosInversion.Text,
                            INV_PLAZO = txtPlazo.Text,
                            INV_MONEDA = txtMoneda.Text,
                            INV_MONTO = Convert.ToInt32(txtMonto.Text)
                        
                        };
                    }
                }

                else // MODIFICAR
                {
                    try
                    {
                        Inversion inversion = new Inversion()
                        {
                            INV_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            INV_CUENTAORIGEN = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                            INV_FONDOSINV = txtFondosInversion.Text,
                            INV_PLAZO = txtPlazo.Text,
                            INV_MONEDA = txtMoneda.Text,
                            INV_MONTO = Convert.ToInt32(txtMonto.Text)
                        };


                        Inversion respuestaInversion = await InversionManager.Actualizar(inversion, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaInversion.INV_FONDOSINV))
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
                        resultado = await InversionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                        if (!string.IsNullOrEmpty(resultado))
                        {
                            ltrModalMensaje.Text = "Solicitud de Inversion eliminada";
                            btnAceptarModal.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                            InicializarControles();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                        lblStatus.Visible = true;
                    }
                }

                protected void btnCancelarModal_Click(object sender, EventArgs e)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);

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

                protected void gvinversion_RowCommand(object sender, GridViewCommandEventArgs e)
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
            f}
            }
    }
}


