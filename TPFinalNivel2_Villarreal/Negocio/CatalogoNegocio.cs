using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class CatalogoNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader lector;
            try
            {
                conn.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdMarca, A.IdCategoria, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id = A.IdCategoria and M.Id = A.IdMarca";
                cmd.Connection = conn;

                conn.Open();
                lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Articulo auxx = new Articulo();
                    auxx.Id = (int)lector["Id"];
                    auxx.Codigo = (string)lector["Codigo"];
                    auxx.Nombre = (string)lector["Nombre"];
                    auxx.Descripcion = (string)lector["Descripcion"];
                    if (!(lector["ImagenUrl"] is DBNull))
                    {
                        auxx.ImagenUrl = (string)lector["ImagenUrl"];
                    }
                    auxx.Precio = (decimal)lector["Precio"];
                    auxx.Marca = new Marca();
                    auxx.Marca.Id = (int)lector["IdMarca"];
                    auxx.Marca.Descripcion = (string)lector["Marca"];
                    auxx.Categoria = new Categoria();
                    auxx.Categoria.Id = (int)lector["IdCategoria"];
                    auxx.Categoria.Descripcion = (string)lector["Categoria"];

                    lista.Add(auxx);
                }
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values(@Codigo, @Nombre, @Descripcion , @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("Precio", nuevo.Precio);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio where Id = @id");
                datos.setearParametro("@codigo", modificar.Codigo);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@descripcion", modificar.Descripcion);
                datos.setearParametro("@idMarca", modificar.Marca.Id);
                datos.setearParametro("@idCategoria", modificar.Categoria.Id);
                datos.setearParametro("@imagenUrl", modificar.ImagenUrl);
                datos.setearParametro("@precio", modificar.Precio);
                datos.setearParametro("@id", modificar.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdMarca, A.IdCategoria, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id = A.IdCategoria and M.Id = A.IdMarca And ";
                switch (campo)
                {
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Precio >" + filtro;
                                break;
                            case "Menor a":
                                consulta += "Precio <" + filtro;
                                break;
                            default:
                                consulta += "Precio =" + filtro;
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "Nombre like '%" + filtro + "%'";
                                break;
                        }

                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "C.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "C.Descripcion like '%" + filtro + "%'";
                                break;
                        }

                        break;
                    default:
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "M.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "M.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "M.Descripcion like '%" + filtro + "%'";
                                break;
                        }

                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo auxx = new Articulo();
                    auxx.Id = (int)datos.Lector["Id"];
                    auxx.Codigo = (string)datos.Lector["Codigo"];
                    auxx.Nombre = (string)datos.Lector["Nombre"];
                    auxx.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        auxx.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }
                    auxx.Precio = (decimal)datos.Lector["Precio"];
                    auxx.Marca = new Marca();
                    auxx.Marca.Id = (int)datos.Lector["IdMarca"];
                    auxx.Marca.Descripcion = (string)datos.Lector["Marca"];
                    auxx.Categoria = new Categoria();
                    auxx.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    auxx.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(auxx);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }
    }

    
}
