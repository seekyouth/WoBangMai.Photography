using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Interface;
using WoBangMai.Repositories;
using WoBangMai.Utils;
using System.Text;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class OtherController : BaseController
    {
        INewRepository _inewRepository = new NewRepository();

        // GET: News
        public ActionResult Index(string newsTitle, int? page = 1, int? pageSize = 1)
        {
            int total = 0;
            ViewBag.categoryId = CategoryEnum.Other.GetHashCode();
            TempData["currentNav"] = CategoryEnum.Other.GetHashCode();
            StringBuilder whereSql = new StringBuilder();
             whereSql.AppendFormat(" NewsCategoryID in ({0})", CategoryEnum.Other.GetHashCode());
            var aboutModel = _inewRepository.GetModelListWithPaging("", (int)page, (int)pageSize, whereSql.ToString(), "", "", "", out total).FirstOrDefault();
            if (aboutModel != null)
            {
                aboutModel.NewsContent = RexImage.ReplaseImgSrc(aboutModel.NewsContent, DataSupplierUrl);
            }
            return View(aboutModel);
        }
    }
}