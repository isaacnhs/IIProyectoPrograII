<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="SistemaVeterinaria.Views.LoginForm" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login Form</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-6">
                <h1 class="text-center">Sistema de veterinaria</h1>
                <br />
                <div class="card">
                    <div class="card-header text-center">Login</div>
                    <div class="card-body">
                        <form id="formLogin" runat="server">
                            <div class="form-group">
                                <label for="txtUsuario">Usuario:</label>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtClave">Contraseña:</label>
                                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnConnect" runat="server" Text="Conectar" CssClass="btn btn-info btn-block" OnClick="btnConnect_Click" />
                            <div class="mt-3">
                                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</body>
</html>
