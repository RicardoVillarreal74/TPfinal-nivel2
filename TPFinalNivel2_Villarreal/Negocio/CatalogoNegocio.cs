using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
                cmd.CommandText = "select Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca from ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id = A.IdCategoria and M.Id = A.IdMarca";
                cmd.Connection = conn;

                conn.Open();
                lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    Articulo auxx = new Articulo();
                    auxx.Codigo = (string)lector["Codigo"];
                    auxx.Nombre = (string)lector["Nombre"];
                    auxx.Descripcion = (string)lector["Descripcion"];
                    auxx.ImagenUrl = (string)lector["ImagenUrl"];
                    auxx.Precio = (decimal)lector["Precio"];
                    auxx.Marca = new Marca();
                    auxx.Marca.Descripcion = (string)lector["Marca"];
                    auxx.Categoria = new Categoria();
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
    }
}
