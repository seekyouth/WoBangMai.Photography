/**
* 命名空间: WoBangMai.Models.Entity
*
* 功 能： N/A
* 类 名： Category
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/2/13 10:41:07  张张  
*
* Copyright (c) 2015 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：我帮买　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoBangMai.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int ChannelId { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public string CallIndex { get; set; }

        public int? ParentId { get; set; }

        public int? SortId { get; set; }


        public string LinkUrl { get; set; }


        public string ImgUrl { get; set; }


        public string Content { get; set; }


        public string SeoTitle { get; set; }


        public string SeoKeywords { get; set; }


        public string SeoDescription { get; set; }
    }
}
