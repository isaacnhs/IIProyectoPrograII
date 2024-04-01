<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="MedicosForm.aspx.cs" Inherits="SistemaVeterinaria.Views.MedicosForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-10">

                <asp:HyperLink ID="btnNuevoMedico" runat="server" CssClass="btn btn-info btn-block" Text="Agregar médico" NavigateUrl="~/Views/Medicos/AgregarMedicoForm.aspx"></asp:HyperLink>

                <br />
                <br />

                <h2>Lista de Médicos</h2>

                <asp:GridView ID="GridViewMedicos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowCommand="GridViewMedicos_RowCommand">
                    <columns>
                        <asp:BoundField DataField="ID_Medico" HeaderText="ID" />
                        <asp:BoundField DataField="Nombre_Medico" HeaderText="Nombre" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" />
                        <asp:TemplateField HeaderText="Acciones">
                            <itemtemplate>
                                <asp:Button runat="server" CommandName="EliminarMedico" CommandArgument='<%# Eval("ID_Medico") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" />
                                <asp:Button runat="server" CommandName="ActualizarMedico" CommandArgument='<%# Eval("ID_Medico") %>' Text="Actualizar" CssClass="btn btn-primary btn-sm" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
