<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteMarchamos.aspx.cs" Inherits="AppWebInternetBanking.Views.frmReporteMarchamos" %>
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
        $('[id*=gvMarchamos]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
            dom: 'Bfrtip',
            'aoColumnDefs': [{ 'bSortable': true, 'aTargets': [0] }],
            'iDisplayLength': 20,
            buttons: [
                { extend: 'copy', text: 'Copiar a portapapeles', className: 'exportExcel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'excel', text: 'Exportar a Excel', className: 'exportExcel', filename: 'Marchamos_Excel', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'csv', text: 'Exportar a CSV', className: 'exportExcel', filename: 'Marchamos_Csv', exportOptions: { modifier: { page: 'all' } } },
                { extend: 'pdf', text: 'Exportar a PDF', className: 'exportExcel', filename: 'Marchamos_Pdf', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
            ]
        });
    });
    </script>
    
    <h1>Bitacora de Marchamos</h1>
    <asp:GridView ID="gvMarchamos" runat="server" AutoGenerateColumns="false" CssClass="table table-dark table-striped"
       HeaderStyle-BackColor="#355B75" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#869CAC" >
        <Columns>
             <asp:BoundField HeaderText="Codigo" DataField="idMarchamo" />
            <asp:BoundField HeaderText="Placa" DataField="idPlaca" />
            <asp:BoundField HeaderText="Monto" DataField="monto" />
            <asp:BoundField HeaderText="Seguro Obligatorio" DataField="seguroVehiculo" />
            <asp:BoundField HeaderText="Aporte Consejo Seguridad Vial" DataField="aporteCSV" />
            <asp:BoundField HeaderText="Impuesto sobre propiedad" DataField="impuestoPropiedad" />
            <asp:BoundField HeaderText="Impuesto de municipalidad" DataField="impuestoMunicipalidad" />
            <asp:BoundField HeaderText="Timbre Fauna Silvestre" DataField="timbreFS" />
            <asp:BoundField HeaderText="IVA" DataField="iva" />
        </Columns>
    </asp:GridView>

     <div class="row">
            <div class="col-sm">
     <div id="canvas-holder" style=" text-align  width:40%  ">
		            <canvas id="vistas-chart"></canvas>
	            </div>
              <script >
                  new Chart(document.getElementById("vistas-chart"), {
                      type: 'bar',
                      data: {
                          labels: [<%= this.etiquetasGrafico %>],
                          datasets: [{
                              label: "Monto Marchamos por Placa",
                              backgroundColor: [<%= this.coloresGrafico %>],
                            data: [<%= this.informacionGrafico %>]
                          }]
                      },
                      options: {
                          title: {
                              display: true,
                              text: 'Monto Marchamos por Placa'
                          }
                      }
                  });
              </script>
                </div>
            </div>

</asp:Content>
