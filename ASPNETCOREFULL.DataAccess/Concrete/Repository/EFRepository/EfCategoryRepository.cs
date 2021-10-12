using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using ASPNETCOREFULL.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Repository.EFRepository
{
    public class EfCategoryRepository : EFGenericRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(FullContext fullContext):base(fullContext)
        {

        }

    }
}
