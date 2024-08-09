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
using Negocio;

namespace Presentation
{
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo article = new Articulo();
            CatalogoNegocio negocio = new CatalogoNegocio();
            try
            {
                article.Codigo = txtCodigo.Text;
                article.Nombre = txtNombre.Text;
                article.Descripcion = txtDescripcion.Text;  
                article.ImagenUrl = txtImagenUrl.Text;
                article.Precio = decimal.Parse(txtPrecio.Text);

                negocio.agregar(article);
                MessageBox.Show("Se agrego exitosamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
