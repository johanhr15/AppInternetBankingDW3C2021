<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMarchamo.aspx.cs" Inherits="AppWebInternetBanking.Views.frmMarchamo" %>

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
                $("#MainContent_gvMarchamos tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
    <h1>Mantenimiento de Marchamos</h1>
    <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvMarchamos" runat="server" AutoGenerateColumns="false"
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark"
        HeaderStyle-BackColor="#243054" HeaderStyle-ForeColor="White"
        AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="gvMarchamos_RowCommand">
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
            <asp:ButtonField HeaderText="Modificar" CommandName="Modificar"
                ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
            <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar"
                ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton type="Button" CssClass="btn btn-success" ID="btnNuevo" runat="server" OnClick="btnNuevo_Click"
        Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />


    <!--VENTANA DE MANTENIMIENTO -->
    <div id="myModalMantenimiento" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Literal ID="ltrTituloMantenimiento" runat="server"></asp:Literal></h4>
                </div>
                <div class="modal-body">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltridMarchamo" Text="Codigo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtidMarchamo" runat="server" Enabled="false" CssClass="form-control" /></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPlaca" Text="Placa" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexPlaca"
                                    ControlToValidate="txtPlaca" ValidationExpression="[a-zA-Z]{3,3}[0-9]{3,3}"
                                    ErrorMessage="Ingresa una placa con formato ABC123" ForeColor="Red" /></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMonto" Text="Monto" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexMonto"
                                    ControlToValidate="txtMonto" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el monto en formato decimal (0.0)" ForeColor="Red" /></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrSoa" Text="Seguro Obligatorio" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtSoa" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexSoa"
                                    ControlToValidate="txtSoa" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el SOA en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrAcsv" Text="Aporte Consejo Seguridad Vial" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtAcsv" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexAcsv"
                                    ControlToValidate="txtAcsv" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el aporte en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrImpPro" Text="Impuesto sobre propiedad" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtImpPro" runat="server" CssClass="form-control" /></td>

                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexImpPro"
                                    ControlToValidate="txtImpPro" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el impuesto en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrImpMuni" Text="Impuesto de municipalidad" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtImpMuni" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexImpMuni"
                                    ControlToValidate="txtImpMuni" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el impuesto en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrFauna" Text="Timbre Fauna Silvestre" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtFauna" runat="server" CssClass="form-control" /></td>
                             <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexFauna"
                                    ControlToValidate="txtFauna" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el timbre en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrIva" Text="IVA" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtIva" runat="server" CssClass="form-control" /></td>
                               <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexIva"
                                    ControlToValidate="txtIva" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el IVA en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>

                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" OnClick="btnAceptarMant_Click" CssClass="btn btn-success" ID="btnAceptarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
                    <asp:LinkButton type="button" OnClick="btnCancelarMant_Click" CssClass="btn btn-danger" ID="btnCancelarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
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
                    <h4 class="modal-title">Mantenimiento de Marchamos</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrModalMensaje" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" OnClick="btnAceptarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" />
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarModal" OnClick="btnCancelarModal_Click" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
                </div>
            </div>
        </div>
    </div>


        <div id="modalError" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrErrorMessage" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="LinkButton2" OnClick="btnCancelarModal_Click" 
                        runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
