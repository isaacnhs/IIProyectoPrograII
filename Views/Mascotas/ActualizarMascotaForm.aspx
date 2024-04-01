<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ActualizarMascotaForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Mascotas.ActualizarMascotaForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <h2>Actualizar Mascota</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombreMascota">Nombre:</label>
                            <asp:TextBox ID="txtNombreMascota" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="ddlRaza">Raza:</label>
                            <asp:DropDownList ID="ddlRaza" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlTipoMascota">Tipo de Mascota:</label>
                            <asp:DropDownList ID="ddlTipoMascota" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlCliente">Cliente:</label>
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtComidaFavorita">Comida Favorita:</label>
                            <asp:TextBox ID="txtComidaFavorita" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnActualizarMascota" runat="server" Text="Actualizar" CssClass="btn btn-info btn-block" OnClick="btnActualizarMascota_Click" />
                    </div>
                </div>
            </div>
    </div>
</asp:Content>
