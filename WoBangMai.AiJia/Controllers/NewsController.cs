using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WoBangMai.SDK.CMS;

namespace WoBangMai.AiJia.Controllers
{
    public class NewsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">分类编号</param>
        /// <returns></returns>
        // GET: NewsList
        public ActionResult Index(int?id =0,int page=1,int pagesize = 20)
        {
            string parameter = string.Format("category_id={0}&page={1}&size{2}", id, page, pagesize);
            var list = NewsAPI.GetList("", parameter);
            return View(list);
        
        }

        public ActionResult Details(int?id=0)
        {
            return View();
        }
    }
}