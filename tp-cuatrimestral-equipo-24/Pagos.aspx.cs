using Negocio;
using System;
using Dominio;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

            btnPdf.Visible = true;
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear documento PDF
                Document document = new Document(PageSize.A4);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Configurar fuentes
                iTextSharp.text.Font titleFont  = FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textFont = FontFactory.GetFont("Courier", 12);

                // Agregar título
                Paragraph title = new Paragraph("RECIBO DE PAGO", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                // Agregar separador
                document.Add(new Paragraph("***************************************", textFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                // Detalles del recibo
                document.Add(new Paragraph($"Num de Mesa: {txtMesa.InnerText}", textFont));
                document.Add(new Paragraph($"Mesero: {txtMesero.InnerText}", textFont));
                document.Add(new Paragraph($"Fecha: {Txtfecha.InnerText}", textFont));
                document.Add(new Paragraph($"Total: {txtTotal.InnerText}", textFont));
                document.Add(new Paragraph($"Tipo de Pago: {txtTipo.InnerText}", textFont));
                document.Add(new Paragraph($"Consumición: {txtConsumicion.InnerText}", textFont));

                // Agregar separador final
                document.Add(new Paragraph("***************************************", textFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                // Mensaje de agradecimiento
                document.Add(new Paragraph("ESPERO QUE LE HAYA GUSTADO", textFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });
                document.Add(new Paragraph("GRACIAS!", textFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                // Cerrar documento
                document.Close();

                // Enviar el PDF al navegador
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=ReciboDePago.pdf");
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }

}