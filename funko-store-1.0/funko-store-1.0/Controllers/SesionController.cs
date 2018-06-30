using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

using funko_store_1._0.Models;

namespace funko_store_1._0.Controllers
{
    public class SesionController : Controller
    {
        FUNKOBDEntities1 data = new FUNKOBDEntities1();
        
        // GET: Sesion
        public ActionResult Registro()
        {
            return View(new tb_usuarios()); 
        }

        [HttpPost]
        public ActionResult Registro(string nomusu, string pass)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);
            cn.Open();

            if (!ModelState.IsValid)
            {
                return View(new tb_usuarios());
            }


            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tb_usuarios(nomusu, pass, tipusu, estado) VALUES(@usu, @pass, @tipo, @estado)", cn);
                cmd.Parameters.AddWithValue("@usu", nomusu);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@tipo", "CLIENTE");
                cmd.Parameters.AddWithValue("@estado", "A");

                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT idusu FROM tb_usuarios order by idusu desc", cn);
                SqlDataReader dr = cmd2.ExecuteReader();

                if(Session["usuario"] == null)
                {
                    UsuarioSesion ususesion = new UsuarioSesion();
                    ususesion.idUsuSesion = dr.GetString(0);
                    ususesion.nomUsuSesion = nomusu;
                    Session["usuario"] = ususesion;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ", e.Message);
            }
            finally
            {
                cn.Close();
            }

            return RedirectToAction("../Funko/Index");

        }

        // GET: Sesion
        public ActionResult Login()
        {
            return View(new tb_usuarios());
        }

        [HttpPost]
        public ActionResult Login(string nomusu, string pass)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);
            cn.Open();

            if (!ModelState.IsValid)
            {
                return View(new tb_usuarios());
            }


            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tb_usuarios where nomusu = @usu and pass = @pass and estado = 'A'", cn);
                cmd.Parameters.AddWithValue("@usu", nomusu);
                cmd.Parameters.AddWithValue("@pass", pass);          
                                               
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Session["usuario"] != null)
                    {
                        Session["usuario"] = null;
                    }
                    UsuarioSesion ususesion = new UsuarioSesion();
                    ususesion.idUsuSesion = dr.GetString(0);
                    ususesion.nomUsuSesion = dr.GetString(1);
                    Session["usuario"] = ususesion;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ", e.Message);
            }
            finally
            {
                cn.Close();
            }

            return RedirectToAction("../Funko/Index");

        }

        public ActionResult LogOut()
        {
            Session["usuario"] = null;
            return RedirectToAction("../Funko/Index");
        }

    }
}