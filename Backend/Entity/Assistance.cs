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
    
    public partial class Assistance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Assistance()
        {
            this.Activity_Assitance = new HashSet<Activity_Assitance>();
        }
    
        public int idAssistance { get; set; }
        public System.DateTime date { get; set; }
        public System.DateTime time { get; set; }
        public int idUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity_Assitance> Activity_Assitance { get; set; }
        public virtual User User { get; set; }
    }
}
