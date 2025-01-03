using Negocio;
using System;
using Dominio;

namespace tp_cuatrimestral_equipo_24
{
    public partial class Pagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(Request.QueryString["idPedido"]);
            PagosNegocio negocio = new PagosNegocio();
            Pago pago = new Pago();
            pago = negocio.ListarPorId(idPedido);

            // Asignar valores a los controles
            txtMesa.InnerText = pago.nroMesa.ToString();
            txtMesero.InnerText = pago.Mesero; // Ejemplo
            Txtfecha.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            txtTotal.InnerText = pago.PrecioTotal.ToString(); // Ejemplo
            txtTipo.InnerText = ddlPagos.SelectedItem.Text;
            txtConsumicion.InnerText = "Pizza, Refresco"; // Ejemplo

            // Mostrar los controles con los valores asignados
            lblMesa.Visible = true;
            txtMesa.Visible = true;
            lblmesero.Visible = true;
            txtMesero.Visible = true;
            lblFecha.Visible = true;
            Txtfecha.Visible = true;
            lbltotal.Visible = true;
            txtTotal.Visible = true;
            lblTipo.Visible = true;
            txtTipo.Visible = true;
            lblconsumicion.Visible = true;
            txtConsumicion.Visible = true;
            htext.Visible = true;
            divtext.Visible = true;
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {

        }
    }
}