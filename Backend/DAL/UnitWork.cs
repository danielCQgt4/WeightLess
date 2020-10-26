using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {

    public class UnitWork<E> : IDisposable where E : class {

        public readonly DBContext context;
        public IDALGeneric<E> genericDAL;


        public UnitWork(DBContext _context) {
            context = _context;
            genericDAL = new DALGenericImpl<E>(context);
        }

        public UnitWork() {
            context = new DBContext();
            genericDAL = new DALGenericImpl<E>(context);
        }

        public bool Complete() {
            try {
                context.SaveChanges();
                return true;
            } catch(Exception) {

                return false;
            }

        }

        public void Dispose() {
            context.Dispose();
        }

    }
}
