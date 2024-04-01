<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="CitasForm.aspx.cs" Inherits="SistemaVeterinaria.Views.CitasForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <h2>Crear Cita</h2>
        <div class="form-group">
            <label for="ddlMascota">Mascota:</label>
            <asp:DropDownList ID="ddlMascota" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ddlMedico">Médico:</label>
            <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtFecha">Fecha de la Cita:</label>
            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
        </div>

        <asp:Button ID="btnCrearCita" runat="server" Text="Crear Cita" CssClass="btn btn-info btn-block" OnClick="btnCrearCita_Click" />
    </div>
</asp:Content>
