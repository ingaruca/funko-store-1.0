using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
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
        Procedimientos proce = new Procedimientos();

        // GET: Producto
        public ActionResult Index()
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Login", "Sesion");
            }

            return View(proce.listarProdcutos());
        }

        public ActionResult Create()
        {
            ViewBag.tb_categorias = new SelectList(proce.listarCategorias().ToList(), "idcate", "nomcate");
            ViewBag.Opciones = proce.Estados();
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
                ViewBag.tb_categorias = new SelectList(proce.listarCategorias().ToList(), "idcate", "nomcate");
                return View(objpro);
            }

            objpro.imagen = img;
            string msg = proce.insertarProducto(objpro);
            Debug.WriteLine(msg);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Productos xpro = proce.DetalleProducto(id);

            ViewBag.tb_categorias = new SelectList(proce.listarCategorias().ToList(), "idcate", "nomcate", xpro.idcate);
            ViewBag.Opciones = proce.Estados();

            return View(xpro);
        }

        [HttpPost]
        public ActionResult Edit(Productos objpro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tb_categorias = new SelectList(proce.listarCategorias().ToList(), "idcate", "nomcate", objpro.idcate);
                return View(objpro);
            }

            if (objpro.imagen == null)
            {
                Productos xpro = proce.DetalleProducto(objpro.idprodu);
                objpro.imagen = xpro.imagen;
            }

            Debug.WriteLine("Imagen: " + objpro.imagen);
            string msg = proce.actualizaProducto(objpro);
            Debug.WriteLine(msg);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            return View(proce.DetalleProducto(id));
        }

        public ActionResult Delete(string id)
        {
            string msg = proce.eliminarProducto(id);
            Debug.WriteLine(msg);
            return RedirectToAction("Index");
        }

    }
}