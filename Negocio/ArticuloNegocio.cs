﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            string consulta = "SELECT A.ID AS ID, A.CODIGO AS CODIGO, A.NOMBRE AS NOMBRE, A.DESCRIPCION AS DESCRIPCION, M.ID AS IdMarca,M.Descripcion AS MARCA, C.ID AS IdCategoria, C.Descripcion AS CATEGORIA, A.IMAGENURL AS IMAGENURL, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id";
            db.SetearConsulta(consulta);
            db.EjecutarLectura();

            try
            {
                while (db.Reader.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = (int)db.Reader["ID"];
                    articulo.Codigo = (string)db.Reader["Codigo"];
                    articulo.Nombre = (string)db.Reader["Nombre"];
                    articulo.Descripcion = (string)db.Reader["Descripcion"];

                    articulo.Marca = new Marca();

                    if (!(db.Reader["MARCA"] is DBNull))
                    {
                        articulo.Marca.ID = (int)db.Reader["IdMarca"];
                        articulo.Marca.Descripcion = (string)db.Reader["Marca"];
                    }

                    if (!(db.Reader["CATEGORIA"] is DBNull))
                    {
                        articulo.Categoria = new Categoria();
                        articulo.Categoria.ID = (int)db.Reader["IdCategoria"];
                        articulo.Categoria.Descripcion = (string)db.Reader["Categoria"];
                    }


                    articulo.ImagenUrl = (string)db.Reader["ImagenUrl"];
                    articulo.Precio = (float)db.Reader.GetDecimal(9);

                    articulos.Add(articulo);
                }

                return articulos;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            finally
            {
                db.CerrarConexion();
            }

        }

        public void AgregarArticulo(Articulo nuevo)
        {
            try
            {
                db.SetearParametro("@IdMarca", nuevo.Marca.ID);
                db.SetearParametro("@IdCategoria", nuevo.Categoria.ID);

                string Consulta = $"Insert Into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) Values ('{nuevo.Codigo}', '{nuevo.Nombre}', '{nuevo.Descripcion}', @IdMarca, @IdCategoria, '{nuevo.ImagenUrl}',  {nuevo.Precio})";

                db.SetearConsulta(Consulta);
                db.EjecutarAccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            finally
            {
                db.CerrarConexion();
            }
        }

        public void EliminarArticulo(Articulo AEliminar)
        {
            string Accion = $"Delete From ARTICULOS Where ARTICULOS.Id = {AEliminar.Id}";

            try
            {
                db.SetearConsulta(Accion);
                db.EjecutarAccion();

                MessageBox.Show($"Articulo {AEliminar.Codigo} eliminado!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally 
            { 
                db.CerrarConexion(); 
            }
        }

        public void ModificarArticulo(Articulo articulo)
        {
            try
            {
                string consulta = $"UPDATE ARTICULOS SET CODIGO = '{articulo.Codigo}', NOMBRE = '{articulo.Nombre}', Descripcion ='{articulo.Descripcion}', IdMarca=@IdMarca, IdCategoria=@IdCategoria, ImagenUrl = '{articulo.ImagenUrl}', PRECIO = {articulo.Precio} WHERE ID = {articulo.Id}";
                db.SetearConsulta(consulta);

                db.SetearParametro("@IdMarca", articulo.Marca.ID);
                db.SetearParametro("@IdCategoria", articulo.Categoria.ID);

                db.EjecutarAccion();
            }
            catch (Exception ex)
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
