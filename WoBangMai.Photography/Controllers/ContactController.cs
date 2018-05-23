using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.categoryId = CategoryEnum.Contact.GetHashCode();
            TempData["currentNav"] = EnumModels.CategoryEnum.Contact.GetHashCode();
            return View();
        }

      

       
        public JsonResult AddMessage()
        {
            return null;
        }
    }
}