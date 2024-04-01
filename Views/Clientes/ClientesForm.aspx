<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ClientesForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Clientes.ClientesForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <asp:HyperLink ID="linkAgregarCliente" runat="server" CssClass="btn btn-info btn-block" Text="Agregar cliente" NavigateUrl="~/Views/Clientes/AgregarClienteForm.aspx"></asp:HyperLink>

                <br />
                <br />

                <h2>Lista de Clientes</h2>

                <asp:GridView ID="GridViewClientes" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewClientes_RowCommand">
                    <columns>
                        <asp:BoundField DataField="ID_Cliente" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                        <asp:TemplateField HeaderText="Acciones">
                            <itemtemplate>
                                <asp:Button runat="server" CommandName="EliminarCliente" CommandArgument='<%# Eval("ID_Cliente") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                                <asp:Button runat="server" CommandName="ActualizarCliente" CommandArgument='<%# Eval("ID_Cliente") %>' Text="Actualizar" CssClass="btn btn-primary btn-sm" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
