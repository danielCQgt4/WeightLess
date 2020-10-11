using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {

    public interface IDALGeneric<E> where E : class {

        E Get(int id);
        IEnumerable<E> GetAll();
        IEnumerable<E> Find(Expression<Func<E, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        E SingleOrDefault(Expression<Func<E, bool>> predicate);

        bool Add(E entity);
        void AddRange(IEnumerable<E> entities);

        bool Update(E entity);
        bool Remove(E entity);
        void RemoveRange(IEnumerable<E> entities);

    }
}
