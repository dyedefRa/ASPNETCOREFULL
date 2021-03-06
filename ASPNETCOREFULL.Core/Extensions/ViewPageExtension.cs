using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ASPNETCOREFULL.Core.Extensions
{
    public static class ViewPageExtensions
    {
        private const string ScriptblockBuilder = "ScriptBlockBuilder";

        public static MvcHtmlString ScriptBlock(this WebViewPage webPage, Func<dynamic, HelperResult> template)
        {
            if (!webPage.IsAjax)
            {
                //var scriptBuilder = webPage.Context.Items[ScriptblockBuilder] as StringBuilder ?? new StringBuilder();

                //scriptBuilder.Append(template(null).ToHtmlString());
                //webPage.Context.Items[ScriptblockBuilder] = scriptBuilder;

                return new MvcHtmlString(string.Empty);
            }

            return new MvcHtmlString(template(null).ToHtmlString());
        }

        public static MvcHtmlString WriteScriptBlocks(this WebViewPage webPage)
        {
            var scriptBuilder = "";

            return new MvcHtmlString(scriptBuilder.ToString());
        }
    }
}
