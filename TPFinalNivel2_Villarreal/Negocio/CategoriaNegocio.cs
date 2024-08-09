using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
			List<Categoria> lista = new List<Categoria>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("select Id, Descripcion from CATEGORIAS");
				datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria auxx = new Categoria();
                    auxx.Id = (int)datos.Lector["Id"];
                    auxx.Descripcion = (string)datos.Lector["Description"];

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
