<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ActualizarRazaForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Mascotas.ActualizarRazaForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <h2>Actualizar Raza</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombre">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-info btn-block" OnClick="btnActualizar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
