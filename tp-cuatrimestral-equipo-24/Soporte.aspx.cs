using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace tp_cuatrimestral_equipo_24
{
    public partial class Soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            DateTime fecha;
            fecha = DateTime.Now;
            int puntaje;
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtEmail.Text) || string.IsNullOrWhiteSpace(txtmensaje.Text) || string.IsNullOrWhiteSpace(txtpuntaje.Text))
            {
                Response.Write("<script>alert('Por favor, complete todos los campos obligatorios.');</script>");
                return;
            }
            if (!int.TryParse(txtpuntaje.Text, out puntaje) || (puntaje < 1 || puntaje > 10))
            {
                Response.Write("<script>alert('El puntaje debe ser un número entre el 1 y 10.');</script>");
                return;
            }

            EmailServic emailService = new EmailServic();
            string emailDestino = "bitesybocados24@gmail.com";
            string asunto = "Nueva Reseña de Cliente";
            string cuerpo = $"<h1>Reseña de Cliente</h1><br>" +
                            $"<b>Nombre:</b> {TxtNombre.Text}<br>" +
                            $"<b>Email:</b> {TxtEmail.Text}<br>" +
                            $"<b>Mensaje:</b> {txtmensaje.Text}<br>" +
                            $"<b>Puntaje:</b> {puntaje}<br>" +
                            $"<b>Fecha:</b> {fecha}<br>";


            emailService.armarCorreo(emailDestino, asunto, cuerpo);

            try
            {
                emailService.EnviarMail();

                Reseña Nuevareseña = new Reseña
                {
                    Nombre = TxtNombre.Text,
                    Email = TxtEmail.Text,
                    Fecha = fecha,
                    puntaje = puntaje,
                    mensaje = txtmensaje.Text
                };
                

                ClientesGestion Client = new ClientesGestion();
                Client.InsertarReseña(Nuevareseña);
               

                TxtNombre.Text = "";
                TxtEmail.Text = "";
                txtmensaje.Text = "";
                txtpuntaje.Text = "";

                // Muestra el mensaje de éxito y luego redirige
                ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", "alert('Reseña enviada.'); window.location='Soporte.aspx';", true);
                /*
                Response.Write("<script>alert('Reseña enviada.');</script>");
                Response.Redirect("VistaCliente.aspx");
                */
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Write("<script>alert('Error al enviar la reseña.');</script>");
            }
        }
    }
}