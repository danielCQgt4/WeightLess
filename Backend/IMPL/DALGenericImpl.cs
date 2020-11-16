using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {

    public class DALGenericImpl<E> : IDALGeneric<E> where E : class {

        protected readonly DBContext Context;

        public DALGenericImpl(DBContext context) {
            Context = context;
        }


        public bool Add(E entity) {
            try {
                Context.Set<E>().Add(entity);
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public void AddRange(IEnumerable<E> entities) {
            try {
                Context.Set<E>().AddRange(entities);
            } catch (Exception) {

                throw;
            }
        }

        public IEnumerable<E> Find(Expression<Func<E, bool>> predicate) {
            try {
                return Context.Set<E>().Where(predicate);
            } catch (Exception) {

                return null;
            }
        }

        public E Get(int id) {
            try {
                return Context.Set<E>().Find(id);
            } catch (Exception) {

                return null;
            }
        }

        public IEnumerable<E> GetAll() {
            try {
                return Context.Set<E>().ToList();
            } catch (Exception) {

                return null;
            }
        }

        public bool Remove(E entity) {
            try {
                Context.Set<E>().Attach(entity);
                Context.Set<E>().Remove(entity);
                return true;
            } catch (Exception) {

                return false;
            }
        }

        public void RemoveRange(IEnumerable<E> entities) {
            try {
                Context.Set<E>().RemoveRange(entities);
            } catch (Exception) {

                throw;
            }
        }

        public E SingleOrDefault(Expression<Func<E, bool>> predicate) {
            try {
                return Context.Set<E>().SingleOrDefault(predicate);
            } catch (Exception) {

                return null;
            }
        }

        public bool Update(E entity) {
            try {
                Context.Entry(entity).State = EntityState.Modified;
                return true;
            } catch (Exception) {

                return false;
            }
        }
    }
}
