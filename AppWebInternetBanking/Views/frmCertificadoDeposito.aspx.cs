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
    public partial class frmCerfitificadoDeposito : System.Web.UI.Page
    {
        IEnumerable<CertificadoDeposito> certificados = new ObservableCollection<CertificadoDeposito>();
        CertificadoDepositoManager certificadoManager = new CertificadoDepositoManager();
        IEnumerable<Usuario> usuarios = new ObservableCollection<Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();
        IEnumerable<Moneda> monedas = new ObservableCollection<Moneda>();
        MonedaManager monedaManager = new MonedaManager();

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
                certificados = await certificadoManager.ObtenerCertificadosDepositos(Session["Token"].ToString());
                gvCertificados.DataSource = certificados.ToList();
                gvCertificados.DataBind();
            }
            catch (Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario = 0,
                    FechaHora = DateTime.Now,
                    Vista = "Login.aspx",
                    Accion = "btnAceptar_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };

                Error errorIngresaso = await errorManager.Ingresar(error);
                lblStatus.Text = "Hubo un error al cargar la lista de servicios.";
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
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
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
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo certificado";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrCodigoUsuario.Visible = true;
            txtCodigoCuenta.Visible = true;
            ltrCodigoCuenta.Visible = true;
            ltrCodigoMoneda.Visible = true;
            txtMonto.Visible = true;
            ltrMonto.Visible = true;
            txtInteres.Visible = true;
            ltrInteres.Visible = true;
            txtFechaInicio.Visible = true;
            ltrFechaInicio.Visible = true;
            txtFechaFin.Visible = true;
            ltrFechaFin.Visible = true;
            txtCodigoMant.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvCertificados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCertificados.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Certificado";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    ddlCodigoUsuario.SelectedValue = row.Cells[1].Text.Trim();
                    txtCodigoCuenta.Text = row.Cells[2].Text.Trim();
                    ddlCodigoMoneda.SelectedValue = row.Cells[3].Text.Trim();
                    txtMonto.Text = row.Cells[4].Text.Trim();
                    txtInteres.Text = row.Cells[5].Text.Trim();
                    txtFechaInicio.Text = row.Cells[6].Text.Trim();
                    txtFechaFin.Text = row.Cells[7].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el Certificado # " + lblCodigoEliminar.Text + "?";
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
                if (string.IsNullOrEmpty(txtCodigoMant.Text))//Insertar
                {
                    if (Convert.ToDateTime(txtFechaInicio.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaInicio.Text) && Convert.ToDateTime(txtFechaFin.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaFin.Text))
                    {
                        CertificadoDeposito certificado = new CertificadoDeposito()
                        {
                            CodigoUsuario = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CodigoCuenta = Convert.ToInt32(txtCodigoCuenta.Text),
                            CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            Monto = Convert.ToDecimal(txtMonto.Text),
                            Interes = txtInteres.Text,
                            FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                            FechaFin = Convert.ToDateTime(txtFechaFin.Text)
                        };

                        CertificadoDeposito certificadoIngresado = await certificadoManager.Ingresar(certificado, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(certificadoIngresado.Interes))
                        {
                            lblResultado.Text = "Servicio ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();
                            //Correo correo = new Correo();
                            //correo.Enviar("Nuevo servicio incluido", certificado.Descripcion, "svillagra07@gmail.com");
                        }
                        else
                        {
                            lblResultado.Text = "Hubo un error al efectuar la operacion.";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Maroon;
                        }
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }



                }
                else //Modificar
                {
                    if (Convert.ToDateTime(txtFechaInicio.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaInicio.Text) && Convert.ToDateTime(txtFechaFin.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaFin.Text))
                    {
                        CertificadoDeposito certificado = new CertificadoDeposito()
                        {
                            Codigo = Convert.ToInt32(txtCodigoMant.Text),
                            CodigoUsuario = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CodigoCuenta = Convert.ToInt32(txtCodigoCuenta.Text),
                            CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            Monto = Convert.ToDecimal(txtMonto.Text),
                            Interes = txtInteres.Text,
                            FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                            FechaFin = Convert.ToDateTime(txtFechaFin.Text)
                        };

                        CertificadoDeposito certificadoModificado = await certificadoManager.Actualizar(certificado, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(certificadoModificado.Interes))
                        {
                            lblResultado.Text = "Servicio actualizado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();
                        }
                        else
                        {
                            lblResultado.Text = "Hubo un error al efectuar la operacion.";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Maroon;
                        }
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al efectuar la operacion.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }
            }
            catch 
            {
                lblResultado.Text = "Hubo un error al efectuar la operacion.";
                lblResultado.Visible = true;
                lblResultado.ForeColor = Color.Maroon;
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseMantenimiento(); });", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await certificadoManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "Servicio eliminado";
                btnAceptarModal.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                InicializarControles();
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }
    }
}