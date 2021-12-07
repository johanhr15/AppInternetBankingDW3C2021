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
    public partial class frmCuenta : System.Web.UI.Page
    {
        IEnumerable<Cuenta> Cuentas = new ObservableCollection<Cuenta>();
        CuentaManager CuentaManager = new CuentaManager();

        static string _codigo = string.Empty;

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
                Cuentas = await CuentaManager.ObtenerCuentas(Session["Token"].ToString());
                gvCuenta.DataSource = Cuentas.ToList();
                gvCuenta.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios";
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text)) //insertar
            {
                try
                {
                    Cuenta Cuentas = new Cuenta()
                    {
                        CodigoUsuario = Convert.ToInt32(txtCodigoUsuario.Text),
                        CodigoMoneda = Convert.ToInt32(txtCodigoMoneda.Text),
                        Descripcion = txtDescripcion.Text,
                        IBAN = txtIBAN.Text,
                        Saldo = Convert.ToDecimal(txtSaldo.Text),
                        Estado = ddlEstado.SelectedValue
                    };

                    Cuenta Cuenta_Ingresada = await CuentaManager.Ingresar(Cuentas, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(Cuenta_Ingresada.Descripcion))
                    {
                        lblResultado.Text = "Solicitud de Cuenta ingresada con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();

                        Correo correo = new Correo();
                        correo.Enviar("Ha solicitado una nuevo Cuenta", "Descripcion: " + Cuenta_Ingresada.Descripcion, "johanhr100@gmail.com",
                            Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                        ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }


                }
                catch
                {
                    lblResultado.Text = "Hubo un error al efectuar la operacion";
                    lblResultado.Visible = true;
                    lblResultado.ForeColor = Color.Maroon;

                }
            }
            else // modificar
            {
                try
                {

                    Cuenta Cuentas = new Cuenta()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        CodigoUsuario = Convert.ToInt32(txtCodigoUsuario.Text),
                        CodigoMoneda = Convert.ToInt32(txtCodigoMoneda.Text),
                        Descripcion = txtDescripcion.Text,
                        IBAN = txtIBAN.Text,
                        Saldo = Convert.ToDecimal(txtSaldo.Text),
                        Estado = ddlEstado.SelectedValue
                    };


                    Cuenta Cuenta_Actualizada = await CuentaManager.Actualizar(Cuentas, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(Cuenta_Actualizada.Descripcion))
                    {
                        lblResultado.Text = "Solicitud de Cuenta actualizada con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();

                        Correo correo = new Correo();
                        correo.Enviar("Solicitud de Cuenta actualizada con exito", "Descripcion: " + Cuenta_Actualizada.Descripcion, "johanhr100@gmail.com",
                            Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                        ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
                catch
                {
                    lblResultado.Text = "Hubo un error al efectuar la operacion";
                    lblResultado.Visible = true;
                    lblResultado.ForeColor = Color.Maroon;

                }
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseMantenimiento(); });", true);

        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await CuentaManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Cuenta eliminada";
                    btnAceptarModal.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
                }
            }
            catch (Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmCuenta.aspx",
                    Accion = "btnAceptarModal_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nueva Solicitud de Cuenta";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-success";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            txtCodigoMant.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCodigoMoneda.Text = string.Empty;
            txtCodigoUsuario.Text = string.Empty;
            txtIBAN.Text = string.Empty;
            txtSaldo.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvCuenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCuenta.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Cuenta";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtCodigoUsuario.Text = row.Cells[1].Text.Trim();
                    txtCodigoMoneda.Text = row.Cells[2].Text.Trim();
                    txtDescripcion.Text = row.Cells[3].Text.Trim();
                    txtIBAN.Text = row.Cells[4].Text.Trim();
                    txtSaldo.Text = row.Cells[5].Text.Trim();
                    ddlEstado.SelectedValue = row.Cells[6].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar la Cuenta?";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }
    }
}