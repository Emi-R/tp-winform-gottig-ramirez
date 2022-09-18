﻿using System;
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

namespace TPWinForm_Gottig_Ramirez
{
    public partial class frmAgregarMarca : Form
    {
        private Marca marca = null;
        private MarcaNegocio negocioMarca = new MarcaNegocio();
        public frmAgregarMarca()
        {
            InitializeComponent();
        }

        public frmAgregarMarca(Marca marca)
        {
            this.marca = marca;
            InitializeComponent();
        }

        private void frmAgregarMarca_Load(object sender, EventArgs e)
        {
            if (marca != null)
            {
                lblMarca.Text = "Modificar marca";
                tbxDescripcion.Text = marca.Descripcion;
                btnAgregar.Text = "Modificar marca";
            }
            
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (marca == null) marca = new Marca();

                marca.Descripcion = tbxDescripcion.Text;

                if (marca.ID != 0)
                {
                    negocioMarca.ModificarMarca(marca);
                    MessageBox.Show("Marca modificada exitosamente!");
                }
                else
                {
                    negocioMarca.AgregarMarca(marca);
                    MessageBox.Show("Marca agregada exitosamente!");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

    }
}
