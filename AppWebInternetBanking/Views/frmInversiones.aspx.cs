﻿using AppWebInternetBanking.Controllers;
using AppWebInternetBanking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AppWebInternetBanking.Views
{
    public partial class frmInversiones : System.Web.UI.Page
    {
        IEnumerable<Inversion> inversion = new ObservableCollection<Inversion>();
        InversionManager inversionManager = new InversionManager();
        IEnumerable<Cuenta> cuentas = new ObservableCollection<Cuenta>();
        CuentaManager cuentaManager = new CuentaManager();
        IEnumerable<Moneda> monedas = new ObservableCollection<Moneda>();
        MonedaManager monedaManager = new MonedaManager();
        static string _codigo = string.Empty;

        public string labelsGrafico = string.Empty;
        public string backgroundcolorsGrafico = string.Empty;
        public string dataGrafico = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    InicializarControles();
                    ObtenerDatosgrafico();
                }
            }
        }
        private async void ObtenerDatosgrafico()
        {
            StringBuilder labels = new StringBuilder();
            StringBuilder data = new StringBuilder();
            StringBuilder backgroundColors = new StringBuilder();

            var random = new Random();
            List<string> colores = new List<string>();
            colores.Add("red");
            colores.Add("orange");
            colores.Add("blue");
            colores.Add("green");
            colores.Add("pink");
            colores.Add("purple");
            colores.Add("gray");
            colores.Add("yellow");
            colores.Add("black");
            colores.Add("white");

            int i = 0;

            inversion = await inversionManager.ObtenerInversion(Session["Token"].ToString());

            foreach (var inversiones in inversion.GroupBy(e => e.FondosInversion).
                Select(group => new
                {
                    FondosInversion = group.Key,
                    Cantidad = group.Count()
                }).OrderBy(x => x.FondosInversion))
            {
                //string color = String.Format("#{0:X6}", random.Next(0x1000000));
                labels.Append(string.Format("'{0}',", inversiones.FondosInversion));
                data.Append(string.Format("'{0}',", inversiones.Cantidad));
                backgroundColors.Append(string.Format("'{0}',", colores.ElementAt(i)));

                labelsGrafico = labels.ToString().Substring(0, labels.Length - 1);
                dataGrafico = data.ToString().Substring(0, data.Length - 1);
                backgroundcolorsGrafico = backgroundColors.ToString().Substring(0, backgroundColors.Length - 1);

                i++;
            }
        }

        private async void InicializarControles()
        {
            try
            {
                inversion = await inversionManager.ObtenerInversion(Session["Token"].ToString());
                gvInversion.DataSource = inversion.ToList();
                gvInversion.DataBind();
            }
            catch (Exception)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios";
                lblStatus.Visible = true;
            }

            try
            {
                cuentas = await cuentaManager.ObtenerCuentas(Session["Token"].ToString());
                ddlCuentaOrigen.DataSource = cuentas.ToList();
                ddlCuentaOrigen.DataBind();
                ddlCuentaOrigen.DataTextField = "IBAN";
                ddlCuentaOrigen.DataValueField = "Codigo";
                ddlCuentaOrigen.DataBind();
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

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
            {
                try
                {
                    Inversion inversiones = new Inversion()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        CuentaOrigen = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                        FondosInversion = txtFondosInversion.Text,
                        Plazo = txtPlazo.Text,
                        CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                        Cantidad = Convert.ToInt32(txtMonto.Text)

                    };
                    Inversion inversionIngresada = await inversionManager.Ingresar(inversiones, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(inversionIngresada.FondosInversion))
                    {
                        lblResultado.Text = "Solicitud de inversion ingresada con exito";
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
                catch
                {
                    lblResultado.Text = "Hubo un error al efectuar la operacion";
                    lblResultado.Visible = true;
                    lblResultado.ForeColor = Color.Maroon;
                }
            }

            else // MODIFICAR
            {
                try
                {
                    Inversion inversion = new Inversion()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        CuentaOrigen = Convert.ToInt32(ddlCuentaOrigen.SelectedValue),
                        FondosInversion = txtFondosInversion.Text,
                        Plazo = txtPlazo.Text,
                        CodigoMoneda = Convert.ToInt32(ddlCodigoMoneda.SelectedValue),
                        Cantidad = Convert.ToInt32(txtMonto.Text)
                    };


                    Inversion respuestaInversion = await inversionManager.Actualizar(inversion, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(respuestaInversion.FondosInversion))
                    {
                        lblResultado.Text = "Inversion modificada con exito";
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
            foreach (Control item in Page.Controls)
            {
                foreach (Control hijo in item.Controls)
                {
                    if (hijo is TextBox)
                        ((TextBox)hijo).Text = string.Empty;

                }
            }
        }

        protected void gvInversion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvInversion.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar Inversion";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    ddlCuentaOrigen.SelectedValue = fila.Cells[1].Text;
                    txtFondosInversion.Text = fila.Cells[2].Text;
                    txtPlazo.Text = fila.Cells[3].Text;
                    ddlCodigoMoneda.SelectedValue = fila.Cells[4].Text;
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
                resultado = await inversionManager.Eliminar(lblCodigoEliminar.Text, Session["Token"].ToString());
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
                ErrorManager errorManager = new ErrorManager();
                Error error = new Error()
                {
                    CodigoUsuario =
                    Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    Vista = "frmInversion.aspx",
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
    }
}