<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VistaCliente.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.VistaCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BB</title>
    <link href="EstiloCliente.css" rel="stylesheet" />
</head>
<script>
    function updateTime() {
        const now = new Date();
        const hours = now.getHours().toString().padStart(2, '0');
        const minutes = now.getMinutes().toString().padStart(2, '0');
        const seconds = now.getSeconds().toString().padStart(2, '0');
        const timeString = `${hours}:${minutes}:${seconds}`;
        document.getElementById('time').textContent = timeString;
    }

    setInterval(updateTime, 1000); // Actualiza la hora cada segundo
    document.addEventListener('DOMContentLoaded', updateTime); // Llama a la función para que muestre la hora inmediatamente al cargar la página
</script>

<body>

    <form id="form1" runat="server">
        <!-- SECCION CLIENTE -->
        <section id="inicio">
            <div class="contenido">
                <header>
                    <!-- contenedor de la hora -->
                    <div id="time" class="time-display"></div>
                    <div class="contenido-header">
                        <h1>|BB|</h1>
                        <!-- icono del menu -->
                        <div id="icono-nav" onclick="responsiveMenu()">
                            <i class="fa-solid fa-bars"></i>
                        </div>
                    </div>
                </header>
                <div class="presentacion">
                    <p class="bienvenida">Bienvenidos</p>
                    <h2>!!<span>Bytes y Bocados</span>¡¡</h2>
                    <!-- Botones -->
                    <asp:Button ID="Button1" runat="server" CssClass="Ingresar" Text="Menú" PostBackUrl="~/Menu.aspx" />
                    <asp:Button ID="Button2" runat="server" CssClass="Ingresar" Text="Reseñas" PostBackUrl="~/Soporte.aspx" />
                </div>


            </div>
        </section>
    </form>
    <div class="footer">
        Desarrollado por 
   
    <a href="https://www.linkedin.com/in/ismael-oreste-8b116a254">Ismael Oreste</a>,
   
    <a href="https://www.linkedin.com/in/facundopino/">Facundo Pino</a> y
   
    <a href="https://www.linkedin.com/in/pedroquiros/">Pedro Quiros</a>
    </div>
</body>
</html>
