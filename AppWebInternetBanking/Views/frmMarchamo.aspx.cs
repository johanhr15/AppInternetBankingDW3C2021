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
    public partial class frmMarchamo : System.Web.UI.Page
    {


        IEnumerable<Marchamo> marchamos = new ObservableCollection<Marchamo>();
        MarchamoManager marchamoManager = new MarchamoManager();
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
                marchamos = await marchamoManager.GetMarchamos(Session["Token"].ToString());
                gvMarchamos.DataSource = marchamos.ToList();
                gvMarchamos.DataBind();
            }
            catch (Exception ex)
            {

                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario = Convert.ToInt32( Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmMarchamo",
                    Accion = "InicializarControles",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };

                Error errorIngresado = await errorManager.Ingresar(error);

                lblStatus.Text = "Hubo un error al cargar la lista de marchamos";
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtidMarchamo.Text)) //insertar
                {
                    Marchamo marchamo = new Marchamo()
                    {
                        idPlaca = txtPlaca.Text,
                        monto = Convert.ToDecimal(txtMonto.Text),
                        seguroVehiculo = Convert.ToDecimal(txtSoa.Text),
                        impuestoPropiedad = Convert.ToDecimal(txtImpPro.Text),
                        impuestoMunicipalidad = Convert.ToDecimal(txtImpMuni.Text),
                        timbreFS = Convert.ToDecimal(txtFauna.Text),
                        iva = Convert.ToDecimal(txtIva.Text),
                        aporteCSV = Convert.ToDecimal(txtAcsv.Text)
                    };

                    Marchamo marchamoInsertado = await marchamoManager.Insert(marchamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(marchamoInsertado.idPlaca))
                    {
                        lblResultado.Text = $"Marchamo de placa {marchamoInsertado.idPlaca} ingresado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();

                        Correo correo = new Correo();
                        correo.Enviar("Nuevo marchamo incluido", marchamoInsertado.idPlaca, "bgonzaleza003@gmail.com",
                            Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                        ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar nuevo marchamo";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }

                }


                else // modificar
                {
                    Marchamo marchamo = new Marchamo()
                    {
                        idMarchamo = Convert.ToInt32(txtidMarchamo.Text),
                        idPlaca = txtPlaca.Text,
                        monto = Convert.ToDecimal(txtMonto.Text),
                        seguroVehiculo = Convert.ToDecimal(txtSoa.Text),
                        impuestoPropiedad = Convert.ToDecimal(txtImpPro.Text),
                        impuestoMunicipalidad = Convert.ToDecimal(txtImpMuni.Text),
                        timbreFS = Convert.ToDecimal(txtFauna.Text),
                        iva = Convert.ToDecimal(txtIva.Text),
                        aporteCSV = Convert.ToDecimal(txtAcsv.Text)
                    };

                    Marchamo marchamoActualizado = await marchamoManager.Update(marchamo, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(marchamoActualizado.idPlaca))
                    {
                        lblResultado.Text = $"Marchamo de placa {marchamoActualizado.idPlaca} actualizado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();

                        Correo correo = new Correo();
                        correo.Enviar("Servicio actualizado con exito", marchamoActualizado.idPlaca, "bgonzaleza003@gmail.com",
                            Convert.ToInt32(Session["CodigoUsuario"].ToString()));

                        ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al actualizar la operacion";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Maroon;
                    }
                }

            }catch(Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmMarchamo",
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

                int marchamoEliminado  = await marchamoManager.Delete( _codigo, Session["Token"].ToString());
                if (marchamoEliminado > 0)
                {
                    ltrModalMensaje.Text = "Marchamo eliminado";
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
                    Vista = "frmMarchamo",
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

                ltrTituloMantenimiento.Text = "Nuevo marchamo";
                txtPlaca.Enabled = true;
                btnAceptarMant.ControlStyle.CssClass = "btn btn-success";
                btnAceptarMant.Visible = true;
                ltridMarchamo.Visible = false;
                txtidMarchamo.Visible = false;
                txtidMarchamo.Text = string.Empty;
                txtPlaca.Text = string.Empty;
                txtMonto.Text = string.Empty;
                txtSoa.Text = string.Empty;
                txtImpPro.Text = string.Empty;
                txtImpMuni.Text = string.Empty;
                txtFauna.Text = string.Empty;
                txtIva.Text = string.Empty;
                txtAcsv.Text = string.Empty; ;
                lblResultado.Visible = false;

                ScriptManager.RegisterStartupScript(this,
                    this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
            }
            catch (Exception ex)
            {
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    FechaHora = DateTime.Now,
                    Vista = "frmMarchamo",
                    Accion = "btnNuevo_Click",
                    Fuente = ex.Source,
                    Numero = ex.HResult,
                    Descripcion = ex.Message
                };
                Error errorIngresado = await errorManager.Ingresar(error);

            }
        }

        protected void gvMarchamos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvMarchamos.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = $"Modificar marchamo de matricula {row.Cells[1].Text.Trim()}";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";              
                    txtidMarchamo.Text = row.Cells[0].Text.Trim();
                    txtidMarchamo.Visible = false;
                    ltridMarchamo.Visible = false;
                    txtPlaca.Text = row.Cells[1].Text.Trim();
                    txtPlaca.Enabled = false;
                    txtMonto.Text = string.Empty;
                    txtSoa.Text = string.Empty;
                    txtImpPro.Text = string.Empty;
                    txtImpMuni.Text = string.Empty;
                    txtFauna.Text = string.Empty;
                    txtIva.Text = string.Empty;
                    txtAcsv.Text = string.Empty;
                    btnAceptarMant.Visible = true;
                    lblResultado.Visible = false;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    _codigo = Convert.ToInt32(row.Cells[0].Text.Trim());
                    ltrModalMensaje.Text = $"Esta seguro que desea eliminar el marchamo ligado a la matricula {row.Cells[1].Text.Trim()}?";
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }

    }
}