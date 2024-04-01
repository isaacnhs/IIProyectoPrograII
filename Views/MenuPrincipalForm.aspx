<%@ Page Language="C#" MasterPageFile="~/HeaderFooter.Master" AutoEventWireup="true" CodeBehind="MenuPrincipalForm.aspx.cs" Inherits="SistemaVeterinaria.Views.MenuPrincipalForm" %>

<asp:Content ID="ContenidoFormulario" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <br />
    <h1 class="text-center">¡Bienvenido al sistema!</h1>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Image ID="imgVeterinaria" runat="server" CssClass="img-fluid" ImageUrl="~/Assets/Img/vet.jpg" AlternateText="Imagn" />
            </div>
        </div>
    </div>
</asp:Content>

