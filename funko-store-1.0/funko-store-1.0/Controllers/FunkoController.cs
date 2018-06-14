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
    public class FunkoController : Controller
    {
        FUNKOBDEntities data = new FUNKOBDEntities();

        #region Metodos



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
        public ActionResult Seleccionar(string id)
        {
            if (Session["carrito"] == null)
            {
                List<Registro> detalle = new List<Registro>();
                Session["carrito"] = detalle;
            }

            return View(data.tb_productos.Where(x => x.idprodu == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Seleccionar(string id,string nada )
        {
            tb_productos reg = data.tb_productos.Where(x => x.idprodu == id).FirstOrDefault();

            Registro it = new Registro();
            it.id = reg.idprodu;
            it.nombre = reg.nomprodu;
            it.precio = reg.precio;
            it.cantidad = 1;
            it.monto = reg.precio;

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
    }
}