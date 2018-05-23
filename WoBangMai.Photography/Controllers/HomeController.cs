using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WoBangMai.Photography.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
          TempData["currentNav"]= EnumModels.CategoryEnum.Home.GetHashCode();
            return View();
        }

        public ActionResult Index2()
        {
            TempData["currentNav"] = EnumModels.CategoryEnum.Home.GetHashCode();
            return View();
        }
    }
}