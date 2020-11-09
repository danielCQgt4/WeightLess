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
        public bool autoClose;


        public UnitWork(DBContext _context) {
            autoClose = false;
            context = _context;
            genericDAL = new DALGenericImpl<E>(context);
        }

        public UnitWork() {
            autoClose = true;
            context = new DBContext();
            genericDAL = new DALGenericImpl<E>(context);
        }

        public bool Complete() {
            try {
                context.SaveChanges();
                return true;
            } catch (Exception) {

                return false;
            }

        }

        public void Dispose() {
            if (autoClose) {
                context.Dispose();
            }
        }

        public void Close() {
            context.Dispose();
        }

    }
}
