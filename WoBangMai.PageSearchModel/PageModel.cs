using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.WebPages;

namespace System.Web.Mvc
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel
    {
        public PageModel()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            SearchQuery = new List<ConditionItem>();
            Id = Guid.NewGuid().ToString().Replace(@"/", "").Replace(@"\", "").Replace("-", "_");
        }
        /// <summary>
        /// 页的唯一标识
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 页码从1开始
        /// </summary>
        public virtual int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public virtual int PageSize { get; set; }
        /// <summary>
        /// 页总数
        /// </summary>
        public virtual long PageTotal
        {
            get
            {
                #region
                if (DataTotal == 0)
                {
                    return 0;
                }
                else if (DataTotal <= PageSize)
                {
                    return 1;
                }
                else
                {
                    var z = DataTotal / PageSize;
                    var y = DataTotal % PageSize;
                    if (y != 0)
                    {
                        return z + 1;
                    }
                    else
                    {
                        return z;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public virtual long DataTotal { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public virtual List<ConditionItem> SearchQuery { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual QueryOrder Order { get; set; }
        /// <summary>
        /// 是否全选
        /// </summary>
        public bool SelectAll { get; set; }
        /// <summary>
        /// 选择的查询集合
        /// </summary>
        public IList<string> SelectPrimaryKeys { get; set; }

        /// <summary>
        /// 判断查询字段是存在
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public bool CheckSearchName<T>(Expression<Func<T, object>> expr)
        {
            var field = expr.Body.ToString().Split('.').Last();
            if (SearchQuery.Where(m => m.Field == field).Count() > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 查询字段值
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public object SearchValue<T>(Expression<Func<T, object>> expr)
        {
            var field = expr.Body.ToString().Split('.').Last();
            var data = this.SearchQuery.Where(m => m.Field == field);
            if (data.Count() > 0)
            {
                return data.First().Value;
            }
            return "";
        }
        /// <summary>
        /// 设置当前分页对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="dataTotal"></param>
        /// <returns></returns>
        public PageModel<T> Record<T>(IEnumerable<T> data, long dataTotal = -1) where T : class
        {
            var model = new PageModel<T>(data, dataTotal);
            model.PageIndex = this.PageIndex;
            model.PageSize = this.PageSize;
            model.Order = this.Order;
            model.SearchQuery = this.SearchQuery;
            return model;
        }

    }
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel<T> : PageModel, IHtmlString where T : class//,IHtmlString
    {
        public PageModel(IEnumerable<T> data, Int64 dataTotal = -1)
            : base()
        {
            #region 设置数据总数
            if (data != null)
                DataSource = data;
            else
                DataSource = new List<T>();
            if (dataTotal == -1)
            {
                if (data != null)
                {
                    DataTotal = data.Count();
                }
                else
                {
                    DataTotal = 0;
                }
            }
            else
            {
                DataTotal = dataTotal;
            }
            #endregion
            this.TableClassName = "baseui-table";
            Colums = new List<PageColum<T>>();
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public virtual IEnumerable<T> DataSource { get; set; }
        /// <summary>
        /// 数据列
        /// </summary>
        private IList<PageColum<T>> Colums { get; set; }
        /// <summary>
        /// 数据的主键字段名
        /// </summary>
        private string SourcePrimaryKey { get; set; }
        /// <summary>
        /// 是否显示选择框
        /// </summary>
        private bool ShowCheckBox { get; set; }//显示选择框
        /// <summary>
        /// 列样式类名
        /// </summary>
        private string TableClassName { get; set; }
        /// <summary>
        /// 提交的表单ID
        /// </summary>
        private string SearchFormId { get; set; }
        /// <summary>
        /// 要查询的form表单ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageModel<T> SetSearchFormId(string id)
        {
            this.SearchFormId = id;
            return this;
        }
        /// <summary>
        /// 是否显示选择框
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public PageModel<T> SetShowCheckBox(bool show = false)
        {
            this.ShowCheckBox = show;
            return this;
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public string SearchName(Expression<Func<T, object>> expr)
        {
            return expr.Body.ToString().Split('.').Last();
        }
        /// <summary>
        /// 查询字段值
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public object SearchValue(Expression<Func<T, object>> expr)
        {
            var data = this.SearchQuery.Where(m => m.Field == SearchName(expr));
            if (data.Count() > 0)
            {
                return data.First().Value;
            }
            return "";
        }
        /// <summary>
        /// 设置数据源的每一行的主键字段
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public PageModel<T> SetSourceKey(Expression<Func<T, object>> expr)
        {
            this.SourcePrimaryKey = expr.Body.ToString().Split('.').Last();
            return this;
        }
        /// <summary>
        /// 设置表的样式类
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public PageModel<T> SetClass(string className)
        {
            this.TableClassName = className;

            return this;
        }
        /// <summary>
        /// 设置显示列
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="header"></param>
        /// <param name="sort"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public PageModel<T> ColumFor(Expression<Func<T, object>> expr, string header = "", bool sort = false, string style = "")
        {
            var exprBody = expr.Body.ToString();
            if (exprBody.Contains("Convert"))
            {
                exprBody = exprBody.Replace("Convert", "").Replace("(", "").Replace(")", "");
            }
            var field = exprBody.Split('.').Last();//ExpressionHelper.GetExpressionText(expr);
            if (string.IsNullOrEmpty(header))
            {
                try
                {
                    var metadata = ModelMetadata.FromLambdaExpression<T, object>(expr, new ViewDataDictionary<T>());
                    string resolvedDisplayName = metadata.DisplayName ?? metadata.PropertyName ?? field.Split('.').Last();
                    header = HttpUtility.HtmlEncode(resolvedDisplayName);
                }
                catch//处理比如datetime类型的属性
                {
                    try
                    {
                        foreach (var attr in TypeDescriptor.GetProperties(typeof(T))[field].Attributes)
                        {
                            if (attr is System.ComponentModel.DataAnnotations.DisplayAttribute)
                            {
                                header = (attr as System.ComponentModel.DataAnnotations.DisplayAttribute).Name;
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
            this.Colums.Add(new PageColum<T>()
            {
                Field = field,
                Header = header,
                Style = style,
                Sort = sort
            });
            return this;
        }
        /// <summary>
        /// 列
        /// </summary>
        /// <param name="header"></param>
        /// <param name="func"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public PageModel<T> Colum(string header, Func<T, object> func, bool sort = false, string style = "")
        {
            this.Colums.Add(new PageColum<T>()
            {
                Field = "",
                Header = header,
                Style = style,
                Format = func,
                Sort = sort
            });
            return this;
        }
        public MvcHtmlString ToMvcHtmlString()
        {
            var Model = this;
            var html = new StringBuilder();
            if (Model.Order != null && Model.Order.Field != "")
            {
                html.Append(string.Format("<input name=\"[PageModel_Order]\" type=\"hidden\" value=\"{0}\">", Model.Order.Field + "|" + Model.Order.Order.ToString()));
            }
            else
            {
                html.Append(string.Format("<input name=\"[PageModel_Order]\" type=\"hidden\" value=\"{0}\">", ""));
            }
            html.Append(string.Format("<input name=\"[PageModel_PageIndex]\" type=\"hidden\" value=\"{0}\">", 1));
            html.Append(string.Format("<input name=\"[PageModel_PageSize]\" type=\"hidden\" value=\"{0}\">", Model.PageSize.ToString()));
            html.Append(string.Format("<input name=\"[PageModel_CheckSearch]\" type=\"hidden\" value=\"{0}\">", "1"));
            html.Append(string.Format("<input name=\"[PageModel_SelectIds]\" type=\"hidden\" value=\"{0}\">", ""));
            html.Append(string.Format("<input name=\"[PageModel_PageTotal]\" type=\"hidden\" value=\"{0}\">", Model.PageTotal));


            #region 表

            var columCount = 0;

            html.Append(string.Format("<table class=\"{0}\">", this.TableClassName));
            html.Append("<thead>");
            html.Append("<tr>");
            //html.Append("<th>[]</th>");//列头
            if (Colums != null)
            {
                columCount = Colums.Count;
                if (this.ShowCheckBox)
                {
                    columCount += 1;
                    html.Append("<th style=\"width: 40px;\" ><input class=\"baseui-checkbox\" onchange=\"PageModelCheck('pageModelCheck_All')\" id=\"pageModelCheck_All\" name=\"pageModelCheck_All\" type=\"checkbox\" value=\"true\">选择</th>");
                }
                foreach (var colum in Colums)
                {
                    if (colum.Sort)
                    {
                        html.Append(string.Format("<th ><a href=\"javascript:;\" onclick=\"SearchPageModelOrder('{2}','{0}');\">{1}</a></th>"
                            , colum.Field
                            , colum.Header
                            , this.SearchFormId));
                    }
                    else
                    {
                        html.Append(string.Format("<th ><a href=\"javascript:;\">{0}</a></th>", colum.Header));
                    }
                }
            }
            html.Append("</tr>");
            html.Append("</thead>");
            if (Model != null && Model.DataSource.Count() > 0)
            {
                html.Append("<tfoot>");
                html.Append("<tr>");
                html.Append(string.Format("<td colspan=\"{0}\">", columCount));
                html.Append("<div class=\"baseui-paging\">");
                if (Model.PageIndex == 1)
                {
                    html.Append(" <a href=\"javascript:;\" class=\"baseui-paging-prev\">");
                    html.Append("<i class=\"iconfont\" >&#xF039;</i> 第一页");
                    html.Append("</a>");
                }
                else
                {
                    html.Append(string.Format(" <a href=\"javascript:;\" onclick=\"SearchPageModelPage('{0}','1');\" class=\"baseui-paging-prev\">", this.SearchFormId));
                    html.Append("<i class=\"iconfont\" >&#xF039;</i> 第一页");
                    html.Append("</a>");
                }
                var pNumber = PageSize;
                var start = GetStartNumber(Model.PageIndex, Model.PageTotal, pNumber);
                for (int i = 0; i < pNumber; i++)
                {
                    if (start + i <= Model.PageTotal)
                    {
                        if (start + i == Model.PageIndex)
                        {
                            html.Append(string.Format("<a href=\"javascript:;\" class=\"baseui-paging-item baseui-paging-current\">{0}</a>", start + i));
                        }
                        else
                        {
                            html.Append(string.Format("<a href=\"javascript:;\" onclick=\"SearchPageModelPage('{1}','{0}');\" class=\"baseui-paging-item\">{0}</a>", start + i, this.SearchFormId));
                        }
                    }
                }
                if (Model.PageIndex == Model.PageTotal || Model.PageTotal == 0)
                {
                    html.Append("<a href=\"javascript:;\" class=\"baseui-paging-next\">最后一页 <i class=\"iconfont\" >&#xF03A;</i></a>");
                }
                else
                {
                    html.Append(string.Format("<a href=\"javascript:;\" onclick=\"SearchPageModelPage('{1}','{0}');\" class=\"baseui-paging-next\">最后一页 <i class=\"iconfont\">&#xF03A;</i></a>", Model.PageTotal, this.SearchFormId));
                }
                html.Append(string.Format("<span class=\"baseui-paging-info\"><span class=\"baseui-paging-bold\">{0}/{1}</span>页</span>", Model.PageIndex, Model.PageTotal));
                html.Append("<span class=\"baseui-paging-which\">");
                html.Append(string.Format("<input name=\"[psome_name]\" value=\"{0}\" type=\"text\">", Model.PageIndex));
                html.Append("</span>");
                html.Append(string.Format(" <a class=\"baseui-paging-info baseui-paging-goto\" href=\"javascript:;\" onclick=\"SearchPageModelPageJmp('{0}')\">跳转</a>", this.SearchFormId));
                html.Append(" </div>");
                html.Append(" </td>");
                html.Append("</tr>");
                html.Append("</tfoot>");
                html.Append("</table>");
            }
            else
            {
                html.Append("<tr>");
                html.Append(string.Format("<td colspan=\"{0}\">暂无数据</td>", columCount));
                html.Append("</tr>");
                html.Append("</tbody>");
                html.Append("</table>");

            }
            #endregion
            html.Append("<script>    function SearchPageModelOrder(searchFormId, value) {        var oldValue = $(\"input[name='[PageModel_Order]']\").val();        var newValue = \"\";        if (oldValue == \"\" || oldValue == value + \"|Desc\")            $(\"input[name='[PageModel_Order]']\").val(value + \"|Asc\");       else  $(\"input[name='[PageModel_Order]']\").val(value + \"|Desc\"); $(\"input[name='[PageModel_CheckSearch]']\").val(\"0\");$(\"#\" + searchFormId).submit();}function SearchPageModelPage(searchFormId, value) {$(\"input[name='[PageModel_PageIndex]']\").val(value);$(\"input[name='[PageModel_CheckSearch]']\").val(\"0\");$(\"#\" + searchFormId).submit();} function SearchPageModelPageJmp(searchFormId) {var pindex = $(\"input[name='[psome_name]']\").val();$(\"input[name='[PageModel_PageIndex]']\").val(pindex);$(\"#\" + searchFormId).submit();}");

            string js = "function PageModelCheck(v) {var id = v.split('_')[1];  var oldValue=$(\"input[name='[PageModel_SelectIds]']\").val();    if ($(\"#\" + v).is(\":checked\")) {  if (id == \"All\") {                $(\"input:checkbox\").each(function (i, ele) {                    var cid = $(ele).attr(\"id\");                    if (cid.indexOf(\"pageModelCheck\") >= 0) {                        $(ele).attr(\"checked\", true);                    }                });                $(\"input[name='[PageModel_SelectIds]']\").val(id);            }           else {                if (oldValue.indexOf(id) < 0) {                   var newValue = oldValue + \"|\" + id;                    $(\"input[name='[PageModel_SelectIds]']\").val(newValue);                }            }       }        else {            if (id == \"All\") {                $(\"input:checkbox\").each(function (i, ele) {                    var cid = $(ele).attr(\"id\");                    if (cid.indexOf(\"pageModelCheck\") >= 0) {                        $(ele).attr(\"checked\", false);                    }                });                $(\"input[name='[PageModel_SelectIds]']\").val(\"\");            }           else {                if (oldValue.indexOf(id) >= 0) {                    var newValue = oldValue.replace(\"|\"+id,\"\");                    $(\"input[name='[PageModel_SelectIds]']\").val(newValue);                }            }        }    }";

            html.Append(js);

            html.Append("  function PageModeSelectIds() {        var ids = $(\"input[name='[PageModel_SelectIds]']\").val();        return ids;}");

            html.Append(" </script>");
            return MvcHtmlString.Create(html.ToString());
        }
        private List<PageRow> CreateRows()
        {
            var rows = new List<PageRow>();
            //ModelMetadataProvider provider = ModelMetadataProviders.Current;
            //ModelMetadata containerMetadata = new ModelMetadata(provider, null, () => null, typeof(T), null);
            //var properties = containerMetadata.Properties.Select(m => m.PropertyName);
            if (this.DataSource != null && this.DataSource.Count() > 0 && this.Colums != null && this.Colums.Count > 0)
            {
                foreach (var data in this.DataSource)
                {
                    var row = new PageRow();
                    if (!string.IsNullOrEmpty(this.SourcePrimaryKey))
                    {
                        var keyValue = data.GetType().GetProperty(this.SourcePrimaryKey).GetValue(data, null);
                        row.SourcePrimaryKey = keyValue == null ? Guid.NewGuid().ToString() : keyValue.ToString();
                    }
                    else
                    {
                        row.SourcePrimaryKey = Guid.NewGuid().ToString();
                    }
                    foreach (var colum in this.Colums)
                    {
                        if (!string.IsNullOrEmpty(colum.Field))
                        {
                            var colValue = data.GetType().GetProperty(colum.Field).GetValue(data, null);
                            row.RowData.Add(new RowItem() { ColumId = colum.Id, Vaule = colValue, CheckFieldColum = true });

                        }
                        else
                        {
                            var html = Format(colum.Format, data);
                            row.RowData.Add(new RowItem() { ColumId = colum.Id, Vaule = HttpUtility.HtmlEncode(html), CheckFieldColum = false });

                        }
                    }
                    rows.Add(row);
                }
            }
            return rows;
        }
        /// <summary>
        /// 执行委托
        /// </summary>
        /// <param name="format">lambda表达示</param>
        /// <param name="arg">委托参数</param>
        /// <returns></returns>
        private HelperResult Format(Func<T, object> format, dynamic arg)
        {
            var result = format.Invoke(arg);
            return new HelperResult(tw =>
            {
                var helper = result as HelperResult;
                if (helper != null)
                {
                    helper.WriteTo(tw);
                    return;
                }
                IHtmlString htmlString = result as IHtmlString;
                if (htmlString != null)
                {
                    tw.Write(htmlString);
                    return;
                }
                if (result != null)
                {
                    tw.Write(HttpUtility.HtmlEncode(result));
                }
            });
        }
        /// <summary>
        /// 获得一个数左右多少个的起始数
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="currentNumber">当前数</param>
        /// <param name="maxNumber">最大数</param>
        /// <param name="showCount">一共多少个</param>
        /// <returns></returns>
        private Int64 GetStartNumber(Int64 currentNumber, Int64 maxNumber, int showCount)
        {
            var arr = new List<Int64>();
            var pAvg = showCount / 2;//左右显示个数
            for (int i = 1; i <= pAvg; i++)
            {
                if (currentNumber - i > 0)
                {
                    arr.Add(currentNumber - i);
                }
                if (currentNumber + i <= maxNumber)
                {
                    arr.Add(currentNumber + i);
                }
            }
            arr.Add(currentNumber);
            if (arr.Count < 10)
            {
                var le = showCount - arr.Count;
                var min = arr.Min();
                var max = arr.Max();
                for (int i = 1; i <= le; i++)
                {
                    if (min == 1 && max + 1 <= maxNumber)
                    {
                        arr.Add(max + 1);
                    }
                    if (max == maxNumber && min - 1 >= 1)
                    {
                        arr.Add(min - 1);
                    }
                }
            }
            return arr.Min();
        }

        public string ToHtmlString()
        {
            return this.ToMvcHtmlString().ToHtmlString();
        }
    }
}
