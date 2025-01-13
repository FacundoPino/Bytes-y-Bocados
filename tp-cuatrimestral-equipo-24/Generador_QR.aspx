<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Generar Código QR</title>
    <script src="https://cdn.jsdelivr.net/npm/qrcode/build/qrcode.min.js"></script>
</head>
<body>

    <h1>Generador de Código QR</h1>

    <div>
        <input type="text" id="urlInput" placeholder="Ingrese URL" value="https://localhost:44318/VistaCliente.aspx">
        <button onclick="generarQR()">Generar QR</button>
    </div>

    <!-- Aquí se generará el código QR en un canvas -->
    <canvas id="qrCodeCanvas"></canvas>

    <script>
        function generarQR() {
            // Obtener la URL del input
            var url = document.getElementById("urlInput").value;

            // Obtener el canvas donde se generará el código QR
            var canvas = document.getElementById('qrCodeCanvas');

            // Limpiar el canvas antes de generar uno nuevo
            var ctx = canvas.getContext('2d');
            ctx.clearRect(0, 0, canvas.width, canvas.height);

            // Generar el código QR en el canvas
            QRCode.toCanvas(canvas, url, function (error) {
                if (error) {
                    console.error(error);
                } else {
                    console.log('Código QR generado');
                }
            });
        }
    </script>

</body>
</html>
