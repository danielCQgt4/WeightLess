﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Activity_Assitance> Activity_Assitance { get; set; }
        public virtual DbSet<Assistance> Assistance { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<Publication_Activity> Publication_Activity { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserDataHistory> UserDataHistory { get; set; }
    
        public virtual ObjectResult<User> validate_login(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User>("validate_login", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<User> validate_login(string email, string password, MergeOption mergeOption)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User>("validate_login", mergeOption, emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<sp_Report_Assistance_Result> sp_Report_Assistance(Nullable<System.DateTime> date)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Report_Assistance_Result>("sp_Report_Assistance", dateParameter);
        }
    }
}
