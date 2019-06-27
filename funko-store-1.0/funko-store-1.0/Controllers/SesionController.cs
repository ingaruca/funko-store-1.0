using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

using funko_store_1._0.Models;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

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
                SqlCommand cmd = new SqlCommand("INSERT INTO tb_usuarios(nomusu, pass, tipusu, estado, correo, direcenvio, tarjeta) VALUES(@usu, @pass, @tipo, @estado, @correo, @direc, @tarjeta)", cn);
                cmd.Parameters.AddWithValue("@usu", nomusu);
                cmd.Parameters.AddWithValue("@pass", GenerateSHA512String(pass));
                cmd.Parameters.AddWithValue("@tipo", "CLIENTE");
                cmd.Parameters.AddWithValue("@estado", "A");
                cmd.Parameters.AddWithValue("@correo", " ");
                cmd.Parameters.AddWithValue("@direc", " ");
                cmd.Parameters.AddWithValue("@tarjeta", " ");


                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("SELECT idusu,tipusu FROM tb_usuarios order by idusu desc", cn);
                SqlDataReader dr = cmd2.ExecuteReader();

                if(Session["usuario"] == null)
                {
                    UsuarioSesion ususesion = new UsuarioSesion();
                    ususesion.idUsuSesion = dr.GetString(0);
                    ususesion.nomUsuSesion = nomusu;
                    ususesion.tipo = dr.GetString(1);
                    Session["usuario"] = ususesion;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: ", e.Message);
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
                cmd.Parameters.AddWithValue("@pass", GenerateSHA512String(pass));

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    if (dr.Read())
                    {
                        if (Session["usuario"] != null)
                        {
                            Session["usuario"] = null;
                        }
                        UsuarioSesion ususesion = new UsuarioSesion();
                        ususesion.idUsuSesion = dr.GetString(0);
                        ususesion.nomUsuSesion = dr.GetString(1);
                        ususesion.tipo = dr.GetString(3);
                        Session["usuario"] = ususesion;

                    }
                }else
                {
                    //ViewBag.msg = "Usuario Invalido";
                    return View(new tb_usuarios());
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
            ViewBag.msg = "SI";
            return RedirectToAction("../Funko/Index");

        }

        public ActionResult LogOut()
        {
            Session["usuario"] = null;
            return RedirectToAction("../Funko/Index");
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

    }
}