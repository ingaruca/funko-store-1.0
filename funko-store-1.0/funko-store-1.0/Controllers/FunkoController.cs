using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// INGARUKA LE PUSO 1.0 POR ESO EL "1._0" >:v 
using funko_store_1._0.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;

namespace funko_store_1._0.Controllers
{
    public class FunkoController : Controller
    {
        FUNKOBDEntities1 data = new FUNKOBDEntities1();

        #region Metodos
        int Autogenerar()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Autogenera", cn);
            cn.Open();
            int n = Int32.Parse(cmd.ExecuteScalar().ToString());
            cn.Close();

            return n;
        }

        decimal Monto()
        {
            List<Registro> detalle = (List<Registro>)Session["carrito"];
            decimal mt = 0;
            foreach (Registro it in detalle)
            {
                mt += it.monto;
            }
            return mt;
        }

        #endregion Metodos

        #region Index
        public ActionResult Index()
        {
            if (Session["carrito"] == null)
            {
                List<Registro> detalle = new List<Registro>();
                Session["carrito"] = detalle;
            }

            return View(data.tb_productos.ToList());
        }

        #endregion Index

        #region Seleccionar
        public ActionResult Seleccionar(string id = null)
        {
            if (Session["carrito"] == null)
            {
                List<Registro> detalle = new List<Registro>();
                Session["carrito"] = detalle;
            }

            return View(data.tb_productos.Where(x => x.idprodu == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Seleccionar(string id, int cantidad)
        {
            tb_productos reg = data.tb_productos.Where(x => x.idprodu == id).FirstOrDefault();

            Registro it = new Registro();
            it.id = reg.idprodu;
            it.nombre = reg.nomprodu;
            it.precio = reg.precio;
            it.cantidad = cantidad;
            it.monto = reg.precio * cantidad;

            List<Registro> detalle = (List<Registro>)Session["carrito"];
            detalle.Add(it);
            Session["carrito"] = detalle;

            return RedirectToAction("Index");
        }

        #endregion Seleccionar

        #region Comprar
        public ActionResult Comprar()
        {
            List<Registro> detalle = (List<Registro>)Session["carrito"];
            decimal mt = 0;
            foreach (Registro it in detalle)
            {
                mt += it.monto;
            }
            ViewBag.mt = mt;
            return View(detalle);
        }

        public ActionResult Eliminar(string id = null)
        {
            List<Registro> detalle = (List<Registro>)Session["carrito"];

            foreach (Registro it in detalle)
            {
                if (it.id == id)
                {
                    detalle.Remove(it);
                    break;
                }
            }

            Session["carrito"] = detalle;
            return RedirectToAction("comprar");
        }

        #endregion Comprar

        #region Pagar
        public ActionResult Pago()
        {
            List<Registro> detalle = (List<Registro>)Session["carrito"];
            decimal mt = 0;
            foreach (Registro it in detalle)
            {
                mt += it.monto;
            }

            ViewBag.mt = mt;
            return View(detalle);
        }

        [HttpPost]
        public ActionResult Pago(string dni = null)
        {
            if(Session["usuario"] == null)
            {
                return RedirectToAction("../Sesion/Login/");
            }

            UsuarioSesion usu = (UsuarioSesion)Session["usuario"];
            // OBTIENE EL ID DEL ULTIMO PEDIDO + 1
            int id = Autogenerar();
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);
            cn.Open();

            SqlTransaction tr = cn.BeginTransaction(System.Data.IsolationLevel.Serializable);

            try
            {
                string sql = "insert into tb_pedido (idusu,total,estado) values (@idusu,@monto,@estado)";
                SqlCommand cmd = new SqlCommand(sql, cn, tr);
                cmd.Parameters.Add("@idusu", SqlDbType.Char, 8).Value = usu.idUsuSesion;
                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = Monto();
                cmd.Parameters.Add("@estado", SqlDbType.Char, 10).Value = "PENDIENTE";
                cmd.ExecuteNonQuery();

                List<Registro> detalle = (List<Registro>)Session["carrito"];
                foreach (Registro it in detalle)
                {
                    cmd = new SqlCommand("insert into tb_detapedido (idpedido,idprodu,precio,cantidad) values (@id,@prod,@pre,@q)", cn, tr);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@prod", SqlDbType.Char, 8).Value = it.id;
                    cmd.Parameters.Add("@pre", SqlDbType.Decimal).Value = it.precio;
                    cmd.Parameters.Add("@q", SqlDbType.Int).Value = it.cantidad;
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
                Console.WriteLine("Both records are written to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                try
                {
                    tr.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            finally
            {
                cn.Close();
                Session["carrito"] = null;
                Console.WriteLine("FINALLY");
            }
            return RedirectToAction("Index");
        }
        #endregion Pagar



    }
}