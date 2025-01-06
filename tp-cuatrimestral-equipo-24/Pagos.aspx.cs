using Negocio;
using System;
using Dominio;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

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
            Pago pago = negocio.ListarPorId(idPedido);

            // Datos simulados de productos del pedido (deberías obtener esto de la base de datos)
            var productos = new List<dynamic>
            {
                new { Producto = "Pizza", Cantidad = 2, PrecioUnitario = 10.0 },
                new { Producto = "Refresco", Cantidad = 3, PrecioUnitario = 2.5 },
                new { Producto = "Postre", Cantidad = 1, PrecioUnitario = 5.0 }
            };

            // Generar la tabla dinámica
            double total = 0;
            foreach (var item in productos)
            {
                tblDetalle.InnerHtml += $"<tr>" +
                                            $"<td>{item.Producto}</td>" +
                                            $"<td>{item.Cantidad}</td>" +
                                            $"<td>{item.PrecioUnitario:C}</td>" +
                                            $"<td>{item.Cantidad * item.PrecioUnitario:C}</td>" +
                                            $"</tr>";
                total += item.Cantidad * item.PrecioUnitario;
            }

            // Actualizar el total
            txtTotal.InnerText = total.ToString("C");

            // Asignar otros valores
            txtMesa.InnerText = pago.nroMesa.ToString();
            txtMesero.InnerText = pago.Mesero;
            Txtfecha.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            txtTipo.InnerText = ddlPagos.SelectedItem.Text;

            // Mostrar los controles
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
            tblDetalle.Visible = true;
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
                iTextSharp.text.Font titleFont = FontFactory.GetFont("Courier", 16, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textFont = FontFactory.GetFont("Courier", 12);
                iTextSharp.text.Font headerFont = FontFactory.GetFont("Courier", 12, iTextSharp.text.Font.BOLD);

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
                document.Add(new Paragraph($"Tipo de Pago: {txtTipo.InnerText}", textFont));
                document.Add(new Paragraph(" ", textFont)); // Espacio

                // Crear tabla para los productos
                PdfPTable table = new PdfPTable(4); // 4 columnas
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 40f, 20f, 20f, 20f }); // Ancho de columnas (en porcentajes)

                // Encabezados de la tabla
                table.AddCell(new PdfPCell(new Phrase("Producto", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Cantidad", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Precio Unitario", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Total", headerFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                // Datos de los productos
                var productos = new List<dynamic>
        {
            new { Producto = "Pizza", Cantidad = 2, PrecioUnitario = 10.0 },
            new { Producto = "Refresco", Cantidad = 3, PrecioUnitario = 2.5 },
            new { Producto = "Postre", Cantidad = 1, PrecioUnitario = 5.0 }
        };

                double total = 0;
                foreach (var item in productos)
                {
                    table.AddCell(new PdfPCell(new Phrase(item.Producto, textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(item.Cantidad.ToString(), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(item.PrecioUnitario.ToString("C"), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase((item.Cantidad * item.PrecioUnitario).ToString("C"), textFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    total += item.Cantidad * item.PrecioUnitario;
                }

                // Agregar la tabla al documento
                document.Add(table);

                // Agregar total general
                document.Add(new Paragraph($"Total: {total.ToString("C")}", textFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                });

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