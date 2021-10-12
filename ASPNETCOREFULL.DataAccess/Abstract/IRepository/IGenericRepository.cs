using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Abstract.IRepository
{
    public interface IGenericRepository<T> where T:class
    {
        T GetById(int Id);
        List<T> List();
        bool Add(T entity);
        bool Update(T entity);
        List<T> Expression(Func<T,bool> expression);
        bool Delete(int Id);
        bool Delete(T entity);
        bool SaveChanges();
    }

}
