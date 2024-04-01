<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="AgregarTipoMascotaForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Mascotas.TiposMascota.AgregarTipoMascota" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <h2>Agregar Nuevo Tipo de Mascota</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombreTipoMascota">Nombre:</label>
                            <asp:TextBox ID="txtNombreTipoMascota" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnGuardarTipoMascota" runat="server" Text="Guardar" CssClass="btn btn-info btn-block" OnClick="btnGuardarTipoMascota_Click" />
                    </div>
                </div>
            </div>
    </div>
    </div>
</asp:Content>
