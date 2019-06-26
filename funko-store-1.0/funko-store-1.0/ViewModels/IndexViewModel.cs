using funko_store_1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace funko_store_1._0.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<tb_productos> productos { get; set; }
        public List<tb_categorias> categorias { get; set; }
    }
}