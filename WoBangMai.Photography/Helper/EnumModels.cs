using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WoBangMai.Photography
{
    public static class EnumModels
    {
        public enum CategoryEnum
        {
            /// <summary>
            /// 首页
            /// </summary>
            Home = 0,
            /// <summary>
            /// 资讯
            /// </summary>
            News = 1,
            /// <summary>
            /// 作品
            /// </summary>
            Gallery = 4,
            /// <summary>
            /// 关于
            /// </summary>
            About = 1006,
            /// <summary>
            /// 联系
            /// </summary>
            Contact = 1007,
            /// <summary>
            /// 自由页面
            /// </summary>
            Other = 1008,
        }
    }
}