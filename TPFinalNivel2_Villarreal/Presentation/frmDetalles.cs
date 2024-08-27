using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio; 

namespace Presentation
{
    public partial class frmDetalles : Form
    {
        private Articulo art;
        public frmDetalles(Articulo art)
        {
            InitializeComponent();
            this.art = art;
            Text = "DETALLES";
        }

        private void frmDetalles_Load(object sender, EventArgs e)
        {
            txtDetNombre.Text = art.Nombre;
            txtPrecio.Text = art.Precio.ToString("#0.00");
            txtMarca.Text = art.Marca.Descripcion;
            txtCategoria.Text = art.Categoria.Descripcion;
            txtDescrip.Text = art.Descripcion;
            cargarImagen(art.ImagenUrl);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                picBoxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                picBoxArticulo.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
