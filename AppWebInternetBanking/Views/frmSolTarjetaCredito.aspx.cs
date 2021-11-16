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
    public partial class frmSolTarjetaCredito : System.Web.UI.Page
    {
        IEnumerable<Sol_Tarjeta_Credito> sol_Tarjeta_Credito = new ObservableCollection<Sol_Tarjeta_Credito>();
        SolTarjetaCreditoManager solTarjetaCreditoManager = new SolTarjetaCreditoManager();
        static int _codigo = -2;

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
                sol_Tarjeta_Credito = await solTarjetaCreditoManager.GetSolTarjetasCredito(Session["Token"].ToString());
                gvSolicitudes.DataSource = sol_Tarjeta_Credito.ToList();
                gvSolicitudes.DataBind();

                if (sol_Tarjeta_Credito.ToList().Count > 0)
                {
                    lblStatus.Visible = false;
                    
                }
                else
                {
                    lblStatus.Text = "No hay solicitudes de tarjetas de credito disponibles";
                    lblStatus.Visible = true;
                }

               

            }
            catch (Exception ex)
            {

                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmSolTarjetaCredito",
                    Accion = "InicializarControles",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };

                Error errorIngresado = await errorManager.Ingresar(error);

                //lblStatus.Text = "Hubo un error al cargar la lista de solitudes";
                //lblStatus.Visible = true;

                ltrErrorMessage.Text = "Hubo un error al cargar la lista de solicitudes de tarjetas";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);
            }
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtIdSolTarjeta.Text) && !string.IsNullOrEmpty(txtfechaNacimiento.Text) &&
                    !string.IsNullOrEmpty(txtIngresoMensual.Text) && !string.IsNullOrEmpty(txtNombreEmpresa.Text)
                    && !string.IsNullOrEmpty(txtTelefonoTrabajo.Text) && !string.IsNullOrEmpty(txtTelefonoContacto.Text)) //insertar
                {

                     if (CalculateAge( Convert.ToDateTime(txtfechaNacimiento.Text)) >= 18)
                    {
                        Sol_Tarjeta_Credito sol_Tarjeta_Credito = new Sol_Tarjeta_Credito()
                        {
                            cedula = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                            fechaNacimiento = Convert.ToDateTime(txtfechaNacimiento.Text),
                            ingresoMensual = Convert.ToDecimal(txtIngresoMensual.Text),
                            condicionLaboral = ddlCondicionLaboral.SelectedValue,
                            idTipoTarjeta = Convert.ToInt32(ddlTipoTarjeta.SelectedValue),
                            nombreEmpresa = txtNombreEmpresa.Text,
                            telefonoTrabajo = Convert.ToInt32(txtTelefonoTrabajo.Text),
                            puesto = txtPuesto.Text,
                            tiempoLaborar = ddlTiempoLaborar.SelectedValue,
                            telefonoContacto = Convert.ToInt32(txtTelefonoContacto.Text)
                        };

                        Sol_Tarjeta_Credito solicitudInsertada = await solTarjetaCreditoManager.Insert(sol_Tarjeta_Credito, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(solicitudInsertada.nombreEmpresa))
                        {
                            lblResultado.Text = $"Solicitud #{solicitudInsertada.idSolTarjeta} ingresada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();

                            Correo correo = new Correo();
                            correo.Enviar("Nueva solicitud incluida", Convert.ToString(solicitudInsertada.idSolTarjeta), "bgonzaleza003@gmail.com",
                                Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                        }
                        else
                        {
                            ltrErrorMessage.Text = "Hubo un error al ingresar una nueva solicitud";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);
                        }

                    }

                    else
                    {
                        ltrErrorMessage.Text = "Ingresa una fecha de un mayor de edad.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);
                    }

                } 
              
                else if(!string.IsNullOrEmpty(txtIdSolTarjeta.Text) && !string.IsNullOrEmpty(txtfechaNacimiento.Text) &&
                    !string.IsNullOrEmpty(txtIngresoMensual.Text) && !string.IsNullOrEmpty(txtNombreEmpresa.Text)
                    && !string.IsNullOrEmpty(txtTelefonoTrabajo.Text) && !string.IsNullOrEmpty(txtTelefonoContacto.Text))
                {
                    if (CalculateAge(Convert.ToDateTime(txtfechaNacimiento.Text)) >= 18)
                    {

                        Sol_Tarjeta_Credito sol_Tarjeta_Credito = new Sol_Tarjeta_Credito()
                        {
                            idSolTarjeta = Convert.ToInt32(txtIdSolTarjeta.Text),
                            cedula = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                            fechaNacimiento = Convert.ToDateTime(txtfechaNacimiento.Text),
                            ingresoMensual = Convert.ToDecimal(txtIngresoMensual.Text),
                            condicionLaboral = ddlCondicionLaboral.SelectedValue,
                            idTipoTarjeta = Convert.ToInt32(ddlTipoTarjeta.SelectedValue),
                            nombreEmpresa = txtNombreEmpresa.Text,
                            telefonoTrabajo = Convert.ToInt32(txtTelefonoTrabajo.Text),
                            puesto = txtPuesto.Text,
                            tiempoLaborar = ddlTiempoLaborar.SelectedValue,
                            telefonoContacto = Convert.ToInt32(txtTelefonoContacto.Text)
                        };

                        Sol_Tarjeta_Credito sol_Tarjeta_Actualizada = await solTarjetaCreditoManager.Update(sol_Tarjeta_Credito, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(sol_Tarjeta_Actualizada.puesto))
                        {
                            lblResultado.Text = $"Solicitud de tarjeta #{sol_Tarjeta_Actualizada.idSolTarjeta} actualizada con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            btnAceptarMant.Visible = false;
                            InicializarControles();


                            /*
                            Correo correo = new Correo();
                            correo.Enviar("Solicitud de tarjeta #", Convert.ToString(sol_Tarjeta_Actualizada.idSolTarjeta), "bgonzaleza003@gmail.com",
                                Convert.ToInt32(Session["CodigoUsuario"].ToString()));*/

                            ScriptManager.RegisterStartupScript(this,
                        this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                        }
                        else
                        {
                            ltrErrorMessage.Text = "Hubo un error al actualizar la solicitud";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);
                        }

                    }
                    else
                    {
                        ltrErrorMessage.Text = "Ingresa una fecha de un mayor de edad.";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);
                    }



                }
                else
                {
                    ltrErrorMessage.Text = "Ingrese todos los datos e intente nuevamente.";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "$(document).ready(function () {$('#modalError').modal();});", true);

                }

            }
            catch(Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmSolTarjetaCredito",
                    Accion = "btnAceptarMant_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);

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

                int solicitudEliminado = await solTarjetaCreditoManager.Delete(_codigo, Session["Token"].ToString());
                if (solicitudEliminado > 0)
                {
                    ltrModalMensaje.Text = "Solicitud eliminada";
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
                    Vista = "frmSolTarjetaCredito",
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

        protected async void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                ltrTituloMantenimiento.Text = "Nueva Solicitud de Tarjeta de Credito";
                btnAceptarMant.ControlStyle.CssClass = "btn btn-success";
                btnAceptarMant.Visible = true;
                ltrIdSolTarjeta.Visible = false;
                txtIdSolTarjeta.Visible = false;
                txtCedula.Text = Session["CodigoUsuario"].ToString();
                txtCedula.Enabled = false;
                txtfechaNacimiento.Text = string.Empty;
                txtIngresoMensual.Text = string.Empty;
                txtNombreEmpresa.Text = string.Empty;
                txtPuesto.Text = string.Empty;
                txtTelefonoContacto.Text = string.Empty;
                txtTelefonoTrabajo.Text = string.Empty;
                lblResultado.Visible = false;

                ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
            }
            catch(Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmSolTarjetaCredito",
                    Accion = "btnNuevo_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }

        protected async void gvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSolicitudes.Rows[index];

                switch (e.CommandName)
                {
                    case "Modificar":
                        ltrTituloMantenimiento.Text = $"Modificar solicitud #{row.Cells[0].Text.Trim()}";
                        btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                        ltrIdSolTarjeta.Visible = true;
                        txtIdSolTarjeta.Enabled = false;
                        txtIdSolTarjeta.Text = row.Cells[0].Text.Trim();
                        txtCedula.Text = Session["CodigoUsuario"].ToString();
                        txtCedula.Enabled = false;
                        txtfechaNacimiento.Text = string.Empty;
                        txtIngresoMensual.Text = string.Empty;
                        txtNombreEmpresa.Text = string.Empty;
                        txtPuesto.Text = string.Empty;
                        txtTelefonoContacto.Text = string.Empty;
                        txtTelefonoTrabajo.Text = string.Empty;
                        lblResultado.Visible = false;
                        btnAceptarMant.Visible = true;
                        lblResultado.Visible = false;
                        ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                        break;
                    case "Eliminar":
                        _codigo = Convert.ToInt32(row.Cells[0].Text.Trim());
                        ltrModalMensaje.Text = $"Esta seguro que desea eliminar la solicitud #{row.Cells[0].Text.Trim()}?";
                        ScriptManager.RegisterStartupScript(this,
                   this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                        break;
                    default:
                        break;
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
                    Vista = "frmSolTarjetaCredito",
                    Accion = "gvSolicitudes_RowCommand",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);
            }
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell celdaCondicionLaboral = e.Row.Cells[4];
                if (celdaCondicionLaboral.Text == "1")
                {
                    celdaCondicionLaboral.Text = "Asalariado";
                }
                if (celdaCondicionLaboral.Text == "2")
                {
                    celdaCondicionLaboral.Text = "Independiente";
                }

                TableCell celdaTarjeta = e.Row.Cells[5];
                if (celdaTarjeta.Text == "1")
                {
                    celdaTarjeta.Text = "Millas";
                }
                if (celdaTarjeta.Text == "2")
                {
                    celdaTarjeta.Text = "Puntos";
                }

                TableCell celdaTiempoLaborar = e.Row.Cells[9];
                if (celdaTiempoLaborar.Text == "0")
                {
                    celdaTiempoLaborar.Text = "menor 1 año";
                }
                if (celdaTiempoLaborar.Text == "1")
                {
                    celdaTiempoLaborar.Text = "1 año";
                }
                if (celdaTiempoLaborar.Text == "2")
                {
                    celdaTiempoLaborar.Text = "2 años";
                }
                if (celdaTiempoLaborar.Text == "3")
                {
                    celdaTiempoLaborar.Text = "3 años o mas";
                }

            }
        }


        private int CalculateAge(DateTime birthAge)
        {
            int age = 0;
            age = DateTime.Now.Subtract(birthAge).Days;
            age = age / 365;
            return age;
        }

    }
}