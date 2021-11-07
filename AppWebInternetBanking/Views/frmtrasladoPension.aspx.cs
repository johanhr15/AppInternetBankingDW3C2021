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
    public partial class frmtrasladoPension : System.Web.UI.Page
    {
        IEnumerable<Traslado_Pensiones> traslado_Pensiones = new ObservableCollection<Traslado_Pensiones>();
        TrasladoPensionManager trasladoPensionManager = new TrasladoPensionManager();
        IEnumerable<Usuario> usuarios = new ObservableCollection<Usuario>();
        UsuarioManager usuarioManager = new UsuarioManager();
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
                traslado_Pensiones = await trasladoPensionManager.ObtenerTrasladoPensiones(Session["Token"].ToString());
                gvtrasladoPensiones.DataSource = traslado_Pensiones.ToList();
                gvtrasladoPensiones.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios";
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
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text)) //insertar
            {
                try
                {
                    if (txtTelefono.Text.Length < 50 && txtTelefono.Text.Length > 7 && txtCorreo.Text.Contains("@"))
                    {
                        Traslado_Pensiones traslado_Pensiones = new Traslado_Pensiones()
                        {
                            TRAS_CLIENTE = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            TRAS_CLIENTE_CORREO = txtCorreo.Text,
                            TRAS_CLIENTE_TELEFONO = txtTelefono.Text,
                            TRAS_ROP_DESTINO = ddlTRAS_ROP_DESTINO.SelectedValue,
                            TRAS_FCL_DESTINO = ddlTRAS_FCL_DESTINO.SelectedValue
                        };

                        Traslado_Pensiones traslado_PensionIngresado = await trasladoPensionManager.Ingresar(traslado_Pensiones, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(traslado_PensionIngresado.TRAS_CLIENTE_CORREO))
                        {
                            lblResultado.Text = "Solicitud de traslado de Pensiones ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                            Correo correo = new Correo();
                            correo.Enviar("Ha solicitado un nuevo traslado de Pensiones","Su entidad de destino para su FCL es: "+ traslado_PensionIngresado.TRAS_FCL_DESTINO, "johanhr100@gmail.com",
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
                        lblResultado.Text = "Los datos ingresados son erroneos";
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
                    if (txtTelefono.Text.Length < 50 && txtTelefono.Text.Length > 7 && txtCorreo.Text.Contains("@"))
                    {
                        Traslado_Pensiones traslado_Pensiones = new Traslado_Pensiones()
                        {
                            TRAS_CODIGO = Convert.ToInt32(txtCodigoMant.Text),
                            TRAS_CLIENTE = Convert.ToInt32(ddlCodigoUsuario.SelectedValue),
                            TRAS_CLIENTE_CORREO = txtCorreo.Text,
                            TRAS_CLIENTE_TELEFONO = txtTelefono.Text,
                            TRAS_ROP_DESTINO = ddlTRAS_ROP_DESTINO.SelectedValue,
                            TRAS_FCL_DESTINO = ddlTRAS_FCL_DESTINO.SelectedValue
                        };


                        Traslado_Pensiones traslado_PensionActualizado = await trasladoPensionManager.Actualizar(traslado_Pensiones, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(traslado_PensionActualizado.TRAS_CLIENTE_CORREO))
                        {
                            lblResultado.Text = "Solicitud de traslado de Pensiones actualizada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                            Correo correo = new Correo();
                            correo.Enviar("Solicitud de traslado de Pensiones actualizada con exito", "Su entidad de destino para su FCL es: " + traslado_PensionActualizado.TRAS_FCL_DESTINO, "johanhr100@gmail.com",
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
                        lblResultado.Text = "Los datos ingresados son erroneos";
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
                resultado = await trasladoPensionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Solicitud de Traslado eliminada";
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
                    Vista = "frmtrasladoPension.aspx",
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
            ltrTituloMantenimiento.Text = "Nueva Solicitud de Traslado de Pensiones";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-success";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            txtCodigoMant.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void gvtrasladoPensiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvtrasladoPensiones.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Solicitud de Traslado";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    ddlCodigoUsuario.SelectedValue = row.Cells[1].Text.Trim();
                    txtCorreo.Text = row.Cells[2].Text.Trim();
                    txtTelefono.Text = row.Cells[3].Text.Trim();
                    ddlTRAS_ROP_DESTINO.SelectedValue = row.Cells[4].Text.Trim();
                    ddlTRAS_FCL_DESTINO.SelectedValue = row.Cells[5].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar la solicitud de traslado?";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }
    }
}