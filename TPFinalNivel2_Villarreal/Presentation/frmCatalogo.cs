﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulos;
        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl); 
            }
              
        }

        private void cargar()
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                hideColumns();

                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void hideColumns()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAdd add = new frmAdd();
            add.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAdd modify = new frmAdd(seleccionado);
            modify.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CatalogoNegocio negocio = new CatalogoNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult answer = MessageBox.Show("Esta seguro de eliminar el articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e){}

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lstFiltrada = new List<Articulo>();
            string filtro = txtFiltro.Text;

            if (filtro.Length >= 2)
            {
                lstFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                lstFiltrada = listaArticulos;
            }



            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = lstFiltrada;
            hideColumns();
        }
    }
}
