using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader lector;
        public SqlDataReader Lector 
        {
            get {  return lector; } 
        }

        public AccesoDatos()
        {
            conn = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true");
            cmd = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            cmd.Connection = conn;
            try
            {
                conn.Open();
                lector = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conn.Close();
        }

        public void setearParametro(string nombre, object valor)
        {
            cmd.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
