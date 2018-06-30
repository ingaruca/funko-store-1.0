using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace funko_store_1._0.Models
{
    public class Productos
    {
        public string idprodu { get; set; }
        public string codbar { get; set; }
        public string nomprodu { get; set; }
        public int idcate { get; set; }
        public string nomcate { get; set; }
        public int entrada { get; set; }
        public int salida { get; set; }
        public decimal precio { get; set; }
        public string caracte { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public string estado { get; set; }


    }
}