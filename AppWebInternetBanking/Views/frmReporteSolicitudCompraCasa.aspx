<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteSolicitudCompraCasa.aspx.cs" Inherits="AppWebInternetBanking.Views.frmReporteSolicitudCompraCasa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $('[id*=gvSolicitudes]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                'aoColumnDefs': [{ 'bSortable': true, 'aTargets': [0] }],
                'iDisplayLength': 20,
                buttons: [
                    { extend: 'copy', text: 'Copy to clipboard', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'excel', text: 'Export to Excel', className: 'exportExcel', filename: 'SolicitudCompraCasa_Excel', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'csv', text: 'Export to CSV', className: 'exportExcel', filename: 'SolicitudCompraCasa_Csv', exportOptions: { modifier: { page: 'all' } } },
                    { extend: 'pdf', text: 'Export to PDF', className: 'exportExcel', filename: 'SolicitudCompraCasa_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
                ]
            });
        });
    </script>

    <h1><asp:Label Text="Mantenimiento de solicitud de compra de casa" runat="server"></asp:Label></h1>
    <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
       HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" >
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
    </Columns>
    </asp:GridView>

    <div class="row">
            <div class="col-sm">
     <div id="canvas-holder" style="width:40%">
		            <canvas id="vistas-chart"></canvas>
	            </div>
              <script >
                  new Chart(document.getElementById("vistas-chart"), {
                      type: 'pie',
                      data: {
                          labels: [<%= this.labelsGrafico %>],
                          datasets: [{
                              label: "Cantidad de solicitudes por tipo de Casa.",
                              backgroundColor: [<%= this.backgroundcolorsGrafico %>],
                            data: [<%= this.dataGrafico %>]
                          }]
                      },
                      options: {
                          title: {
                              display: true,
                              text: 'Cantidad de solicitudes por tipo de Casa.'
                          }
                      }
                  });
              </script>
                </div>
            </div>
</asp:Content>
