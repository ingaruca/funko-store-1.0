using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using funko_store_1._0.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;

namespace funko_store_1._0.Controllers
{
    public class PedidosController : Controller
    {
        FUNKOBDEntities1 data = new FUNKOBDEntities1();
        // GET: Pedidos
        #region Index

        public ActionResult Index()
        {
            UsuarioSesion usu = (UsuarioSesion)Session["usuario"];

            if(usu.tipo == "CLIENTE")
            {
                List<tb_pedido> listapedidos = data.tb_pedido.Where(p => p.idusu == usu.idUsuSesion).ToList<tb_pedido>();
                return View(listapedidos);
            }

            return View(data.tb_pedido.ToList());
        }

        #endregion Index
        
        public ActionResult Detalle(int id)
        {
            UsuarioSesion usu = (UsuarioSesion)Session["usuario"];

            string s = data.tb_pedido.Where(p => p.idpedido == id).First().estado;
            ViewBag.idpedi = id;
            ViewBag.s = s;
            ViewBag.tipo = usu.tipo;

            List<tb_detapedido> listadetapedi = data.tb_detapedido.Where(pd => pd.idpedido == id ).ToList<tb_detapedido>();
            return View(listadetapedi);

        }

        public ActionResult Confirmar(int idpedi)
        {
            tb_pedido regPed = data.tb_pedido.Where(p => p.idpedido == idpedi).First();

            regPed.estado = "ENTREGADO";

            data.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idpedi)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);
            cn.Open();

            SqlTransaction tr = cn.BeginTransaction(System.Data.IsolationLevel.Serializable);

            try
            {
                string sql = "DELETE FROM tb_detapedido WHERE idpedido=@idped";
                SqlCommand cmd = new SqlCommand(sql, cn, tr);
                cmd.Parameters.Add("@idped", SqlDbType.Int).Value = idpedi;
                cmd.ExecuteNonQuery();

                string sql2 = "DELETE FROM tb_pedido WHERE idpedido=@idped";
                SqlCommand cmd2 = new SqlCommand(sql2, cn, tr);
                cmd2.Parameters.Add("@idped", SqlDbType.Int).Value = idpedi;
                cmd2.ExecuteNonQuery();

                tr.Commit();
                Console.WriteLine("Eliminado Correctamente");
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
            }
            return RedirectToAction("Index");
        }
    }
}