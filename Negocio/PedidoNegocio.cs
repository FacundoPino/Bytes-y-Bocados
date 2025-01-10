using Dominio;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class PedidoNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Pedido> Listar()
        {
            List<Pedido> lista = new List<Pedido>();

            try
            {
                string consulta = "SELECT IdPedido, Estado, FechaHoraGenerado, IdUsuario FROM Pedido";
                datos.SetearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido
                    {
                        IdPedido = Convert.ToInt32(datos.Lector["IdPedido"]),
                        Estado = (bool)datos.Lector["Estado"],
                        FechaHoraGenerado = (DateTime)datos.Lector["FechaHoraGenerado"],
                        IdUsuario = Convert.ToInt32(datos.Lector["IdUsuario"])  // Agregado
                    };

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los pedidos", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;
        }

        public int CantidadPedidos(int idMesa)
        {
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta(@"
                    SELECT COUNT(IdPedido) AS CANTIDAD_PEDIDOS
                    FROM Pedido
                    WHERE IdMesa = @idMesa
                    GROUP BY IdMesa");
                datos.SeterParametros("@idMesa", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["CANTIDAD_PEDIDOS"];
                }

                return -1;  // Indica que no se encontraron pedidos para la mesa
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar los pedidos", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public decimal TotalPedidos(int idMesa)
        {
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta(@"
                    SELECT SUM(Total) AS TOTAL_PEDIDOS
                    FROM Pedido
                    WHERE IdMesa = @idMesa
                    GROUP BY IdMesa");
                datos.SeterParametros("@idMesa", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (decimal)datos.Lector["TOTAL_PEDIDOS"];
                }

                return 0;  // Si no hay pedidos, el total es 0
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el total de los pedidos", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Insumo> ListarInsumos()
        {
            List<Insumo> lista = new List<Insumo>();

            try
            {
                datos.LimpiarParametros();
                datos.setearProcedimiento("SP_ListarInsumos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo insumo = new Insumo
                    {
                        IdInsumo = (int)datos.Lector["IdInsumo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Stock = (int)datos.Lector["Stock"],
                        Precio = (decimal)datos.Lector["Precio"],
                        Tipo = (string)datos.Lector["Tipo"],  // Asegúrate de que 'Tipo' sea un campo en tu base de datos
                        Descripcion = datos.Lector["Descripcion"] as string,  // Si puede ser nulo
                        UrlImagen = datos.Lector["UrlImagen"] as string  // Si puede ser nulo
                    };

                    lista.Add(insumo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar insumos", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;
        }

        public List<Mesas> ListarMesas()
        {
            List<Mesas> lista = new List<Mesas>();

            try
            {
                datos.LimpiarParametros();
                datos.setearProcedimiento("SP_ListarMesas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesas mesa = new Mesas
                    {
                        IdMesa = (int)datos.Lector["IdMesa"],
                        Numero = (int)datos.Lector["Numero"],
                        Estado = (bool)datos.Lector["Estado"]
                    };

                    lista.Add(mesa);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar mesas", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;
        }

        public void InsertarPedido(DateTime fechaHora, decimal total, int idMesa, int idUsuario)
        {
            try
            {
                datos.LimpiarParametros();
                datos.setearProcedimiento("InsertarPedido");
                datos.SeterParametros("@FechaHora", fechaHora);
                datos.SeterParametros("@Total", total);
                datos.SeterParametros("@IdMesa", idMesa);
                datos.SeterParametros("@IdUsuario", idUsuario); // Este debe corresponder a la columna IdUsuario
                datos.SeterParametros("@Estado", true); // Agregamos el valor de Estado
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar pedido", ex);
            }
        }

        public void AgregarItemPedido(int idPedido, int idInsumo, int cantidad, decimal precioUnitario)
        {
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta("INSERT INTO ItemPedido (IdPedido, IdInsumo, Cantidad, PrecioUnitario) VALUES (@IdPedido, @IdInsumo, @Cantidad, @PrecioUnitario)");
                datos.SeterParametros("@IdPedido", idPedido);
                datos.SeterParametros("@IdInsumo", idInsumo);
                datos.SeterParametros("@Cantidad", cantidad);
                datos.SeterParametros("@PrecioUnitario", precioUnitario);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el ítem al pedido: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ActualizarStockInsumo(int idInsumo, int cantidad)
        {
            try
            {
                datos.LimpiarParametros();
                datos.setearProcedimiento("ActualizarStockInsumo");
                datos.SeterParametros("@IdInsumo", idInsumo);
                datos.SeterParametros("@Cantidad", cantidad);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar stock del insumo", ex);
            }
        }
        public void ActualizarTotalPedido(int idPedido, decimal total)
        {
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta("UPDATE Pedido SET Total = @Total WHERE IdPedido = @IdPedido");
                datos.SeterParametros("@Total", total);
                datos.SeterParametros("@IdPedido", idPedido);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el total del pedido", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public void CerrarPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta("UPDATE Pedido SET Estado = @Estado WHERE IdPedido = @IdPedido");
                datos.SeterParametros("@Estado", false); // Usamos un valor booleano en lugar de una cadena
                datos.SeterParametros("@IdPedido", idPedido);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar el pedido: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int CrearPedido(DateTime fechaHoraGenerado, decimal total, int idMesa, int idUsuario)
        {
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta("INSERT INTO Pedido (FechaHoraGenerado, Total, IdMesa, IdUsuario, Estado) VALUES (@FechaHoraGenerado, @Total, @IdMesa, @IdUsuario, @Estado)");
                datos.SeterParametros("@FechaHoraGenerado", fechaHoraGenerado);
                datos.SeterParametros("@Total", total);
                datos.SeterParametros("@IdMesa", idMesa);
                datos.SeterParametros("@IdUsuario", idUsuario); // Este debe corresponder a la columna IdUsuario
                datos.SeterParametros("@Estado", true); // Agregamos el valor de Estado
                datos.EjecutarAccion();

                datos.LimpiarParametros();
                datos.SetearConsulta("SELECT MAX(IdPedido) FROM Pedido WHERE IdMesa = @IdMesa AND FechaHoraGenerado = @FechaHoraGenerado");
                datos.SeterParametros("@IdMesa", idMesa);
                datos.SeterParametros("@FechaHoraGenerado", fechaHoraGenerado);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector[0];
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID del pedido.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el pedido: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<ItemPedido> ListarItemsPedido(int idPedido)
        {
            List<ItemPedido> lista = new List<ItemPedido>();

            try
            {
                datos.LimpiarParametros();
                string consulta = @"
                    SELECT i.IdInsumo, i.Nombre, ip.Cantidad, ip.PrecioUnitario
                    FROM ItemPedido ip
                    INNER JOIN Insumo i ON ip.IdInsumo = i.IdInsumo
                    WHERE ip.IdPedido = @IdPedido";

                datos.SetearConsulta(consulta);
                datos.SeterParametros("@IdPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo insumo = new Insumo
                    {
                        IdInsumo = (int)datos.Lector["IdInsumo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Precio = (decimal)datos.Lector["PrecioUnitario"]
                    };

                    ItemPedido item = new ItemPedido
                    {
                        Insumo = insumo,
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    };

                    lista.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los ítems del pedido", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;
        }

        public List<ItemPedido> ObtenerItemsDePedido(int idMesa)
        {
            List<ItemPedido> lista = new List<ItemPedido>();

            try
            {
                datos.LimpiarParametros();
                // Usar una consulta SQL directa en lugar del procedimiento almacenado
                datos.SetearConsulta(@"
            SELECT ip.IdInsumo, i.Nombre, ip.Cantidad, ip.PrecioUnitario
            FROM ItemPedido ip
            INNER JOIN Pedido p ON ip.IdPedido = p.IdPedido
            INNER JOIN Insumo i ON ip.IdInsumo = i.IdInsumo
            WHERE p.IdMesa = @IdMesa AND p.Estado = 1
        ");
                datos.SeterParametros("@IdMesa", idMesa);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ItemPedido item = new ItemPedido
                    {
                        Insumo = new Insumo
                        {
                            IdInsumo = (int)datos.Lector["IdInsumo"],
                            Nombre = (string)datos.Lector["Nombre"],
                            Precio = (decimal)datos.Lector["PrecioUnitario"],  // PrecioUnitario es el precio de cada unidad del ítem
                        },
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    };

                    lista.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ítems de pedido", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;
        }

        public void LimpiarMesas()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.LimpiarParametros();
                datos.SetearConsulta("UPDATE Mesa SET Estado = 0");
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar Mesas: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<ItemPedido> obtenerDetallePedido(int idPedido)
        {
            List<ItemPedido> lista = new List<ItemPedido>();

            try
            {
                datos.LimpiarParametros();
                // Usar una consulta SQL directa en lugar del procedimiento almacenado
                datos.SetearConsulta(@"SELECT 
                                        ip.IdInsumo, 
                                        i.Nombre AS NombreInsumo, 
                                        ip.Cantidad, 
                                        ip.PrecioUnitario
                                    FROM 
                                        ItemPedido ip
                                    INNER JOIN 
                                        Pedido p ON ip.IdPedido = p.IdPedido
                                    INNER JOIN 
                                        Insumo i ON ip.IdInsumo = i.IdInsumo 
                                    WHERE ip.IdPedido = @IdPedido;      
                  ");
                datos.SeterParametros("@IdPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ItemPedido item = new ItemPedido
                    {
                        Insumo = new Insumo
                        {
                            IdInsumo = (int)datos.Lector["IdInsumo"],
                            Nombre = (string)datos.Lector["NombreInsumo"],
                            Precio = (decimal)datos.Lector["PrecioUnitario"],  // PrecioUnitario es el precio de cada unidad del ítem
                        },
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    };

                    lista.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ítems de pedido", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return lista;

        }


    }
}
