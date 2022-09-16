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

namespace TPWinForm_Gottig_Ramirez
{
    public partial class frmAgregarArt : Form
    {
        private Articulo articulo = null;
        public frmAgregarArt()
        {
            InitializeComponent();
        }

        public frmAgregarArt(Articulo art)
        {
            InitializeComponent();
            this.articulo = art;
            this.lblTituloAgregar.Text = $"Modificar articulo #{this.articulo.Codigo}";
            Text = "Modificar Articulo";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDesc.Text;

                articulo.Marca = new Marca();
                articulo.Marca = (Marca)cbxMarcas.SelectedItem;

                articulo.Categoria = new Categoria();
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;

                articulo.ImagenUrl = tbxImagenUrl.Text;
                articulo.Precio = float.Parse(tbxPrecio.Text);

                if(articulo.Id != 0)
                {
                    articuloNegocio.ModificarArticulo(articulo);
                    MessageBox.Show("Articulo modificado exitosamente!");
                }
                else
                {
                    articuloNegocio.AgregarArticulo(articulo);
                    MessageBox.Show("Articulo agregado exitosamente!");
                }

                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Campo Precio inválido. Ingrese solo números.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }

        }

        private void frmAgregarArt_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio catNegocio = new CategoriaNegocio();

            try
            {
                cbxMarcas.DataSource = marcaNegocio.listar();
                cbxMarcas.ValueMember = "ID";
                cbxMarcas.DisplayMember = "Descripcion";


                cbxCategoria.DataSource = catNegocio.listar();
                cbxCategoria.ValueMember = "ID";
                cbxCategoria.DisplayMember = "Descripcion";

                if(articulo != null)
                {
                    tbxCodigo.Text = articulo.Codigo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDesc.Text = articulo.Descripcion;

                    //falta arreglar 
                    cbxCategoria.SelectedValue = articulo.Categoria.ID;
                    cbxMarcas.SelectedValue = articulo.Marca.ID;

                    tbxImagenUrl.Text = articulo.ImagenUrl;
                    //img tiene que ir fixed al recuadro
                    cargarImagen(articulo.ImagenUrl);

                    tbxPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cargarImagen(string img)
        {
            try
            {
                pictureBox1.Load(img);
            }
            catch (Exception)
            {
                pictureBox1.Load("https://budmil.at/files/system/no_image.png");
            }
        }

        private void tbxImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxImagenUrl.Text);
        }
    }
}
