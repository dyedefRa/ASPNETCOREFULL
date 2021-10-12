using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Repository.EFRepository
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FullContext _fullContext;
        public EFGenericRepository(FullContext fullContext)
        {
            _fullContext = fullContext;
        }

        public T GetById(int Id)
        {
            return _fullContext.Set<T>().Find(Id);
        }

        public List<T> List()
        {
            return _fullContext.Set<T>().ToList();
        }

        public bool Add(T entity)
        {
            _fullContext.Set<T>().Add(entity);
            return SaveChanges();
        }

        public bool Delete(int Id)
        {
            _fullContext.Set<T>().Add(GetById(Id));
            return SaveChanges();
        }

        public bool Delete(T entity)
        {
            _fullContext.Set<T>().Add(entity);
            return SaveChanges();
        }

        public List<T> Expression(Func<T, bool> expression)
        {
            return _fullContext.Set<T>().Where(expression).ToList();
        }

        public bool Update(T entity)
        {
            _fullContext.Set<T>().Update(entity);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            return _fullContext.SaveChanges() > 0;
        }
    }
}
