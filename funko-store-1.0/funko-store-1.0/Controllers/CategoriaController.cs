using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using funko_store_1._0.Models;
using System.Data.SqlClient;
using System.Data.Sql;

namespace funko_store_1._0.Controllers
{
    public class CategoriaController : Controller
    {
        FUNKOBDEntities1 data = new FUNKOBDEntities1();
        // GET: Categoria
        public ActionResult Index()
        {
            return View(data.tb_categorias.ToList());
        }

        public ActionResult Create()
        {
            return View(new tb_categorias());
        }

        [HttpPost]
        public ActionResult Create(tb_categorias objCat)
        {
            if (!ModelState.IsValid)
            {
                return View(new tb_categorias());
            }

            data.tb_categorias.Add(objCat);
            data.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            tb_categorias regCat = data.tb_categorias.Where(c => c.idcate == id).First();
            return View(regCat);
        }

        [HttpPost]
        public ActionResult Edit(tb_categorias objCat)
        {
            if (!ModelState.IsValid)
            {
                return View(objCat);
            }

            tb_categorias regCat = data.tb_categorias.Where(c => c.idcate == objCat.idcate).First();

            regCat.nomcate = objCat.nomcate;
            regCat.descripcion = objCat.descripcion;
            regCat.estado = objCat.estado;

            data.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            tb_categorias regCat = data.tb_categorias.Where(c => c.idcate == id).First();

            data.tb_categorias.Remove(regCat);

            data.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Desactivar(int id)
        {
            tb_categorias regCat = data.tb_categorias.Where(c => c.idcate == id).First();
  
            if(regCat.estado == "A")
            {
                regCat.estado = "I";
            }else
            {
                regCat.estado = "A";
            }
       
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            tb_categorias regCat = data.tb_categorias.Where(c => c.idcate == id).First();

            return View(regCat);
        }
    }
}