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
    public partial class frmCredito : System.Web.UI.Page
    {
        IEnumerable<Credito> creditos = new ObservableCollection<Credito>();
        CreditoManager creditoManager = new CreditoManager();
        IEnumerable<Usuario> usuarios = new ObservableCollection<Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();
        IEnumerable<Moneda> monedas = new ObservableCollection<Moneda>();
        MonedaManager monedaManager = new MonedaManager();
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
                creditos = await creditoManager.ObtenerCreditos(Session["Token"].ToString());
                gvCreditos.DataSource = creditos.ToList();
                gvCreditos.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios2";
                lblStatus.Visible = true;
            }

            try
            {
                usuarios = await usuarioManager.ObtenerUsuarios(Session["Token"].ToString());
                ddlCodigoUsuario.DataSource = usuarios.ToList();
                ddlCodigoUsuario.DataBind();
                ddlCodigoUsuario.DataTextField = "Nombre";
                ddlCodigoUsuario.DataValueField = "Codigo";
                ddlCodigoUsuario.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios3. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

            try
            {
                monedas = await monedaManager.ObtenerMonedas(Session["Token"].ToString());
                ddlCodigoMoneda.DataSource = monedas.ToList();
                ddlCodigoMoneda.DataBind();
                ddlCodigoMoneda.DataTextField = "Descripcion";
                ddlCodigoMoneda.DataValueField = "Codigo";
                ddlCodigoMoneda.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios4. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text)) //insertar
            {
                try
                {
                    if (Convert.ToDateTime(txtFecha.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFecha.Text))
                    {
                        Credito credito = new Credito()
                        {
                            CRE_COD_CLIENTE = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CRE_COD_MONEDA = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            CRE_BANCO = ddlEntidadFinanciera.SelectedValue,
                            CRE_PLAZO = Convert.ToDecimal(txtPlazos.Text),
                            CRE_INICIO = Convert.ToDateTime(txtFecha.Text),
                            CRE_MONTO = Convert.ToDecimal(txtMontoCredito.Text),
                            CRE_INGRESOS = Convert.ToDecimal(txtIngresos.Text)
                        };

                        Credito creditoIngresado = await creditoManager.Ingresar(credito, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(creditoIngresado.CRE_BANCO))
                        {
                            lblResultado.Text = "Credito ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                            Correo correo = new Correo();
                            correo.Enviar("Ha solicitado un nuevo credito con:", creditoIngresado.CRE_BANCO, "johanhr100@gmail.com",
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
                    if (Convert.ToDateTime(txtFecha.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFecha.Text))
                    {
                        Credito credito = new Credito()
                        {
                            CRE_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            CRE_COD_CLIENTE = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CRE_COD_MONEDA = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            CRE_BANCO = ddlEntidadFinanciera.SelectedValue,
                            CRE_PLAZO = Convert.ToDecimal(txtPlazos.Text),
                            CRE_INICIO = Convert.ToDateTime(txtFecha.Text),
                            CRE_MONTO = Convert.ToDecimal(txtMontoCredito.Text),
                            CRE_INGRESOS = Convert.ToDecimal(txtIngresos.Text)
                        };


                        Credito creditoActualizado = await creditoManager.Actualizar(credito, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(creditoActualizado.CRE_BANCO))
                        {
                            lblResultado.Text = "Servicio actualizado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                            Correo correo = new Correo();
                            correo.Enviar("Servicio actualizado con exito", creditoActualizado.CRE_BANCO, "svillagra07@gmail.com",
                                Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                        }
                        else
                        {
                            lblResultado.Text = "Hubo un error al efectuar la operacion1";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Maroon;
                        }

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
                    lblResultado.Text = "Hubo un error al efectuar la operacion2";
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
                resultado = await creditoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Solicitud de Credito eliminada";
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
                    Vista = "frmCredito.aspx",
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
            ltrTituloMantenimiento.Text = "Nueva Solictud de Credito";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-success";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            txtCodigoMant.Text = string.Empty;
            txtPlazos.Text = string.Empty;
            txtMontoCredito.Text = string.Empty;
            txtIngresos.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvCreditos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCreditos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Solicitud de Credito";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    ddlCodigoUsuario.SelectedValue = row.Cells[1].Text.Trim();
                    ddlCodigoMoneda.SelectedValue = row.Cells[2].Text.Trim();
                    ddlEntidadFinanciera.SelectedValue = row.Cells[3].Text.Trim();
                    txtPlazos.Text = row.Cells[4].Text.Trim();
                    txtFecha.Text = row.Cells[5].Text.Trim();
                    txtMontoCredito.Text = row.Cells[6].Text.Trim();
                    txtIngresos.Text = row.Cells[7].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar la solicitud de Credito?";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }


    }
}