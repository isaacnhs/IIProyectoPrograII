<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="MascotasForm.aspx.cs" Inherits="SistemaVeterinaria.Views.MascotasForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10">

                <asp:Button ID="btnNuevaMascota" runat="server" Text="Nueva Mascota" CssClass="btn btn-info btn-block mb-3" OnClick="btnNuevaMascota_Click" />

                <h2>Lista de Mascotas</h2>
                <asp:GridView ID="GridViewMascotas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewMascotas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID_Mascota" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_Mascota" HeaderText="Nombre" />
                        <asp:BoundField DataField="Raza" HeaderText="Raza" />
                        <asp:BoundField DataField="TipoMascota" HeaderText="Tipo de Mascota" />
                        <asp:BoundField DataField="Nombre_Cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="Comida_Favorita" HeaderText="Comida Favorita" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="EliminarMascota" CommandArgument='<%# Eval("ID_Mascota") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                                <asp:Button runat="server" CommandName="ActualizarMascota" CommandArgument='<%# Eval("ID_Mascota") %>' Text="Actualizar" CssClass="btn btn-primary btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
