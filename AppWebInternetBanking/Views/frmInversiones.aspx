<%@ Page Async ="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmInversion.aspx.cs" Inherits="AppWebInternetBanking.Views.frmInversiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

        
     <script> function openModal() {
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
                $("#MainContent_gvInversion tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        }); </script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.2/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.2.2/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('[id*=gvInversion]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                'aoColumnDefs': [{ 'bSortable': true, 'aTargets': [0] }],
                'iDisplayLength': 20,
                buttons: [
                    { extend: 'copy', text: 'Copy to clipboard', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'excel', text: 'Export to Excel', className: 'exportExcel', filename: 'Inversiones_Excel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'csv', text: 'Export to CSV', className: 'exportExcel', filename: 'Inversiones_Csv', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'pdf', text: 'Export to PDF', className: 'exportExcel', filename: 'Inversiones_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
                ]
            });
        });
        
    </script> 

    <div class="container">
    <h1>Mantenimiento de Inversion</h1>
    <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvInversion" OnRowCommand="gvInversion_RowCommand" runat="server" AutoGenerateColumns="false"
      CssClass="table table-stripped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-CssClass="navy" 
        HeaderStyle-BackColor="#243054" HeaderStyle-ForeColor="White" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="CuentaOrigen" DataField="CuentaOrigen" />
            <asp:BoundField HeaderText="FondosInversion" DataField="FondosInversion" />
            <asp:BoundField HeaderText="Plazo" DataField="Plazo" />
            <asp:BoundField HeaderText="Moneda" DataField="CodigoMoneda" />
            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
            <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" 
                ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
            <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar"
                ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton type="Button" CssClass="btn btn-success" ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" 
      Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
</div>

    <!-- GRAFICO -->
    <div class="row">
            <div class="col-sm">
     <div id="canvas-holder" style="width:40%">
		            <canvas id="vistas-chart"></canvas>
	            </div>
              <script >
                  new Chart(document.getElementById("vistas-chart"), {
                      type: 'bar',
                      data: {
                          labels: [<%= this.labelsGrafico %>],
                          datasets: [{
                              label: "Fondos Inversion",
                              backgroundColor: [<%= this.backgroundcolorsGrafico %>],
                              data: [<%= this.dataGrafico %>]
                          }]
                      },
                      options: {
                          title: {
                              display: true,
                              text: 'Grafico Inversion'
                          }
                      }
                  });
              </script>
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
                  <td><asp:Literal ID="ltrCodigoMant" Text="Codigo de la inversion" runat="server" /></td>
                  <td><asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
              </tr>
              <tr>
                  <td><asp:Literal ID="ltrCuentaOrigen" Text="CuentaOrigen" runat="server" /></td>
                  <td><asp:DropDownList ID="ddlCuentaOrigen" CssClass="form-control" runat="server"> 
                  </asp:DropDownList></td>
                     <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="ddlCuentaOrigen" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                
              </tr> 
                <tr>
                    <td><asp:Literal ID="ltrFondosInversion" Text="FondosInversion" runat="server" /></td>
                    <td><asp:TextBox ID="txtFondosInversion" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtFondosInversion" EnableClientScript="False"></asp:RequiredFieldValidator></td>

                </tr>
                <tr>
                    <td><asp:Literal ID="ltrPlazo" Text="Plazo" runat="server" /></td>
                    <td><asp:TextBox ID="txtPlazo"  runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtPlazo" EnableClientScript="False"></asp:RequiredFieldValidator></td>

                </tr>
               <tr>
                    <td><asp:Literal ID="ltrCodigoMoneda" Text="CodigoMoneda" runat="server" /></td>
                    <td><asp:DropDownList ID="ddlCodigoMoneda"  runat="server" CssClass="form-control"></asp:DropDownList></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="ddlCodigoMoneda" EnableClientScript="False"></asp:RequiredFieldValidator></td>

                </tr>
              <tr>
                    <td><asp:Literal ID="ltrMonto" Text="Monto" runat="server" /></td>
                    <td><asp:TextBox ID="txtMonto"  runat="server" CssClass="form-control"></asp:TextBox></td>
                    <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ErrorMessage="*Espacio Obligatorio*" ControlToValidate="txtMonto" EnableClientScript="False"></asp:RequiredFieldValidator></td>

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