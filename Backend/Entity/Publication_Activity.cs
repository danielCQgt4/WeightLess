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
    
    public partial class Publication_Activity
    {
        public int idPublication { get; set; }
        public int idActivity { get; set; }
        public string description { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
