//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Backend.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity_Assitance
    {
        public int idActivityAssistance { get; set; }
        public int idActivity { get; set; }
        public int idAssistance { get; set; }
        public System.DateTime start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
        public decimal kcal { get; set; }
        public string timeOcurred { get; set; }
        public bool status { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Assistance Assistance { get; set; }
    }
}
