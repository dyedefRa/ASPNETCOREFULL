using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using ASPNETCOREFULL.DataAccess.Concrete.Identity;
using ASPNETCOREFULL.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository, SignInManager<AppUser> signInManager)
        {
            _productRepository = productRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            int a = 0;
            int x = 5 / a;
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

        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre, model.BeniHatirla, false).Result;

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                    //return RedirectToAction("Index", "Admin/Home");
                }

                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre hatalı...");
            }

            return View(model);

        }

        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;

            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var logger = LogManager.GetLogger("FileLogger");
            logger.Log(LogLevel.Error, $"\n Hatanın Gerçekleştiği Yer:{errorInfo.Path} \n HataMesajı : {errorInfo.Error.Message} \nStack Trace:{errorInfo.Error.StackTrace} ");
            return View();
        }
    }
}
