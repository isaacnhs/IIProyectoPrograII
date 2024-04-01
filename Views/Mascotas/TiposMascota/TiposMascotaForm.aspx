<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="TiposMascotaForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Mascotas.TiposMascota.TiposMascota" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <asp:Button ID="btnNuevoTipoMascota" runat="server" Text="Nuevo Tipo de Mascota" CssClass="btn btn-info btn-block mb-3" OnClick="btnNuevoTipoMascota_Click" />
                <br />
                <h2>Lista de Tipos de Mascotas</h2>

                <asp:GridView ID="GridViewTiposMascotas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewTiposMascotas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID_TipoMascota" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_TipoMascota" HeaderText="Nombre" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button runat="server" CommandName="EliminarTipoMascota" CommandArgument='<%# Eval("ID_TipoMascota") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                                <asp:Button runat="server" CommandName="ActualizarTipoMascota" CommandArgument='<%# Eval("ID_TipoMascota") %>' Text="Actualizar" CssClass="btn btn-primary btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
