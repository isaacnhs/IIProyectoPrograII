<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="ActualizarMedicoForm.aspx.cs" Inherits="SistemaVeterinaria.Views.Medicos.ActualizarMedicoForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <h2>Actualizar Médico</h2>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="txtNombre">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtEspecialidad">Especialidad:</label>
                            <asp:TextBox ID="txtEspecialidad" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Médico" CssClass="btn btn-info btn-block" OnClick="btnActualizar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
