<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="AgregarClienteForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Clientes.AgregarClienteForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <h2 class="text-center">Formulario de Cliente</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombre">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre del cliente"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtTelefono">Teléfono:</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese el teléfono del cliente"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtDireccion">Dirección:</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ingrese la dirección del cliente"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-info btn-block" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
