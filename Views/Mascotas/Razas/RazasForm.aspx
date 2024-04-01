<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="RazasForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Mascotas.RazasForm" %>


<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <asp:HyperLink ID="btnNuevaRaza" runat="server" CssClass="btn btn-info btn-block" Text="Agregar raza" NavigateUrl="~/Views/Mascotas/Razas/AgregarRazaForm.aspx"></asp:HyperLink>
                <br />

                <h2>Lista de Razas</h2>


                <asp:GridView ID="GridViewRazas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewRazas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID_Raza" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_Raza" HeaderText="Nombre" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="EliminarRaza" CommandArgument='<%# Eval("ID_Raza") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                                <asp:Button runat="server" CommandName="ActualizarRaza" CommandArgument='<%# Eval("ID_Raza") %>' Text="Actualizar" CssClass="btn btn-primary btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
