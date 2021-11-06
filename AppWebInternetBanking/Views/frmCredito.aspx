<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCredito.aspx.cs" Inherits="AppWebInternetBanking.Views.frmCredito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">
        
       function openModal() {
                 $('#myModal').modal('show'); //ventana de mensajes
        }

        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }    

        function CloseModal() {
            $('#myModal').modal('hide');//cierra ventana de mensajes
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }

        $(document).ready(function () { //filtrar el datagridview
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvCreditos tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
     </script> 
    <h1>Mantenimiento de Creditos</h1>
    <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvCreditos" runat="server" AutoGenerateColumns="false"
      CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" 
        HeaderStyle-BackColor="#243054" HeaderStyle-ForeColor="White" 
        AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="gvCreditos_RowCommand" >
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="CRE_CODIGO" />
            <asp:BoundField HeaderText="Cliente" DataField="CRE_COD_CLIENTE" />
            <asp:BoundField HeaderText="Moneda" DataField="CRE_COD_MONEDA" />
            <asp:BoundField HeaderText="Entidad Financiera" DataField="CRE_BANCO" />
            <asp:BoundField HeaderText="Plazos" DataField="CRE_PLAZO" />
            <asp:BoundField HeaderText="Fecha Inicial" DataField="CRE_INICIO" />
            <asp:BoundField HeaderText="Monto del Credito" DataField="CRE_MONTO" />
            <asp:BoundField HeaderText="Ingresos Netos" DataField="CRE_INGRESOS" />
            <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" 
                ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
            <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar"
                ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton type="Button" CssClass="btn btn-success" ID="btnNuevo" runat="server" OnClick="btnNuevo_Click"
      Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo"    />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />


      <!--VENTANA DE MANTENIMIENTO -->
  <div id="myModalMantenimiento" class="modal fade" role="dialog">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><asp:Literal ID="ltrTituloMantenimiento" runat="server"></asp:Literal></h4>
      </div>
      <div class="modal-body">
          <table style="width: 100%;">
              <tr>
                  <td><asp:Literal ID="ltrCodigoMant" Text="Codigo del Credito" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCodigoUsuario" Text="Cliente" runat="server" /></td>
                  <td><asp:DropDownList ID="ddlCodigoUsuario" CssClass="form-control" runat="server"> 
                  </asp:DropDownList></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCodigoMoneda" Text="Moneda" runat="server" /></td>
                  <td><asp:DropDownList ID="ddlCodigoMoneda" CssClass="form-control" runat="server"> 
                  </asp:DropDownList></td>
              </tr>
               <tr>
                    <td><asp:Literal ID="ltrEntidadFinanciera" Text="Entidad Financiera" runat="server" /></td>
                    <td><asp:DropDownList ID="ddlEntidadFinanciera" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True" Value="BCR">Banco de Costa Rica</asp:ListItem>
                        <asp:ListItem Value="BAC">BAC</asp:ListItem>
                        <asp:ListItem Value="BN">Banco Nacional</asp:ListItem>
                        <asp:ListItem Value="BNP">Banco Popular</asp:ListItem>
                    </asp:DropDownList></td>
               </tr>
              <tr>
                    <td><asp:Literal ID="ltrPlazos" Text="Numero de Meses Plazo" runat="server"></asp:Literal></td>
                    <td><asp:TextBox ID="txtPlazos" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtPlazos" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                </tr>
              <tr>
                    <td><asp:Literal ID="ltrFechaInicial" Text="Fecha Inicial del Credito" runat="server" /></td>
                    <td><asp:TextBox TextMode="DateTime" ID="txtFecha" runat="server" CssClass="form-control"></asp:TextBox></td>
              </tr>
              <tr>
                    <td><asp:Literal ID="ltrMontoCredito" Text="Monto Total del Credito" runat="server"></asp:Literal></td>
                    <td><asp:TextBox ID="txtMontoCredito" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtMontoCredito" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td><asp:Literal ID="ltrIngresos" Text="Ingreos Netos Mensuales" runat="server"></asp:Literal></td>
                    <td><asp:TextBox ID="txtIngresos" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtIngresos" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                </tr>
          </table>
          <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
      </div>
      <div class="modal-footer">
        <asp:LinkButton type="button" OnClick="btnAceptarMant_Click" CssClass="btn btn-success" ID="btnAceptarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button" OnClick="btnCancelarMant_Click"  CssClass="btn btn-danger" ID="btnCancelarMant"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>

    
     <!-- VENTANA MODAL -->
  <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de Creditos</h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal id="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarModal" OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
