using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// 对SearchModel做为Action参数的绑定
    /// </summary>
    public class SearchModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (PageModel)(bindingContext.Model ?? new PageModel());
            var dict = controllerContext.HttpContext.Request.Params;
            var keys = dict.AllKeys.Where(c => c.StartsWith("["));//我们认为只有[开头的为需要处理的
            if (keys.Count() != 0)
            {
                foreach (var key in keys)
                {
                    if (!key.StartsWith("[")) continue;
                    var val = dict[key];
                    //处理无值的情况
                    if (string.IsNullOrEmpty(val)) continue;
                    if (key == "[PageModel_Order]")//排序
                    {
                        var arr = val.Split('|');
                        if (arr.Length == 2)
                        {
                            model.Order = new QueryOrder() { Field = arr[0], Order = (OrderType)Enum.Parse(typeof(OrderType), arr[1]) };
                        }
                        continue;
                    }
                    if (key == "[PageModel_PageIndex]")//页码
                    {
                        try
                        {
                            var pindex = int.Parse(val);
                            if (pindex > 1)
                                model.PageIndex = pindex;
                            else
                                model.PageIndex = 1;
                            if (keys.Contains("[PageModel_PageTotal]"))
                            {
                                var pt = int.Parse(dict["[PageModel_PageTotal]"]);
                                if (pindex > pt)
                                {
                                    model.PageIndex = 1;
                                }
                            }
                        }
                        catch
                        {
                            model.PageIndex = 1;
                        }
                        continue;
                    }
                    if (key == "[PageModel_PageTotal]")
                    {
                        continue;
                    }
                    if (key == "[PageModel_PageSize]")//每页显示数量
                    {
                        try
                        {
                            var pSize = int.Parse(val);
                            if (pSize > 0)
                                model.PageSize = pSize;
                            else
                                model.PageSize = 20;
                        }
                        catch
                        {
                            model.PageSize = 20;
                        }
                        continue;
                    }
                    if (key == "[PageModel_CheckSearch]")
                    {
                        if (val == "1")
                        {
                            //model.che = 1;
                        }
                        continue;
                    }
                    if (key == "[PageModel_SelectIds]")
                    {
                        if (val == "All")
                        {
                            model.SelectAll = true;
                        }
                        else
                        {
                            model.SelectAll = false;
                            model.SelectPrimaryKeys = val.Split('|').Where(m => m != "").ToList();
                        }
                        continue;
                    }
                    if (key == "[psome_name]")
                    {
                        continue;
                    }
                    AddSearchItem(model, key, val);
                }
            }
            return model;
        }

        /// <summary>
        /// 将一组key=value添加入QueryModel.Items
        /// </summary>
        /// <param name="model">QueryModel</param>
        /// <param name="key">当前项的HtmlName</param>
        /// <param name="val">当前项的值</param>
        public static void AddSearchItem(PageModel model, string key, string val)
        {
            string field = "", prefix = "", orGroup = "", method = "";
            var keywords = key.Split(']', ')', '}');
            //将Html中的name分割为我们想要的几个部分
            foreach (var keyword in keywords)
            {
                if (Char.IsLetterOrDigit(keyword[0])) field = keyword;
                var last = keyword.Substring(1);
                if (keyword[0] == '(') prefix = last;
                if (keyword[0] == '[') method = last;
                if (keyword[0] == '{') orGroup = last;
            }
            if (string.IsNullOrEmpty(method)) return;
            if (!string.IsNullOrEmpty(field))
            {
                var item = new ConditionItem
                {
                    Field = field,
                    Value = val.Trim(),
                    Prefix = prefix,
                    OrGroup = orGroup,
                    Method = (QueryMethod)Enum.Parse(typeof(QueryMethod), method)
                };
                model.SearchQuery.Add(item);
            }
        }
    }
}
