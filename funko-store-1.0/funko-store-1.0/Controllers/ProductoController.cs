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
    public class ProductoController : Controller
    {

        FUNKOBDEntities1 data = new FUNKOBDEntities1();
        Procedimientos pro = new Procedimientos();

        // GET: Producto
        public ActionResult Index()
        {
            return View(pro.listarProdcutos());
        }

        public ActionResult Create()
        {
            ViewBag.tb_categorias = new SelectList(pro.listarCategorias().ToList(), "idcate", "nomcate");
            return View(new Productos());
        }

        [HttpPost]
        public ActionResult Create(Productos objpro, HttpPostedFileBase imagen)
        {
            string img = "";
            if (imagen != null)
            {
                if (imagen.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("../Imagenes/"), imagen.FileName);

                    //ViewBag.myimage = imagen.FileName;
                    img = imagen.FileName;
                    imagen.SaveAs(path);
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.tb_categorias = new SelectList(pro.listarCategorias().ToList(), "idcate", "nomcate");
                return View(objpro);
            }

            objpro.imagen = img;
            pro.insertarProducto(objpro);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Productos xpro = pro.DetalleProducto(id);

            ViewBag.tb_categorias = new SelectList(pro.listarCategorias().ToList(), "idcate", "nomcate", xpro.idcate);

            return View(xpro);
        }

        [HttpPost]
        public ActionResult Edit(Productos objpro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tb_categorias = new SelectList(pro.listarCategorias().ToList(), "idcate", "nomcate", objpro.idcate);
                return View(objpro);
            }

            pro.actualizaProducto(objpro);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            return View(pro.DetalleProducto(id));
        }

        public ActionResult Delete(string id)
        {
            pro.eliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}