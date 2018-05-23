using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Models;
using WoBangMai.SDK.CMS;

namespace WoBangMai.AiJia.Controllers
{
    public class PartController : Controller
    {
        // GET: Tools
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult _Nav(int? cateId = 0)
        {
            var channel_id = 1;
            string parameter = string.Format("channel_id={0}", channel_id);
            var list = CategoryAPI.GetList("", parameter);
            return View(list);
        }
        public ActionResult _Banner(int? cateId = 0)
        {
            return View();
        }

        public ActionResult _PageTarget()
        {
            return View();
        }


        public ActionResult _Product()
        {
            return View();
        }
     

        public ActionResult _AboutUs()
        {
            return View();
        }

        public ActionResult _Works()
        {
            return View();
        }


        public ActionResult _Servers()
        {
            return View();
        }

        public ActionResult _Activity()
        {
            return View();
        }


        public ActionResult _News()
        {
            ViewBag.categoryList = new List<Category>();
            var modelList = new List<News>();
            return View();
        }

        public ActionResult _Contact()
        {
            var model = new WebConfig();
            return View(model);
        }

        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public ActionResult _Link()
        {
            return View();
        }

        public ActionResult _Footer()
        {
            return View();
        }

        public ActionResult _Shares()
        {
            return View();
        }

        public ActionResult _Fixed()
        {
            return View();
        }

        public ActionResult _OnlineOpen()
        {
            return View();
        }

        public ActionResult _OnlineLx()
        {
            return View();
        }

        public ActionResult _OpenAssist()
        {
            return View();
        }

        public ActionResult _AssistBtn()
        {
            return View();
        }
        
    }
}