using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WoBangMai.Interface;
using WoBangMai.Models;
using WoBangMai.Repositories;
using WoBangMai.Utils;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class AboutController : BaseController
    {
        INewRepository _inewRepository = new NewRepository();
        ICategoryRepository _icategoryRepository = new CategoryRepository();
        // GET: News
        public ActionResult Index(PageModel model, int? id = 0)
        {
            List<cms_category> categoryList = new List<cms_category>();
            int total = 0;
            StringBuilder whereSql = new StringBuilder();
            id = (id == 0 ? CategoryEnum.About.GetHashCode() : (int)id);
            _icategoryRepository.GetChildList((int)id, ref categoryList);
            if (categoryList.Count() > 0)
            {
                //categoryList.ForEach(a => parameter += a.Category_ID + ",");
                //parameter += 0;
                id = categoryList.FirstOrDefault().Category_ID;
                whereSql.AppendFormat(" NewsCategoryID in ({0})", id);
            }
            else
                whereSql.AppendFormat(" NewsCategoryID in ({0})", id);
            var aboutModel = _inewRepository.GetModelListWithPaging("", model.PageIndex, model.PageSize, whereSql.ToString(), "", "", "", out total).FirstOrDefault();
            if (aboutModel!=null)
            {
                aboutModel.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(aboutModel.NewsPic) == true ? "" : aboutModel.NewsPic.Replace(",", ""));
                aboutModel.NewsContent = RexImage.ReplaseImgSrc(aboutModel.NewsContent, DataSupplierUrl);
            }
            TempData["currentNav"] = CategoryEnum.About.GetHashCode();
            TempData["currentchildNav"] = id;
            ViewBag.categoryId = CategoryEnum.About.GetHashCode();
            return View(aboutModel);

        }

    }
}