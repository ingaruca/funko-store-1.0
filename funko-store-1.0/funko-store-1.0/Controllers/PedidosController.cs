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
    }
}