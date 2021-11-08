<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmSolTarjetaCredito.aspx.cs" Inherits="AppWebInternetBanking.Views.frmSolTarjetaCredito" %>

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
                $("#MainContent_gvSolicitudes tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
    <h1>Mantenimiento de Solitudes de Tarjetas de Credito</h1>
    <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
    <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="false"
        CssClass="table table-sm" HeaderStyle-CssClass="thead-dark"
        HeaderStyle-BackColor="#243054" HeaderStyle-ForeColor="White"
        AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="gvSolicitudes_RowCommand" OnRowDataBound="OnRowDataBound">
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
                                <asp:Literal ID="ltrIdSolTarjeta" Text="Codigo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtIdSolTarjeta" runat="server" Enabled="false" CssClass="form-control" /></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCedula" Text="Cliente" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control" /></td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrfechaNacimiento" Text="Fecha Nacimiento" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtfechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" /></td>
                 


                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrIngresoMensual" Text="Ingreso mensual" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtIngresoMensual" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexIngresoMensual"
                                    ControlToValidate="txtIngresoMensual" ValidationExpression="\d+([,\.]\d{1,2})?"
                                    ErrorMessage="Ingresa el ingreso en formato decimal (0.0)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal Text="Condicion Laboral" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlCondicionLaboral" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">Asalariado</asp:ListItem>
                                    <asp:ListItem Value="2">Independiente</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal Text="Tipo de tarjeta" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlTipoTarjeta" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">Millas</asp:ListItem>
                                    <asp:ListItem Value="2">Puntos</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrNombreEmpresa" Text="Nombre empresa" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexNombreEmpresa"
                                    ControlToValidate="txtNombreEmpresa" ValidationExpression="^[a-zA-Z. \w]{3,30}$"
                                    ErrorMessage="Ingrese el nombre de la empresa con un minimo de 3 caracteres y maximo 30 caracteres" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrTelefonoTrabajo" Text="Telefono trabajo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtTelefonoTrabajo" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexTelefonoTrabajo"
                                    ControlToValidate="txtTelefonoTrabajo" ValidationExpression="^[0-9]{1,8}"
                                    ErrorMessage="Solo se aceptan numeros de 8 digitos" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPuesto" Text="Puesto" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtPuesto" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="rexPuesto"
                                    ControlToValidate="txtPuesto" ValidationExpression="^[a-zA-Z. \w]{3,50}$"
                                    ErrorMessage="Ingrese el puesto con un minimo de 3 caracteres y maximo 50 caracteres" ForeColor="Red" /></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal Text="Tiempo de laborar" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlTiempoLaborar" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">menor 1 año</asp:ListItem>
                                    <asp:ListItem Value="1">1 año</asp:ListItem>
                                    <asp:ListItem Value="2">2 años</asp:ListItem>
                                    <asp:ListItem Value="3">3 años o mas</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Literal ID="ltrTelefonoContacto" Text="Telefono contacto" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtTelefonoContacto" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
                                    ControlToValidate="txtTelefonoContacto" ValidationExpression="^[0-9]{1,8}"
                                    ErrorMessage="Solo se aceptan numeros de 8 digitos" ForeColor="Red" /></td>
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
                    <h4 class="modal-title">Mantenimiento de Solitudes de Tarjetas de Credito</h4>
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
</asp:Content>
