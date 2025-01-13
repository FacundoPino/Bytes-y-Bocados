<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Recepción</title>
    <link href="Estilos2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <section id="inicio">
            
            <div class="presentacion">
                <p class="bienvenida">Bienvenidos</p>
                <h2>AL <span>RESTAURANTE</span>, BYTES Y BOCADOS</h2>
                <p class="descripcion">
                    El Restaurante con la mejor critica en el mundo web 
                     y gastronomico.
                </p>
                <div class="mb-3 row">
                    <label for="inputPassword" class="col-sm-2 col-form-label">Usuario:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">
                    <label for="inputPassword" class="col-sm-2 col-form-label">Contraseña:</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3 row">

                    <asp:Button ID="txtIngresar" runat="server" CssClass="Ingresar" Text="Iniciar Sesión" OnClick="btnIngresar_Click" />
                    <asp:Button ID="btnRegistrarse" runat="server" CssClass="Ingresar" Text="Registrarse" Visible ="false " PostBackUrl="~/RegistroLogin.aspx" OnClick="btnRegistrarse_Click" />
                </div>
                    <asp:Button ID="txtRecupPass" runat="server" CssClass="Ingresar" Text="Reseña" Visible ="false " OnClick="btnRecuperarPass_Click"/>
            </div>
        </section>
    </form>
</body>
</html>
