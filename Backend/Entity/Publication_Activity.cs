//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Backend.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publication_Activity
    {
        public int idPublication { get; set; }
        public int idActivity { get; set; }
        public string description { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
