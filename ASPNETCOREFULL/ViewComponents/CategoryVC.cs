using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.ViewComponents
{
    public class CategoryVC : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryVC(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var model = _categoryRepository.List();
            return View(model);
        }
    }
}
