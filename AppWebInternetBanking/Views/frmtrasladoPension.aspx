<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmtrasladoPension.aspx.cs" Inherits="AppWebInternetBanking.Views.frmtrasladoPension" %>
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
                $("#MainContent_gvtrasladoPensiones tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
     </script> 
    <h1>Mantenimiento de Traslado de Afiliacion a Pensiones</h1>
    <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvtrasladoPensiones" runat="server" AutoGenerateColumns="false"
      CssClass="table table-sm" HeaderStyle-CssClass="thead-dark" 
        HeaderStyle-BackColor="#243054" HeaderStyle-ForeColor="White" 
        AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="gvtrasladoPensiones_RowCommand" >
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="TRAS_CODIGO" />
            <asp:BoundField HeaderText="Cliente" DataField="TRAS_CLIENTE" />
            <asp:BoundField HeaderText="Correo" DataField="TRAS_CLIENTE_CORREO" />
            <asp:BoundField HeaderText="Telefono" DataField="TRAS_CLIENTE_TELEFONO" />
            <asp:BoundField HeaderText="Entidad de Destino (ROP)" DataField="TRAS_ROP_DESTINO" />
            <asp:BoundField HeaderText="Entidad de Destino (FCL)" DataField="TRAS_FCL_DESTINO" />
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
                  <td><asp:Literal ID="ltrCodigoMant" Text="Codigo del Traslado" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCodigoUsuario" Text="Cliente" runat="server" /></td>
                  <td><asp:DropDownList ID="ddlCodigoUsuario" CssClass="form-control" runat="server"> 
                  </asp:DropDownList></td>
              </tr>
                <tr>
                    <td><asp:Literal ID="ltrCorreo" Text="Correo" runat="server" /></td>
                    <td><asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtCorreo" EnableClientScript="False"></asp:RequiredFieldValidator></td>

                </tr>
                <tr>
                    <td><asp:Literal ID="ltrTelefono" Text="Telefono" runat="server" /></td>
                    <td><asp:TextBox ID="txtTelefono" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtTelefono" EnableClientScript="False"></asp:RequiredFieldValidator></td>

                </tr>
               <tr>
                    <td><asp:Literal ID="ltrTRAS_ROP_DESTINO" Text="Entidad de Destino (ROP)" runat="server" /></td>
                    <td><asp:DropDownList ID="ddlTRAS_ROP_DESTINO" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True" Value="BCR">BCR Pensiones</asp:ListItem>
                        <asp:ListItem Value="CCSS">CCSS OP</asp:ListItem>
                        <asp:ListItem Value="BN">BN Vital</asp:ListItem>
                        <asp:ListItem Value="BNP">Popular Pensiones</asp:ListItem>
                    </asp:DropDownList></td>
               </tr>
              <tr>
                    <td><asp:Literal ID="ltrTRAS_FCL_DESTINO" Text="Entidad de Destino (ROP)" runat="server" /></td>
                    <td><asp:DropDownList ID="ddlTRAS_FCL_DESTINO" CssClass="form-control" runat="server">
                        <asp:ListItem Selected="True" Value="BCR">BCR Pensiones</asp:ListItem>
                        <asp:ListItem Value="CCSS">CCSS OP</asp:ListItem>
                        <asp:ListItem Value="BN">BN Vital</asp:ListItem>
                        <asp:ListItem Value="BNP">Popular Pensiones</asp:ListItem>
                    </asp:DropDownList></td>
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
        <h4 class="modal-title">Mantenimiento de Traslado de Afiliacion a Pensiones</h4>
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
