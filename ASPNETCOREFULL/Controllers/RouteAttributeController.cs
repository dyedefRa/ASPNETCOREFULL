using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Controllers
{
    [Route("routeAttr/[action]")]
    public class RouteAttributeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
