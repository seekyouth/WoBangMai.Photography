using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WoBangMai.Interface;
using WoBangMai.Models;
using WoBangMai.Repositories.CMSModule;
using System.Text;

namespace WoBangMai.Photography.Controllers
{
    public class CommentController : Controller
    {

        private ICommentRepository _icommentRepository = new CommentRepository();

        // GET: Comment
        public ActionResult Index()
        {
            List<cms_comment> list = _icommentRepository.GetList(m => m.ArticleId ==1).ToList();
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
            int Floor = 0;
            List<cms_comment> list = _icommentRepository.GetList(m=>m.ArticleId== model.ArticleId).ToList();  // 获取全部列表
            List<cms_comment> quoteList = new List<cms_comment>();  // 创建当前评论所引用的评论列表
            Addcms_comment(list, quoteList, model);       // 为当前评论的引用列表添加项目
            quoteList.Sort(cms_comment.GetComparer());  // 对列表排序，顺序排列
            foreach (cms_comment quote in quoteList)    // 生成引用的评论列表
            {
                output = String.Format(
                        "<div>{0}<span>{1} 原贴：</span><br />{2}</div>",
                        output, quote.UserId, quote.Content);
                Floor++;
            }
            // 添加当前引用
            output = String.Format(
                    "<div class='comment'><p class='title'><span>{0} {1}楼</span>{2}</p>{3}<p>{4}</p></div>",
                    model.PostDate.ToString("yyyy/MM/dd HH:mm"), model.UserId, Floor, output, model.Content);

            return output;
        }




        // 向quoteList中添加 符合条件的cms_comment
        protected void Addcms_comment(List<cms_comment> list, List<cms_comment> quoteList, cms_comment cmt)
        {
            if (cmt.ment_Id != 0)
            {
                cms_comment find = list.Find(new Predicate<cms_comment>(cmt.MatchRule));
                if (find!=null)
                {
                    quoteList.Add(find);
                    // 递归调用，只要cms_commentId不为零，就加入到引用评论列表
                    Addcms_comment(list, quoteList, find);
                }
            }
            else
                return;
        }


        public string Edit_Comment(cms_comment model)
        {
            if (ModelState.IsValid)
            {
                if (model.ment_Id == 0)
                {
                    _icommentRepository.Add(model);
                }
                else
                {
                    _icommentRepository.Update(m=>m.ment_Id==model.ment_Id,model);
                }
            }
            return "";
        }

        //// 按钮提交事件，通常是要保存到数据库
        //// 作为演示，这里使用ViewState进行持久化
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    // 从ViewState中获取 cms_comment列表
        //    List<cms_comment> list = ViewState["List"] as List<cms_comment>;

        //    cms_comment cmt = new cms_comment();
        //    cmt.ArticleId = 16;
        //    cmt.cms_commentId = Convert.ToInt32(ddlcms_commentId.SelectedValue);
        //    cmt.Content = txtContent.Text;
        //    cmt.Id = 15 + list.Count;           // 设置当前评论的Id
        //    cmt.PostDate = DateTime.Now;
        //    cmt.UserName = txtUserName.Text;

        //    // 将新评论的id添加到DropDownList中
        //    ListItem item = new ListItem(cmt.Id.ToString());
        //    ddlcms_commentId.Items.Insert(0, item);
        //    ddlcms_commentId.SelectedIndex = 0;

        //    list.Add(cmt);                                  // 添加新评论。
        //    list.Sort(cms_comment.GetComparer(false));  // 倒序排列回帖
        //                                                // ViewState["List"] = list;		这里是没有必要的，因为ViewState和list引用的是用同一个对象
        //    rpcms_comment.DataSource = list;
        //    rpcms_comment.DataBind();
        //}

    }
}