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
        IEnumerable<Ahorro> ahorro = new ObservableCollection<Ahorro>();
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
                ahorro = await ahorroManager.ObtenerAhorro(Session["Token"].ToString());
                gvAhorro.DataSource = ahorro.ToList();
                gvAhorro.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar los ahorros. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }

        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
            {
                try

                {
                    Ahorro datos = new Ahorro()
                    {
                        CuentaOrigen = Convert.ToInt32(txtCuentaOrigen.Text),
                        Monto = Convert.ToInt32(txtMontoA.Text),
                        Plazo = Convert.ToDecimal(txtPlazo.Text),
                        TipoAhorro = txtTipoAhorro.Text
                    };

                    Ahorro respuestaAhorro = await ahorroManager.Ingresar(datos, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(respuestaAhorro.TipoAhorro))
                    {
                        lblResultado.Text = "Ahorro ingresado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        InicializarControles();

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

                    Ahorro ahorro = new Ahorro()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        CuentaOrigen = Convert.ToInt32(txtCuentaOrigen.Text),
                        Monto = Convert.ToInt32(txtMontoA.Text),
                        Plazo = Convert.ToDecimal(txtPlazo.Text),
                        TipoAhorro = txtTipoAhorro.Text
                    };


                    Ahorro respuestaAhorro = await ahorroManager.Actualizar(ahorro, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(respuestaAhorro.TipoAhorro))
                    {
                        lblResultado.Text = "Ahorro modificado con exito";
                        lblResultado.Visible = true;
                        lblResultado.ForeColor = Color.Green;
                        btnAceptarMant.Visible = false;
                        InicializarControles();

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
                catch (Exception exc)
                {
                    lblStatus.Text = "Hubo un error en la operacion. " + exc.Message;
                    lblStatus.Visible = true;
                }
            }
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await ahorroManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Solicitud de Ahorro eliminada";
                    btnAceptarModal.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
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

        protected void gvAhorro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvAhorro.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Ahorro";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtCuentaOrigen.Text = fila.Cells[1].Text;
                    txtMontoA.Text = fila.Cells[2].Text;
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigoMant.Text = string.Empty;
            txtCuentaOrigen.Text = string.Empty;
            txtMontoA.Text = string.Empty;
            txtPlazo.Text = string.Empty;
            txtTipoAhorro.Text = string.Empty;
            ltrTituloMantenimiento.Text = "Nuevo Ahorro";
            lblResultado.Text = String.Empty;

            LimpiarControles();


            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        protected void btnCancelarMant_Click1(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() { CloseMantenimiento(); });", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }
    }

}

