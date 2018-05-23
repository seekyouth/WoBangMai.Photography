using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Interface;
using WoBangMai.Models;
using WoBangMai.Repositories;
using WoBangMai.Repositories.CMSModule;
using static WoBangMai.Photography.EnumModels;

namespace WoBangMai.Photography.Controllers
{
    public class ToolsController : BaseController
    {

        IWebConfingRepository _iwebConfingRepository = new WebConfingRepository();
        ICategoryRepository _icategoryRepository = new CategoryRepository();
        INewRepository _inewRepository = new NewRepository();
        ICommentRepository _icommentRepository = new CommentRepository();
        // GET: Tools
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult _WebConfing(int? categoryId = 0)
        {
            cms_category categoryModel = new cms_category();
            if (categoryId == 0)
            {
                var confingModel = _iwebConfingRepository.GetAll().FirstOrDefault();
                return View(confingModel);
            }
            else
            {
                ViewBag.CategoryModel = _icategoryRepository.Get(m => m.Category_ID == categoryId);
                return View();
            }

        }

        public ActionResult _Gallery(int? categoryId = 0)
        {
            var galleryList = _inewRepository.GetAll().WhereIf(m => m.NewsCategoryID == categoryId, categoryId != 0).Take(6);
            return View(galleryList);
        }


        public ActionResult _Slider()
        {
            return View();
        }


        public ActionResult _Nav()
        {
            return View();
        }


        public ActionResult _Footer()
        {
            return View();
        }


        /// <summary>
        /// Gallery首页左边导航
        /// </summary>
        /// <returns></returns>
        public ActionResult _GalleryNav()
        {
            var categorlist = _icategoryRepository.GetList(m => m.ParentID == CategoryEnum.Gallery.GetHashCode());
            return View(categorlist);
        }
        /// <summary>
        /// Gallery首页左边导航
        /// </summary>
        /// <returns></returns>
        public ActionResult _AboutNav()
        {
            var categorlist = _icategoryRepository.GetList(m => m.ParentID == CategoryEnum.About.GetHashCode());
            return View(categorlist);
        }


        /// <summary>
        /// 首页作品集
        /// </summary>
        /// <returns></returns>
        public ActionResult _GalleryHomeIndex()
        {
            int total = 0;
            StringBuilder whereSql = new StringBuilder();
            string parameter = "";
            List<cms_category> categoryList = new List<cms_category>();
            _icategoryRepository.GetChildList(CategoryEnum.Gallery.GetHashCode(), ref categoryList);
            if (categoryList.Count() > 0)
            {
                categoryList.ForEach(a => parameter += a.Category_ID + ",");
                parameter += -1;
                whereSql.AppendFormat(" NewsCategoryID in ({0})", parameter);
            }
            var newlist = _inewRepository.GetModelListWithPaging("", 1, 6, whereSql.ToString(), "", "", "", out total);
            foreach (var item in newlist)
            {
                item.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(item.NewsPic) == true ? "" : item.NewsPic.Replace(",", ""));
            }
            return View(newlist);
        }

        /// <summary>
        /// News左边作品集
        /// </summary>
        /// <returns></returns>
        public ActionResult _GalleryNewsSide()
        {
            int total = 0;
            StringBuilder whereSql = new StringBuilder();
            string parameter = "";
            List<cms_category> categoryList = new List<cms_category>();
            _icategoryRepository.GetChildList(CategoryEnum.Gallery.GetHashCode(), ref categoryList);
            if (categoryList.Count() > 0)
            {
                categoryList.ForEach(a => parameter += a.Category_ID + ",");
                parameter += -1;
                whereSql.AppendFormat(" NewsCategoryID in ({0})", parameter);
            }
            var newlist = _inewRepository.GetModelListWithPaging("", 1, 2, whereSql.ToString(), "", "", "", out total);
            foreach (var item in newlist)
            {
                item.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(item.NewsPic) == true ? "" : item.NewsPic.Replace(",", ""));
            }


            return View(newlist);
        }

        /// <summary>
        /// News部分联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult _ContactNewsSide()
        {
            var model = _inewRepository.GetList(m => m.NewsCategoryID == CategoryEnum.Contact.GetHashCode()).FirstOrDefault();
            return View(model);
        }

        /// <summary>
        /// 首页质询
        /// </summary>
        /// <returns></returns>
        public ActionResult _NewsHomeIndex()
        {
            int total = 0;
            ViewBag.categoryId = CategoryEnum.News.GetHashCode();
            TempData["currentNav"] = CategoryEnum.News.GetHashCode();
            StringBuilder whereSql = new StringBuilder();
            whereSql.AppendFormat(" NewsCategoryID in ({0})", CategoryEnum.News.GetHashCode());
            var newsList = _inewRepository.GetModelListWithPaging("", 1, 6, whereSql.ToString(), "", "", "", out total);
            foreach (var item in newsList)
            {
                item.NewsPic = DataSupplierUrl + (string.IsNullOrEmpty(item.NewsPic) == true ? "" : item.NewsPic.Replace(",", ""));
            }
            return View(newsList);

        }

        /// <summary>
        /// 首页Abaout页面
        /// </summary>
        /// <returns></returns>
        public ActionResult _AboutHomeIndex()
        {
            List<cms_category> categoryList = new List<cms_category>();
            int category_ID = CategoryEnum.About.GetHashCode();
            StringBuilder whereSql = new StringBuilder();
            _icategoryRepository.GetChildList((int)category_ID, ref categoryList);
            if (categoryList.Count() > 0)
            {
                category_ID = categoryList.FirstOrDefault().Category_ID;
            }
            var aboutModel = _inewRepository.Get(m => m.NewsCategoryID == category_ID);
            return View(aboutModel);
        }

        /// <summary>
        ///  评论
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult _Comment(int Id)
        {
            int total = 0;
            List<cms_comment> list = _icommentRepository.GetModelListWithPaging("", 1, 10, "", "", "", "", out total);
            StringBuilder commentStr = new StringBuilder();
            foreach (var item in list)
            {
                commentStr.AppendFormat("{0}", GetContent(item));
            }
            ViewBag.commentStr = commentStr.ToString();
            return View();
        }




        // 根据当前的cms_comment得到HTML输出
        protected string GetContent(cms_comment model)
        {
            string output = "";
            int Floor = 1;
            List<cms_comment> list = _icommentRepository.GetList(m => m.ArticleId == 1002).ToList();  // 获取全部列表
            List<cms_comment> quoteList = new List<cms_comment>();  // 创建当前评论所引用的评论列表
            Addcms_comment(list, quoteList, model);       // 为当前评论的引用列表添加项目
            quoteList.Sort(cms_comment.GetComparer());  // 对列表排序，顺序排列
            foreach (cms_comment quote in quoteList)    // 生成引用的评论列表
            {
                output = String.Format(
                        "<div>{0}<span>{1} 原贴：</span><br />{2}</div>",
                        output, quote.UserName, quote.Content);
                Floor++;
            }
            // 添加当前引用
            output = String.Format(
                    "<div class='comment'><p class='title'><span>{0} {1}楼</span>{2}</p>{3}<p>{4}</p></div>",
                    model.PostDate.ToString("yyyy/MM/dd HH:mm"), Floor, model.UserName, output, model.Content);

            return output;
        }




        // 向quoteList中添加 符合条件的cms_comment
        protected void Addcms_comment(List<cms_comment> list, List<cms_comment> quoteList, cms_comment cmt)
        {
            if (cmt.ment_Id != 0)
            {
                cms_comment find = list.Find(new Predicate<cms_comment>(cmt.MatchRule));
                if (find != null)
                {
                    quoteList.Add(find);
                    // 递归调用，只要cms_commentId不为零，就加入到引用评论列表
                    Addcms_comment(list, quoteList, find);
                }
            }
            else
                return;
        }
    }
}