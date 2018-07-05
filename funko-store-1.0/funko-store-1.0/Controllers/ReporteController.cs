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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;

namespace funko_store_1._0.Controllers
{
    public class ReporteController : Controller
    {
        FUNKOBDEntities1 data = new FUNKOBDEntities1();
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnFunko"].ConnectionString);

        // GET: Reporte

        public ActionResult Index(string idusu = null, string fecha1 = null, string fecha2 = null)
        {
            if (Session["usuario"] == null)
            {
                return RedirectToAction("Index", "Funko");
            }

            List<tb_pedido> listapedidos = new List<tb_pedido>();

            if(idusu == "")
            {
               return View(Fechas(fecha1, fecha2));
            }


            return View(Usuario(idusu));  
        }

        public List<tb_pedido> Usuario(string idusu)
        {
            //if (Session["usuario"] == null)
            //{
            //  return RedirectToAction("Index", "Funko");
            //}

            ViewBag.usuario = new SelectList(data.tb_usuarios.ToList(), "idusu", "nomusu", idusu);

            UsuarioSesion usu = (UsuarioSesion)Session["usuario"];
            List<tb_pedido> listapedidos = new List<tb_pedido>();

            cn.Open();

            try
            {

                string sql = "select idpedido,fecpedido,nomusu,total,p.estado from tb_pedido p join tb_usuarios u on p.idusu=u.idusu where p.idusu=@idusu ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@idusu", idusu);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tb_pedido p = new tb_pedido();
                    p.idpedido = dr.GetInt32(0);
                    p.fecpedido = dr.GetDateTime(1);
                    p.idusu = dr.GetString(2);
                    p.total = dr.GetDecimal(3);
                    p.estado = dr.GetString(4);

                    listapedidos.Add(p);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                cn.Close();
            }

            return listapedidos;
        }

        public List<tb_pedido> Fechas(string fecha1, string fecha2)
        {
            //if (Session["usuario"] == null)
            //{
              //  return RedirectToAction("Index", "Funko");
            //}

            ViewBag.usuario = new SelectList(data.tb_usuarios.ToList(), "idusu", "nomusu");

            UsuarioSesion usu = (UsuarioSesion)Session["usuario"];
            List<tb_pedido> listapedidos = new List<tb_pedido>();

            cn.Open();

            try
            {

                string sql = "select idpedido,fecpedido,nomusu,total,p.estado from tb_pedido p join tb_usuarios u on p.idusu=u.idusu where cast(fecpedido as date) between @fecha1 and @fecha2 ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@fecha1", fecha1);
                cmd.Parameters.AddWithValue("@fecha2", fecha2);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tb_pedido p = new tb_pedido();
                    p.idpedido = dr.GetInt32(0);
                    p.fecpedido = dr.GetDateTime(1);
                    p.idusu = dr.GetString(2);
                    p.total = dr.GetDecimal(3);
                    p.estado = dr.GetString(4);

                    listapedidos.Add(p);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                cn.Close();
            }

            return listapedidos;
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string nomCliente)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(nomCliente);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 100f, 0f);
                //pdfDoc.Add(new Paragraph("LISTADO DE REPORTES", FontFactory.GetFont("arial", 22, Font.ITALIC, BaseColor.CYAN)));
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Reporte.pdf");
            }
        }
    }
}