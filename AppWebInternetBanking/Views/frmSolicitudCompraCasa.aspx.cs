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
    public partial class frmSolicitudCompraCasa : System.Web.UI.Page
    {
        IEnumerable<Solicitud_Compra_Casa> solicitud = new ObservableCollection<Solicitud_Compra_Casa>();
        SolicitudCompraCasaManager SolicitudManager = new SolicitudCompraCasaManager();
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
                solicitud = await SolicitudManager.ObtenerSolicitudesCompraCasa(Session["Token"].ToString());
                gvSolicitudCompraCasa.DataSource = solicitud.ToList();
                gvSolicitudCompraCasa.DataBind();
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
            ltrTituloMantenimiento.Text = "Nueva solicitud";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            ltrCodigoUsuario.Visible = true;
            ltrCodigoMoneda.Visible = true;
            txtTipoCasa.Visible = true;
            ltrTipoCasa.Visible = true;
            txtTasaInteres.Visible = true;
            ltrTasaInteres.Visible = true;
            txtValorCasa.Visible = true;
            ltrValorCasa.Visible = true;
            txtPlazoMeses.Visible = true;
            ltrPlazoMeses.Visible = true;
            txtPrima.Visible = true;
            ltrPrima.Visible = true;
            txtFechaInicio.Visible = true;
            ltrFechaInicio.Visible = true;
            ddlEstado.Enabled = false;
            txtCodigoMant.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvSolicitudCompraCasa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSolicitudCompraCasa.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar solicitud";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    ddlCodigoUsuario.SelectedValue = row.Cells[1].Text;
                    ddlCodigoMoneda.SelectedValue = row.Cells[2].Text;
                    txtTipoCasa.Text = row.Cells[3].Text;
                    txtTasaInteres.Text = row.Cells[4].Text;
                    txtValorCasa.Text = row.Cells[5].Text;
                    txtPrima.Text = row.Cells[6].Text;
                    txtPlazoMeses.Text = row.Cells[7].Text;
                    txtFechaInicio.Text = row.Cells[8].Text;
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar la solicitud # " + lblCodigoEliminar.Text + "?";
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
                    if (Convert.ToDateTime(txtFechaInicio.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaInicio.Text))
                    {
                        Solicitud_Compra_Casa solicitud = new Solicitud_Compra_Casa()
                        {
                            CodigoUsuario = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            TipoCasa = txtTipoCasa.Text,
                            TasaInteres = Convert.ToInt32(txtTasaInteres.Text),
                            ValorCasa = Convert.ToInt32(txtValorCasa.Text),
                            Prima = Convert.ToInt32(txtPrima.Text),
                            PlazoMeses = Convert.ToInt32(txtPlazoMeses.Text),
                            FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                            Estado = ddlEstado.SelectedValue
                        };

                        Solicitud_Compra_Casa solicitudIngresada = await SolicitudManager.Ingresar(solicitud, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(solicitudIngresada.TipoCasa))
                        {
                            lblResultado.Text = "Solicitud ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();
                            //Correo correo = new Correo();
                            //correo.Enviar("Nuevo servicio incluido", certificado.Descripcion, "svillagra07@gmail.com");
                        }
                        else
                        {
                            lblResultado.Text = "Hubo un error al ingresar la solicitud.";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Maroon;
                        }
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar la solicitud.";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }

                }
                else //Modificar
                {
                    if (Convert.ToDateTime(txtFechaInicio.Text) >= DateTime.Now && !string.IsNullOrEmpty(txtFechaInicio.Text))
                    {
                        Solicitud_Compra_Casa solicitud = new Solicitud_Compra_Casa()
                        {
                            Codigo = Convert.ToInt32(txtCodigoMant.Text),
                            CodigoUsuario = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                            TipoCasa = txtTipoCasa.Text,
                            TasaInteres = Convert.ToInt32(txtTasaInteres.Text),
                            ValorCasa = Convert.ToInt32(txtValorCasa.Text),
                            Prima = Convert.ToInt32(txtPrima.Text),
                            PlazoMeses = Convert.ToInt32(txtPlazoMeses.Text),
                            FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                            Estado = ddlEstado.SelectedValue
                        };

                        Solicitud_Compra_Casa solicitudModificada = await SolicitudManager.Actualizar(solicitud, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(solicitudModificada.TipoCasa))
                        {
                            lblResultado.Text = "Solicitud actualizada con exito";
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
            resultado = await SolicitudManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
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