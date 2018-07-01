using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using funko_store_1._0.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace funko_store_1._0.Controllers
{
    public class UsuarioController : Controller
    {
        Procedimientos pro = new Procedimientos();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(pro.LISTARUSUARIO());
        }

        public ActionResult Create()
        {
            return View(new tb_usuarios());
        }

        [HttpPost]
        public ActionResult Create(tb_usuarios objusu)
        {
            if (!ModelState.IsValid)
            {
                return View(objusu);
            }

            pro.INSERTARUSUARIOS(objusu);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            tb_usuarios xpro = pro.DETALLEUSUARIO(id);
            return View(xpro);
        }

        [HttpPost]
        public ActionResult Edit(tb_usuarios objusu)
        {
            if (!ModelState.IsValid)
            {
                //this.Request.Form["estado"];
                return View(objusu);
            }

            pro.ACTUALIZARUSUARIO(objusu);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            return View(pro.DETALLEUSUARIO(id));
        }

        public ActionResult Delete(string id)
        {
            pro.ELIMINARUSUARIO(id);
            return RedirectToAction("Index");
        }
    }
}