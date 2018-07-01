using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace funko_store_1._0.Models
{
    public class Procedimientos
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);

        public string actualizaProducto(Productos pro)
        {
            string mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("modificarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idprodu", pro.idprodu);
                cmd.Parameters.AddWithValue("@codbar", pro.codbar);
                cmd.Parameters.AddWithValue("@nomprodu", pro.nomprodu);
                cmd.Parameters.AddWithValue("@idcate", pro.idcate);
                cmd.Parameters.AddWithValue("@entrada", pro.entrada);
                cmd.Parameters.AddWithValue("@salida", pro.salida);
                cmd.Parameters.AddWithValue("@precio", pro.precio);
                cmd.Parameters.AddWithValue("@caracte", pro.caracte);
                cmd.Parameters.AddWithValue("@descripcion", pro.descripcion);
                cmd.Parameters.AddWithValue("@imagen", pro.imagen);
                cmd.Parameters.AddWithValue("@estado", pro.estado);

                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + "registros actualizados";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return mensaje;
        }

        public string insertarProducto(Productos pro)
        {
            string mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("insertarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codbar", pro.codbar);
                cmd.Parameters.AddWithValue("@nomprodu", pro.nomprodu);
                cmd.Parameters.AddWithValue("@idcate", pro.idcate);
                cmd.Parameters.AddWithValue("@entrada", pro.entrada);
                cmd.Parameters.AddWithValue("@salida", pro.salida);
                cmd.Parameters.AddWithValue("@precio", pro.precio);
                cmd.Parameters.AddWithValue("@caracte", pro.caracte);
                cmd.Parameters.AddWithValue("@descripcion", pro.descripcion);
                cmd.Parameters.AddWithValue("@imagen", pro.imagen);
                cmd.Parameters.AddWithValue("@estado", pro.estado);

                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + "registros ingresados";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return mensaje;
        }

        public string eliminarProducto(string pro)
        {
            string msg = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("eliminarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idprodu", pro);
                int n = cmd.ExecuteNonQuery();
                msg = n.ToString() + "registros eliminado";
            }
            catch (SqlException ex)
            {
                msg = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return msg;
        }

        public List<Productos> listarProdcutos()
        {
            List<Productos> listap = new List<Productos>();
            SqlCommand cmd = new SqlCommand("listarProductos", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Productos pro = new Productos();
                pro.idprodu = dr.GetString(0);
                pro.codbar = dr.GetString(1);
                pro.nomprodu = dr.GetString(2);
                pro.idcate = dr.GetInt32(3);
                pro.nomcate = dr.GetString(4);
                pro.entrada = dr.GetInt32(5);
                pro.salida = dr.GetInt32(6);
                pro.precio = dr.GetDecimal(7);
                pro.caracte = dr.GetString(8);
                pro.descripcion = dr.GetString(9);
                pro.imagen = dr.GetString(10);
                pro.estado = dr.GetString(11);
                listap.Add(pro);
            }
            dr.Close();
            cn.Close();
            return listap;
        }

        public List<tb_categorias> listarCategorias()
        {
            List<tb_categorias> listac = new List<tb_categorias>();
            SqlCommand cmd = new SqlCommand("listarCategorias", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                tb_categorias cat = new tb_categorias();
                cat.idcate = dr.GetInt32(0);
                cat.nomcate = dr.GetString(1);
                listac.Add(cat);
            }
            dr.Close();
            cn.Close();
            return listac;
        }

        public Productos DetalleProducto(string id)
        {
            SqlCommand cmd = new SqlCommand("detalleProducto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idprodu", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            Productos reg = new Productos();
            if (dr.Read())
            {
                reg.idprodu = dr["idprodu"].ToString();
                reg.codbar = dr["codbar"].ToString();
                reg.nomprodu = dr["nomprodu"].ToString();
                reg.idcate = Convert.ToInt32(dr["idcate"].ToString());
                reg.nomcate = dr["nomcate"].ToString();
                reg.entrada = Convert.ToInt32(dr["entrada"].ToString());
                reg.salida = Convert.ToInt32(dr["salida"].ToString());
                reg.precio = Convert.ToDecimal(dr["precio"].ToString());
                reg.caracte = dr["caracte"].ToString();
                reg.descripcion = dr["descripcion"].ToString();
                reg.imagen = dr["imagen"].ToString();
                reg.estado = dr["estado"].ToString();
            }
            dr.Close();
            cn.Close();
            return reg;
        }


        public string ACTUALIZARUSUARIO(tb_usuarios usu)
        {
            string mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("modificarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusu", usu.idusu);
                cmd.Parameters.AddWithValue("@nomusu", usu.nomusu);
                cmd.Parameters.AddWithValue("@pass", usu.pass);
                cmd.Parameters.AddWithValue("@tipusu", usu.tipusu);
                cmd.Parameters.AddWithValue("@correo", usu.correo);
                cmd.Parameters.AddWithValue("@direcenvio", usu.direcenvio);
                cmd.Parameters.AddWithValue("@tarjeta", usu.tarjeta);
                cmd.Parameters.AddWithValue("@estado", usu.estado);

                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + "registros actualizados";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return mensaje;
        }

        public string INSERTARUSUARIOS(tb_usuarios usu)
        {
            string mensaje = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("insertarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusu", usu.idusu);
                cmd.Parameters.AddWithValue("@nomusu", usu.nomusu);
                cmd.Parameters.AddWithValue("@pass", usu.pass);
                cmd.Parameters.AddWithValue("@tipusu", usu.tipusu);
                cmd.Parameters.AddWithValue("@correo", usu.correo);
                cmd.Parameters.AddWithValue("@direcenvio", usu.direcenvio);
                cmd.Parameters.AddWithValue("@tarjeta", usu.tarjeta);
                cmd.Parameters.AddWithValue("@estado", usu.estado);

                int n = cmd.ExecuteNonQuery();
                mensaje = n.ToString() + "registros ingresados";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return mensaje;
        }

        public string ELIMINARUSUARIO(string usu)
        {
            string msg = "";
            cn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("eliminarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idusu", usu);
                int n = cmd.ExecuteNonQuery();
                msg = n.ToString() + "registros eliminado";
            }
            catch (SqlException ex)
            {
                msg = ex.Message;
            }
            finally
            {
                cn.Close();
            }

            return msg;
        }

        public List<tb_usuarios> LISTARUSUARIO()
        {
            List<tb_usuarios> listap = new List<tb_usuarios>();
            SqlCommand cmd = new SqlCommand("listarUsuarios", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                tb_usuarios usu = new tb_usuarios();
                usu.idusu = dr.GetString(0);
                usu.nomusu = dr.GetString(1);
                usu.pass = dr.GetString(2);
                usu.tipusu = dr.GetString(3);
                usu.correo = dr.GetString(4);
                usu.direcenvio = dr.GetString(5);
                usu.tarjeta = dr.GetString(6);
                usu.estado = dr.GetString(7);
                listap.Add(usu);
            }
            dr.Close();
            cn.Close();
            return listap;
        }

        public tb_usuarios DETALLEUSUARIO(string id)
        {
            SqlCommand cmd = new SqlCommand("detalleUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idusu", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            tb_usuarios reg = new tb_usuarios();
            if (dr.Read())
            {
                reg.idusu = dr["idusu"].ToString();
                reg.nomusu = dr["nomusu"].ToString();
                reg.pass = dr["pass"].ToString();
                reg.tipusu = dr["tipusu"].ToString();
                reg.correo = dr["correo"].ToString();
                reg.direcenvio = dr["direcenvio"].ToString();
                reg.tarjeta = dr["tarjeta"].ToString();
                reg.estado = dr["estado"].ToString();
            }
            dr.Close();
            cn.Close();
            return reg;
        }

    }
}