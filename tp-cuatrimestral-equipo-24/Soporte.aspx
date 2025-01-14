<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Soporte.aspx.cs" Inherits="tp_cuatrimestral_equipo_24.Soporte" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formulario de Soporte</title>
    <link href="EstilosSoporte.css" rel="stylesheet" />

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

    <style>
        body, html {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            height: 100%;
        }

        header {
            width: 100%;
            background-color: #333;
            color: white;
            text-align: center;
            padding: 10px 0;
            position: fixed;
            top: 0;
            left: 0;
        }

        #time {
            font-size: 16px;
            font-size: 2.5rem; /* Tamaño de la fuente del título */
            position: absolute;
            right: 20px; /* Margen derecho */
        }

        #contacto {
            min-height: 100vh;
            padding: 120px 15px;
            background: linear-gradient(rgba(0, 0, 0, .7), rgba(0, 0, 0, .7)), url("https://previews.123rf.com/images/yupiramos/yupiramos1307/yupiramos130700939/20982775-restaurante-de-piel-sobre-fondo-de-color-rosa-ilustraci%C3%B3n-vectorial.jpg");
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            color: #ffffff;
        }

            #contacto .titulo-seccion {
                margin-bottom: 20px;
                text-align: center;
                font-size: 22px;
                text-transform: uppercase;
                color: #ffffff;
                text-decoration: underline;
                text-decoration-color: #d3d3d3;
                text-decoration-thickness: 5px;
            }

            #contacto .contenedor-form {
                max-width: 600px;
                margin: auto;
                background: rgba(255, 255, 255, 0.1); /* Fondo blanco con opacidad */
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
            }

                #contacto .contenedor-form .fila {
                    margin-bottom: 15px;
                }

                #contacto .contenedor-form .mitad {
                    display: flex;
                    justify-content: space-between;
                    gap: 15px; /* Espacio entre los elementos en flex */
                }

                #contacto .contenedor-form input,
                #contacto .contenedor-form textarea {
                    padding: 10px;
                    border-radius: 10px;
                    border: 1px solid #919191;
                    width: 100%; /* Asegura que los inputs y textarea ocupen todo el ancho disponible */
                    box-sizing: border-box; /* Incluye el padding y el border dentro del width */
                }

                #contacto .contenedor-form .input-mitad {
                    width: 48%; /* Ajusta el ancho de los inputs cuando están en una fila con dos columnas */
                }

                #contacto .contenedor-form .input-full {
                    width: 100%;
                }

        .btn-enviar {
            display: block;
            width: 100%;
            padding: 8px 15px;
            background-color: #4CAF50;
            color: #ffffff;
            border: none;
            border-radius: 10px;
            cursor: pointer;
            font-size: 14px;
            transition: background-color 0.3s;
        }

            .btn-enviar:hover {
                background-color: #45a049;
            }

        .btn-volver {
            display: inline-block; /* Estilo de botón */
            padding: 8px 15px; /* Espaciado interno */
            background-color: #f44336; /* Color de fondo */
            color: white; /* Color del texto */
            border: none; /* Sin borde */
            border-radius: 8px; /* Bordes redondeados */
            font-size: 14px; /* Tamaño del texto */
            cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
            text-align: center; /* Centra el texto */
            transition: background-color 0.3s; /* Efecto de transición */
            margin-top: 10px; /* Separación superior */
        }

            .btn-volver:hover {
                background-color: #d32f2f; /* Color más oscuro al pasar el mouse */
            }


        .footer {
            color: black;
            background-color: black;
            font-size: 1.5rem; /* Tamaño de la fuente del título */
            font-family: 'Roboto', sans-serif; /* Tipo de fuente */
            padding: 0.5rem;
            background: linear-gradient(to left, green, white); /* Línea inferior */
            text-align: center; /* Alineación del texto */
        }
    </style>

</head>
<body>
    <header>
        <div id="time" class="time-display"></div>
        <!-- Elemento para mostrar la hora -->
    </header>

    <section id="contacto">
        <div class="contenedor-form">
            <form id="form1" runat="server">
                <div class="fila mitad">
                    <label>
                        Nombre Completo*:
                        <asp:TextBox ID="TxtNombre" Text="" CssClass="input-mitad" runat="server"></asp:TextBox>
                    </label>
                    <label>
                        Dirección de Email*:
                        <asp:TextBox ID="TxtEmail" Text="" CssClass="input-mitad" runat="server"></asp:TextBox>
                    </label>
                </div>
                <div class="fila">
                    <label>
                        Mensaje*:
                        <asp:TextBox ID="txtmensaje" Text="" CssClass="input-mitad" runat="server"></asp:TextBox>
                    </label>
                </div>
                <div class="fila">
                    <label>
                        Puntaje (1 al 10)*:
                      <asp:TextBox ID="txtpuntaje" Text="" CssClass="input-mitad" runat="server"></asp:TextBox>
                    </label>
                </div>

                <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CssClass="btn-enviar" OnClick="BtnEnviar_Click" />
                <asp:Button ID="btnvolver" runat="server" Text="Volver" CssClass="btn-volver" PostBackUrl="~/VistaCliente.aspx" />
            </form>
        </div>
    </section>
    <footer>
        <div class="footer">
            Desarrollado por 
           
            <a href="https://www.linkedin.com/in/ismael-oreste-8b116a254">Ismael Oreste</a>,
           
            <a href="https://www.linkedin.com/in/facundopino/">Facundo Pino</a> y
           
            <a href="https://www.linkedin.com/in/pedroquiros/">Pedro Quiros</a>
        </div>
    </footer>

</body>
</html>
