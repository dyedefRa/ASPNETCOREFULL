using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Repository.DapperRepository
{
    public class DPCategoryRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly SqlConnection _connection;

        public DPCategoryRepository(IConfiguration config)
        {
            string conStr = config.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(conStr);
        }

        public bool Add(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> Expression(Func<T, bool> expression)
        {
            throw new NotImplementedException();
        }

        public T GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            return _connection.Update<T>(entity);
        }
    }
}
