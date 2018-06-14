using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace funko_store_1._0.Models
{
    public class Registro
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get; set; }
    }
}