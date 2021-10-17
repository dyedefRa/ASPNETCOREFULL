using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using ASPNETCOREFULL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            //5- TagHelper burdaki viewda kullandık
            var model = _productRepository.List();

            SetCookie("kisi", "bennCookie");
            SetSession("kisi", "bennSession");
            return View(model);
        }

        public IActionResult Add(int id)
        {
            var myCookie = GetCookie("kisi");
            var mySession = GetSession("kisi");

            return View();
        }

        #region Cookie Field
        public void SetCookie(string key, string value)
        {
            HttpContext.Response.Cookies.Append(key, value);
        }

        public string GetCookie(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            return value;
        }
        #endregion

        #region Session Field 
        //Startupda servislere Session ekledik ve middleware de kullandık
        public void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);

        }
        #endregion

        public IActionResult GirisYap()
        {
            KullaniciGirisModel model = new KullaniciGirisModel();
            return View(model);
        }


    }
}
