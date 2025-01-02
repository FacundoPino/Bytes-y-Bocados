using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_24
{
    public partial class Pedidos : System.Web.UI.Page
    {
        private PedidoNegocio pedidoNegocio = new PedidoNegocio();
        private InsumosNegocio insumoNegocio = new InsumosNegocio();
        private MesaNegocio mesaNegocio = new MesaNegocio();
        private AccesoDatos datos = new AccesoDatos();

        private List<Insumo> listaInsumos
        {
            get { return (List<Insumo>)Session["ListadoInsumos"]; }
            set { Session["ListadoInsumos"] = value; }
        }

        private List<ItemPedido> listaPedidos
        {
            get { return (List<ItemPedido>)Session["Pedidos"]; }
            set { Session["Pedidos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UsuarioSeleccionado"] == null)
                {
                    Response.Redirect("RegistroLogin.aspx");
                    return;
                }

                if (Session["IdMesa"] != null)
                {
                    int idMesa = (int)Session["IdMesa"];
                    numeroMesaLabel.Text = idMesa.ToString();

                    try
                    {
                        CargarInsumos();
                        CargarPedidos();
                        CalcularTotal();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Ocurrió un error al cargar los datos: " + ex.Message;
                    }
                }
                else
                {
                    Response.Redirect("Salon.aspx");
                }
            }
        }


        private void CargarInsumos()
        {
            try
            {
                listaInsumos = insumoNegocio.ListarConSpInsumo();
                GridView1.DataSource = listaInsumos;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "Ocurrió un error al cargar los insumos: " + ex.Message;
            }
        }

        private void CargarPedidos()
        {
            try
            {
                // Obtiene el ID de la mesa desde la sesión
                int idMesa = (int)Session["IdMesa"];

                // Obtiene los ítems del pedido para la mesa
                listaPedidos = pedidoNegocio.ObtenerItemsDePedido(idMesa);

                // Establece el origen de datos del GridView de pedidos
                GridViewPedidos.DataSource = listaPedidos;
                GridViewPedidos.DataBind();

                // Calcula el total del pedido
                CalcularTotal();
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "Ocurrió un error al cargar los pedidos: " + ex.Message;
            }
        }


        private void CalcularTotal()
        {
            decimal total = listaPedidos.Sum(p => p.ObtenerTotal());
            TotalLabel.Text = total.ToString("C");
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            var textoFiltro = Filtro.Text.ToLower();
            GridView1.DataSource = listaInsumos
                .Where(i => i.Nombre.ToLower().Contains(textoFiltro) || i.Tipo.ToLower().Contains(textoFiltro))
                .ToList();
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var insumoSeleccionado = listaInsumos[index];

                if (insumoSeleccionado.Stock <= 0)
                {
                    ErrorMessage.Text = "No hay suficiente stock para agregar este insumo.";
                    return;
                }

                var itemPedido = new ItemPedido
                {
                    Insumo = insumoSeleccionado,
                    IdInsumo = insumoSeleccionado.IdInsumo,
                    Cantidad = 1,  // Asumimos una cantidad inicial de 1
                    PrecioUnitario = insumoSeleccionado.Precio
                };

                if (listaPedidos == null)
                {
                    listaPedidos = new List<ItemPedido>();
                }

                // Verificar si el ítem ya está en la lista
                var itemExistente = listaPedidos.FirstOrDefault(i => i.Insumo.IdInsumo == insumoSeleccionado.IdInsumo);
                if (itemExistente != null)
                {
                    itemExistente.Cantidad++;
                }
                else
                {
                    listaPedidos.Add(itemPedido);
                }

                // Actualizar el stock del insumo en la vista y en el negocio
                insumoSeleccionado.Stock--;
                insumoNegocio.ActualizarStockInsumo(insumoSeleccionado.IdInsumo, -1);  // Restar 1 del stock del insumo
                GridView1.DataSource = listaInsumos;
                GridView1.DataBind();

                GridViewPedidos.DataSource = listaPedidos;
                GridViewPedidos.DataBind();
                CalcularTotal();
            }
        }

        protected void GridViewPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var itemPedido = listaPedidos[index];
            var insumo = listaInsumos.First(i => i.IdInsumo == itemPedido.IdInsumo);

            if (e.CommandName == "Aumentar")
            {
                if (insumo.Stock > 0)
                {
                    itemPedido.Cantidad++;
                    insumo.Stock--;
                    insumoNegocio.ActualizarStockInsumo(itemPedido.IdInsumo, -1);
                }
                else
                {
                    ErrorMessage.Text = "No hay suficiente stock para aumentar este ítem.";
                }
            }
            else if (e.CommandName == "Disminuir")
            {
                if (itemPedido.Cantidad > 1)
                {
                    itemPedido.Cantidad--;
                    insumo.Stock++;
                    insumoNegocio.ActualizarStockInsumo(itemPedido.IdInsumo, 1);
                }
                else
                {
                    insumo.Stock += itemPedido.Cantidad;
                    insumoNegocio.ActualizarStockInsumo(itemPedido.IdInsumo, itemPedido.Cantidad);
                    listaPedidos.RemoveAt(index);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                insumo.Stock += itemPedido.Cantidad;
                insumoNegocio.ActualizarStockInsumo(itemPedido.IdInsumo, itemPedido.Cantidad);
                listaPedidos.RemoveAt(index);
            }

            GridViewPedidos.DataSource = listaPedidos;
            GridViewPedidos.DataBind();
            GridView1.DataSource = listaInsumos;
            GridView1.DataBind();
            CalcularTotal();
        }

        protected void BtnCerrarPedido_Click(object sender, EventArgs e)
        {
            if (Session["IdMesa"] != null && listaPedidos != null && listaPedidos.Count > 0)
            {
                try
                {
                    int idMesa = (int)Session["IdMesa"];
                    Usuario usuario = (Usuario)Session["UsuarioSeleccionado"];

                    if (usuario == null || usuario.Id == 0)
                    {
                        throw new Exception("El ID de usuario no está presente en la sesión.");
                    }

                    int idUsuario = usuario.Id;

                    // Usar el IdPedido de la sesión

                    if (Session["IdPedido"] == null)
                    {
                        ErrorMessage.Text = "No se encontró un ID de pedido en la sesión.";
                        return;
                    }

                    int idPedido = (int)Session["IdPedido"] != null ? (int)Session["IdPedido"]:0;

                    if (idPedido == 0)
                    {
                        ErrorMessage.Text = "El pedido no está inicializado.";
                        return;
                    }

                    var totalPedido = listaPedidos.Sum(p => p.ObtenerTotal());
                    
                    // Agregar los ítems al pedido
                    foreach (var item in listaPedidos)
                    {
                        pedidoNegocio.AgregarItemPedido(idPedido, item.IdInsumo, item.Cantidad, item.PrecioUnitario);
                    }

                    // Actualizar el total del pedido
                    pedidoNegocio.ActualizarTotalPedido(idPedido, totalPedido);

                    // Cerrar el pedido
                    pedidoNegocio.CerrarPedido(idPedido);

                    // Cerrar la mesa
                    mesaNegocio.CerrarMesa(idMesa);

                    // Limpiar sesión
                    Session.Remove("IdMesa");
                    Session.Remove("Pedidos");
                    Session.Remove("ListadoInsumos");

                    // Redirigir a la página de salón 
                    Response.Redirect("Pagos.aspx?idPedido=" + idPedido, false);
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    ErrorMessage.Text = "Ocurrió un error al cerrar el pedido: " + ex.Message + " | StackTrace: " + ex.StackTrace;
                }
            }
            else
            {
                // Mostrar un mensaje de error si no hay pedidos
                ErrorMessage.Text = "No hay pedidos para cerrar.";
            }
        }



    }
}
