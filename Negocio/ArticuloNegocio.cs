﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private AccesoDatos db = new AccesoDatos();

        public List<Articulo> ListarArticulos()
        {
            List<Articulo> articulos = new List<Articulo>();
            string consulta = "SELECT A.ID AS ID, A.CODIGO AS CODIGO, A.NOMBRE AS NOMBRE, A.DESCRIPCION AS DESCRIPCION, M.Descripcion AS MARCA, C.Descripcion AS CATEGORIA, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id";
            db.SetearConsulta(consulta);
            db.EjecutarLectura();

            try
            {
                while (db.Reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)db.Reader["ID"];


                    articulos.Add(articulo);
                }

                return articulos;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            finally
            {
                db.CerrarConexion();
            }
            
        }
    }
}
