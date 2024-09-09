using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_24
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UsuarioSeleccionado"] != null)
            //{
            //    Response.Redirect("RegistroLogin.aspx",false);
            //}
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioGestion UsuGesti = new UsuarioGestion();
            try
            {
                usuario.NombreUsuario = txtnombre.Text;
                usuario.Clave = txtpassword.Text;

                if (UsuGesti.Loguear(usuario))
                {
                    PedidoNegocio negoPedi = new PedidoNegocio();
                    negoPedi.LimpiarMesas();
                    int Puesto = UsuGesti.BuscarPuesto(usuario);
                    // El ID de usuario ya debería estar asignado en el método Loguear
                    Session.Add("Puesto", Puesto);
                    Session.Add("Bienvenida", usuario);
                    Session.Add("UsuarioSeleccionado", usuario); // Asegúrate de que aquí también estás guardando el usuario completo

                    Response.Write("<script>alert('Inicio de sesión exitoso.');</script>");
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    Session.Add("Error", "Usuario o contraseña incorrectos");
                    Response.Redirect("Error.aspx", false);
                    Response.Write("<script>alert('Nombre de usuario o contraseña incorrectos.');</script>");
                }
            }
            catch (Exception Ex)
            {
                Response.Write("<script>alert('Error: " + Ex.ToString() + "');</script>");
                Session.Add("Error", Ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }



        protected void btnRecuperarPass_Click(object sender, EventArgs e)
        {

            Response.Redirect("Soporte.aspx");
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroLogin.aspx");
        }
    }
}

