<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ActualizarClienteForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Clientes.ActualizarClienteForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-12">
                <h2>Actualizar Cliente</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombre">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtTelefono">Teléfono:</label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtDireccion">Dirección:</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Cliente" CssClass="btn btn-info btn-block" OnClick="btnActualizar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

