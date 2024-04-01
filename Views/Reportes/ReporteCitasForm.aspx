<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ReporteCitasForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Reportes.ReporteCitasForm" %>


<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
        <div class="container mt-5">
            <h2>Reporte de Control de Citas</h2>
            <asp:GridView ID="GridViewCitas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="NombreMascota" HeaderText="Nombre de la Mascota" />
                    <asp:BoundField DataField="ProximaFecha" HeaderText="Próxima Fecha" />
                    <asp:BoundField DataField="MedicoAsignado" HeaderText="Médico Asignado" />
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
