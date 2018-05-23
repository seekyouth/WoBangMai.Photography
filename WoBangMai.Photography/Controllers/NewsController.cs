using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Interface;
using WoBangMai.Models;
using WoBangMai.Repositories;
using WoBangMai.Utils;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class NewsController : BaseController
    {
        INewRepository _inewRepository = new NewRepository();
     

        // GET: News
        public ActionResult Index(PageModel model, int? id)
        {

            int total = 0;
            ViewBag.categoryId = CategoryEnum.News.GetHashCode();
            TempData["currentNav"]=CategoryEnum.News.GetHashCode();
            StringBuilder whereSql=new StringBuilder();
            whereSql.AppendFormat(" NewsCategoryID in ({0})",CategoryEnum.News.GetHashCode());
            var newsList = _inewRepository.GetModelListWithPaging("", model.PageIndex, model.PageSize, whereSql.ToString(), "", "", "", out total);
            foreach (var item in newsList)
            {
                item.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(item.NewsPic) == true ? "" : item.NewsPic.Replace(",", ""));
            }
            return View(model.Record<cms_news>(newsList, total));
        }


        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id = 0)
        {
            var model = _inewRepository.Get(m => m.News_ID == id);
            TempData["currentNav"] = EnumModels.CategoryEnum.News.GetHashCode();
            if (model != null)
            {
                TempData["currentchildNav"] = model.NewsCategoryID;
                model.NewsContent = RexImage.ReplaseImgSrc(model.NewsContent, DataSupplierUrl);
            }
            return View(model);
        }



    }
}