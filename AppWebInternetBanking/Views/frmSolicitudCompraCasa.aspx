<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSolicitudCompraCasa.aspx.cs" Inherits="AppWebInternetBanking.Views.frmSolicitudCompraCasa" %>
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
                $("#MainContent_gvServicios tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script> 

    <h1><asp:Label Text="Mantenimiento de solicitud de compra de casa" runat="server"></asp:Label></h1>
    <input id="myInput" Placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvSolicitudCompraCasa" OnRowCommand="gvSolicitudCompraCasa_RowCommand" runat="server" AutoGenerateColumns="False" 
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" HeaderStyle-BackColor="#243054"
        HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" Width="100%">
    <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
        <asp:BoundField HeaderText="Codigo Usuario" DataField="CodigoUsuario" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Codigo Moneda" DataField="CodigoMoneda" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="TipoCasa" DataField="TipoCasa" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="TasaInteres" DataField="TasaInteres" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="ValorCasa" DataField="ValorCasa" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Prima" DataField="Prima" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="PlazoMeses" DataField="PlazoMeses" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
         <asp:BoundField HeaderText="FechaInicio" DataField="FechaInicio" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:BoundField HeaderText="Estado" DataField="Estado" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
        <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
        <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
    </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
    <!-- VENTANA MODAL -->
  <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Mantenimiento de solicitud de compra de casa</h4>
      </div>
      <div class="modal-body">
        <p><asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" /></p>
      </div>
      <div class="modal-footer">
         <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click"  runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarModal" OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
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
                  <td><asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoMant" runat="server" TextMode="Number" Enabled="false" CssClass="form-control" /></td>
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
                  <td><asp:Literal ID="ltrTipoCasa" Text="Tipo Casa" runat="server" /></td>
                  <td><asp:TextBox ID="txtTipoCasa" runat="server" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrTasaInteres" Text="Tasa de Interes" runat="server" /></td>
                  <td><asp:TextBox ID="txtTasaInteres" runat="server" TextMode="Number" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrValorCasa" Text="Valor Casa" runat="server" /></td>
                  <td><asp:TextBox ID="txtValorCasa" runat="server" TextMode="Number" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrPrima" Text="Prima" runat="server" /></td>
                  <td><asp:TextBox ID="txtPrima" runat="server" TextMode="Number" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrPlazoMeses" Text="Plazo Meses" runat="server" /></td>
                  <td><asp:TextBox ID="txtPlazoMeses" runat="server" TextMode="Number" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrFechaInicio" Text="Fecha Inicio" runat="server" /></td>
                  <td><asp:TextBox ID="txtFechaInicio" TextMode="DateTimeLocal" runat="server" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal Text="Estado" runat="server" /></td>
                  <td> <asp:DropDownList ID="ddlEstado"  CssClass="form-control" runat="server">
                    <asp:ListItem Value="A">Aprovada</asp:ListItem>
                    <asp:ListItem Value="D">Denegada</asp:ListItem>
                </asp:DropDownList></td>
              </tr>
          </table>
          <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
      </div>
      <div class="modal-footer">
        <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" OnClick="btnAceptarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
         <asp:LinkButton type="button"  CssClass="btn btn-danger" ID="btnCancelarMant" OnClick="btnCancelarMant_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
      </div>
    </div>
  </div>
</div>
</asp:Content>
