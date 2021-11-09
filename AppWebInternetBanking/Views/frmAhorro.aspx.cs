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
    public partial class frmAhorro : System.Web.UI.Page
    {
        IEnumerable<Ahorro> ahorros = new ObservableCollection<Ahorro>();
        AhorroManager ahorroManager = new AhorroManager();
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
                ahorros = await ahorroManager.ObtenerAhorro(Session["Token"].ToString());
                gvAhorro.DataSource = ahorros.ToList();
                gvAhorro.DataBind();
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
                        Ahorro ahorros = new Ahorro()
                        {
                            AH_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            AH_CUENTAORIGEN = Convert.ToInt32(txtCuentaOrigenA.Text),
                            AH_MONTO = Convert.ToInt32(txtMontoA.Text),
                            AH_PLAZO = txtPlazo.Text,
                            AH_TIPOAHO = txtTipoAhorro.Text
                        };
                    }
                }

                else // modificar
                {
                    try
                    {
                        Ahorro ahorro = new Ahorro()
                        {
                            AH_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            AH_CUENTAORIGEN = Convert.ToInt32(txtCuentaOrigenA.Text),
                            AH_MONTO = Convert.ToInt32(txtMontoA.Text),
                            AH_PLAZO = txtPlazo.Text,
                            AH_TIPOAHO = txtTipoAhorro.Text
                        };


                        Ahorro respuestaAhorro = await AhorroManager.Actualizar(ahorro, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(respuestaAhorro.AH_PLAZO))
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
                        resultado = await AhorroManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                        if (!string.IsNullOrEmpty(resultado))
                        {
                            ltrModalMensaje.Text = "Solicitud de Ahorro eliminada";
                            btnAceptarModal.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                            InicializarControles();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Hubo un error al cargar la lista de servicios. " + exc.Message;
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

                protected void gvahorro_RowCommand(object sender, GridViewCommandEventArgs e)
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow fila = gvAhorro.Rows[index];

                    switch (e.CommandName)
                    {
                        case "Modificar":
                            ltrTituloMantenimiento.Text = "Modificar Inversion";
                            txtCodigoMant.Text = fila.Cells[0].Text;
                            txtCuentaOrigen.Text = fila.Cells[1].Text;
                            txtMonto.Text = fila.Cells[2].Text;
                            txtPlazo.Text = fila.Cells[3].Text;
                            txtTipoAhorro.Text = fila.Cells[4].Text;
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
            } 
            }
    }
    }
