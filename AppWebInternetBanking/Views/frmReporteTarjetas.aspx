<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteTarjetas.aspx.cs" Inherits="AppWebInternetBanking.Views.frmReporteTarjetas" %>
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
        $('[id*=gvTarjetas]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
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
    
    <h1>Tarjetas</h1>
    <asp:GridView ID="gvTarjetas" runat="server" AutoGenerateColumns="false" CssClass="table table-sm"
       HeaderStyle-BackColor="Navy" HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="LightBlue" >
        <Columns>
        <asp:BoundField HeaderText="Codigo" DataField="idSolTarjeta" />
            <asp:BoundField HeaderText="Cliente" DataField="cedula" />
            <asp:BoundField HeaderText="Fecha Nacimiento" DataField="fechaNacimiento" />
            <asp:BoundField HeaderText="Ingreso Mensual" DataField="ingresoMensual" />
            <asp:BoundField HeaderText="Condicion laboral" DataField="condicionLaboral" />
            <asp:BoundField HeaderText="Tipo Tarjeta" DataField="idTipoTarjeta" />
            <asp:BoundField HeaderText="Nombre de empresa" DataField="nombreEmpresa" />
            <asp:BoundField HeaderText="Telefono de trabajo" DataField="telefonoTrabajo" />
            <asp:BoundField HeaderText="Puesto" DataField="puesto" />
            <asp:BoundField HeaderText="Tiempo de laborar" DataField="tiempoLaborar" />
            <asp:BoundField HeaderText="Telefono de contacto" DataField="telefonoContacto" />
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
                          labels: [<%= this.etiquetasGrafico %>],
                          datasets: [{
                              label: "Tipos de tarjeta",
                              backgroundColor: [<%= this.coloresGrafico %>],
                            data: [<%= this.informacionGrafico %>]
                          }]
                      },
                      options: {
                          title: {
                              display: true,
                              text: 'Tipos de tarjeta'
                          }
                      }
                  });
              </script>
                </div>
            </div>














</asp:Content>
