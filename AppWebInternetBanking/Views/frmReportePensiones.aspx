<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReportePensiones.aspx.cs" Inherits="AppWebInternetBanking.Views.frmReportePensiones" %>
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
        $('[id*=gvPensiones]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            dom: 'Bfrtip',
            'aoColumnDefs': [{ 'bSortable': true, 'aTargets': [0] }],
            'iDisplayLength': 20,
            buttons: [
                { extend: 'copy', text: 'Copy to clipboard', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'excel', text: 'Export to Excel', className: 'exportExcel', filename: 'Pensiones_Excel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'csv', text: 'Export to CSV', className: 'exportExcel', filename: 'Pensiones_Csv', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'pdf', text: 'Export to PDF', className: 'exportExcel', filename: 'Pensiones_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
            ]
        });
    });
    </script>
    
    <h1>Bitacora de Traslado de Pensiones</h1>
    <asp:GridView ID="gvPensiones" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
       HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" >
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="TRAS_CODIGO" />
            <asp:BoundField HeaderText="Cliente" DataField="TRAS_CLIENTE" />
            <asp:BoundField HeaderText="Correo" DataField="TRAS_CLIENTE_CORREO" />
            <asp:BoundField HeaderText="Telefono" DataField="TRAS_CLIENTE_TELEFONO" />
            <asp:BoundField HeaderText="Entidad de Destino (ROP)" DataField="TRAS_ROP_DESTINO" />
            <asp:BoundField HeaderText="Entidad de Destino (FCL)" DataField="TRAS_FCL_DESTINO" />
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
                              label: "Cantidad de Traslados de FCL por Operadora de Pensiones",
                              backgroundColor: [<%= this.backgroundcolorsGrafico %>],
                            data: [<%= this.dataGrafico %>]
                          }]
                      },
                      options: {
                          title: {
                              display: true,
                              text: 'Cantidad de Traslados de FCL por Operadora de Pensiones'
                          }
                      }
                  });
              </script>
                </div>
            </div>
</asp:Content>
