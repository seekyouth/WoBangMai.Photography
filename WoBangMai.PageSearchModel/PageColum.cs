using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// 列
    /// </summary>
    public class PageColum
    {
        public PageColum()
        {
            Id = Guid.NewGuid().ToString();
            Sort = false;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 列头名
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 排序 DESC为true
        /// </summary>
        public bool Sort { get; set; }
        /// <summary>
        /// 列样式
        /// </summary>
        public string Style { get; set; }
    }
    /// <summary>
    /// 列
    /// </summary>
    public class PageColum<T> : PageColum
    {
        public Func<T, object> Format { get; set; }
    }
}
