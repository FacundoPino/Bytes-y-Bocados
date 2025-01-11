using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace tp_cuatrimestral_equipo_24
{
    public partial class ModificarInsumo : System.Web.UI.Page
    {
        List<Insumo> listaInsumos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int P = Convert.ToInt32(Session["Puesto"]);
                if(P != 2)
                {
                    btnModificarInsumo.Visible = false;
                    BajaAltaLogica.Visible = false;
                }
                Insumo seleccionado = new Insumo();
                InsumosNegocio negocio = new InsumosNegocio();
                List<Insumo> listaInsu = new List<Insumo>();
                Insumo insu = new Insumo();

                if (Session["Listado"] != null)
                {
                    listaInsu = (List<Insumo>)Session["Listado"];
                }
                try
                {
                    if (Request.QueryString["IdInsumo"] != null)
                    {
                        int id = Convert.ToInt32(Request.QueryString["IdInsumo"]);
                        foreach (Insumo item in listaInsu)
                        {
                            if (id == item.IdInsumo)
                            {
                                insu = item;
                            }
                        }

                        txtNombre.Value = insu.Nombre;
                        ddlTipo.Text = insu.Tipo;
                        txtPrecio.Value = insu.Precio.ToString();
                        txtStock.Value = insu.Stock.ToString();
                        txtImagen.Value = insu.UrlImagen.ToString();
                        txtDescripcion.Value = insu.Descripcion;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error: {ex.Message}');</script>");
                }

                int id2 = Convert.ToInt32(Request.QueryString["IdInsumo"]);
                foreach (var item in listaInsu)
                {
                    if (id2 == item.IdInsumo)
                    {
                        seleccionado = item;
                    }
                }

                if (seleccionado.Activo == false)
                {
                    BajaAltaLogica.Text = "Reactivar";
                }
            }
        }

        protected void btnModificarInsumo_Click(object sender, EventArgs e)
        {
            InsumosNegocio InsumosNegocio = new InsumosNegocio();
            Insumo insu = new Insumo();

            try
            {
                if (string.IsNullOrEmpty(txtNombre.Value))
                {
                    Response.Write("<script>alert('Por favor, complete el nombre del Insumo.');</script>");
                }
                if (string.IsNullOrEmpty(txtPrecio.Value))
                {
                    Response.Write("<script>alert('Por favor, complete el nombre del Precio.');</script>");
                }
                if (string.IsNullOrEmpty(txtStock.Value))
                {
                    Response.Write("<script>alert('Por favor, complete el nombre del Stock.');</script>");
                }
                if (string.IsNullOrEmpty(txtDescripcion.Value))
                {
                    Response.Write("<script>alert('Por favor, complete el nombre de la Descripcion.');</script>");
                }
                if (Regex.IsMatch(txtPrecio.Value, @"\D"))
                {
                    Response.Write("<script>alert('Ingresar solo numeros en el Precio');</script>");
                }
                if (Regex.IsMatch(txtStock.Value, @"\D"))
                {
                    Response.Write("<script>alert('Ingresar solo numeros en el Stock');</script>");
                }



                insu.IdInsumo = Convert.ToInt32(Request.QueryString["IdInsumo"]);
                insu.Nombre = txtNombre.Value;
                insu.Tipo = ddlTipo.Text;
                insu.Precio = decimal.Parse(txtPrecio.Value);
                insu.Stock = int.Parse(txtStock.Value);
                insu.UrlImagen = txtImagen.Value;
                insu.Descripcion = txtDescripcion.Value;

                if (insu.Tipo != "" && insu.Nombre != "" && insu.Descripcion != "" && txtPrecio.Value != "" && txtStock.Value != "")
                {
                    InsumosNegocio.Modificar2(insu);
                    MessageBox.Show("Modificado exitosamente :)");
                    ;
                }
                else
                {
                    MessageBox.Show("Complete todos los campos mi estimado/a");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Response.Redirect(Request.RawUrl);
                Response.Redirect("Menu.aspx");
            }


        }

        protected void BajaAltaLogica_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["IdInsumo"]);
                InsumosNegocio negocio = new InsumosNegocio();
                List<Insumo> lista = new List<Insumo>();
                Insumo insu = new Insumo();

                lista = (List<Insumo>)Session["Listado"];
                
                foreach (var item in lista)
                {
                    if (id == item.IdInsumo)
                    {
                        insu = item;
                    }
                }


                if (insu.Activo == true)
                {
                    negocio.BajaLogica(insu);
                    Response.Redirect("Menu.aspx");
                }
                else
                {
                    negocio.AltaLogica(insu);
                    Response.Redirect("Menu.aspx");
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}