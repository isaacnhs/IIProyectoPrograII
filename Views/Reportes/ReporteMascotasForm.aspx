<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ReporteMascotasForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Reportes.ReporteMascotasForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <h2>Reporte de Mascotas</h2>
        <asp:GridView ID="GridViewReporteMascotas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ID_Mascota" HeaderText="ID" />
                <asp:BoundField DataField="Nombre_Mascota" HeaderText="Nombre" />
                <asp:BoundField DataField="Raza" HeaderText="Raza" />
                <asp:BoundField DataField="TipoMascota" HeaderText="Tipo de Mascota" />
                <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" />
                <asp:BoundField DataField="Comida_Favorita" HeaderText="Comida Favorita" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>