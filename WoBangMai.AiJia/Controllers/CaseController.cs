using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WoBangMai.AiJia.Controllers
{
    public class CaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">分类编号</param>
        /// <returns></returns>
        // GET: NewsList
        public ActionResult Index(int? id = 0)
        {
            return View();
        }

        public ActionResult Details(int? id = 0)
        {
            return View();
        }
    }
}