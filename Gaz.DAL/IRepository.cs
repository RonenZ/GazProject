using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Gaz.DAL
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(string include = "");
        T GetByID(int id, params Expression<Func<T, object>>[] includes);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
        void Delete(int id);
    }
}
