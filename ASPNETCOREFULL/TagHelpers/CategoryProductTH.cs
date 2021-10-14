using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.TagHelpers
{
    [HtmlTargetElement("getCategoryName")]
    public class CategoryProductTH : TagHelper
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryProductTH(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public int CategoryId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //TagBuilder tagBuilder = new TagBuilder("");

            var productNames = _categoryRepository.GetById(CategoryId).Products.Select(x => x.Name);
            base.Process(context, output);
        }
    }
}
