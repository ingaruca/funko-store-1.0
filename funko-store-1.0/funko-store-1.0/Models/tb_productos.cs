//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace funko_store_1._0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_productos
    {
        public string idprodu { get; set; }
        public string codbar { get; set; }
        public string nomprodu { get; set; }
        public int idcate { get; set; }
        public Nullable<int> entrada { get; set; }
        public Nullable<int> salida { get; set; }
        public decimal precio { get; set; }
        public string caracte { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public string estado { get; set; }
    
        public virtual tb_categorias tb_categorias { get; set; }
    }
}
