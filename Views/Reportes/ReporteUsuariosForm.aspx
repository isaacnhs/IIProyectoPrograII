<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ReporteUsuariosForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Reportes.ReporteUsuariosForm" %>


<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <h2>Reporte de Usuarios</h2>
        <asp:GridView ID="GridViewUsuarios" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Login_Usuario" HeaderText="ID" />
                <asp:BoundField DataField="Nombre_Usuario" HeaderText="Nombre" />
                <asp:BoundField DataField="Clave_Usuario" HeaderText="Clave" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
