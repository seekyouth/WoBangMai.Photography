using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    public class QueryOrder
    {
        public QueryOrder()
        {
            Order = OrderType.ASC;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string Field { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public virtual OrderType Order { get; set; }

    }
    public enum OrderType
    {
        ASC,
        DESC
    }
}
