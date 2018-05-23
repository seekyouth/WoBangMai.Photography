using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Interface;
using WoBangMai.Models;
using WoBangMai.Repositories;
using WoBangMai.Utils;
using System.Text;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class GalleryController : BaseController
    {

        ICategoryRepository _icategoryRepository = new CategoryRepository();
        INewRepository _inewRepository = new NewRepository();
        string parameter = "";
        List<cms_category> categoryList = new List<cms_category> ();

        // GET: Gallery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id">分类编号</param>
        /// <returns></returns>
        public ActionResult Index(PageModel model, int? id = 0)
        {
            int total = 0;
            StringBuilder whereSql = new StringBuilder();
            id = (id == 0 ? CategoryEnum.Gallery.GetHashCode() : (int)id);
            _icategoryRepository.GetChildList((int)id, ref categoryList);
            if (categoryList.Count() > 0)
            {
                //categoryList.ForEach(a => parameter += a.Category_ID + ",");
                //parameter += -1;
                id = categoryList.FirstOrDefault().Category_ID;
                whereSql.AppendFormat(" NewsCategoryID in ({0})", id);
            }
            else
                whereSql.AppendFormat(" NewsCategoryID in ({0})", id);

            var galleryList = _inewRepository.GetModelListWithPaging("", model.PageIndex, model.PageSize, whereSql.ToString(), "", "", "", out total);
            foreach (var item in galleryList)
            {
                item.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(item.NewsPic) == true ? "" : item.NewsPic.Replace(",", ""));
            }

            TempData["currentNav"] = CategoryEnum.Gallery.GetHashCode();
            TempData["currentchildNav"] = id;
            ViewBag.categoryId = CategoryEnum.Gallery.GetHashCode();
            return View(model.Record<cms_news>(galleryList, total));
        }



        public ActionResult Details(int? id = 0)
        {
            var model = _inewRepository.Get(m => m.News_ID == id);
            TempData["currentNav"] = EnumModels.CategoryEnum.Gallery.GetHashCode();
            if (model != null)
            {
                TempData["currentchildNav"] = model.NewsCategoryID;
                model.NewsContent = RexImage.ReplaseImgSrc(model.NewsContent, DataSupplierUrl);
            }
            return View(model);
        }



    }
}