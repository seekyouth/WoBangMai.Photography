/**
* 命名空间: WoBangMai.Models.Entity
*
* 功 能： N/A
* 类 名： News
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/2/13 10:21:06  张张  
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
    public partial class News
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public int ChannelId { get; set; }

        public int CategoryId { get; set; }

        public int? Brand_id { get; set; }


        public string CallIndex { get; set; }


        public string Title { get; set; }


        public string LinkUrl { get; set; }


        public string ImgUrl { get; set; }


        public string SeoTitle { get; set; }

        public string SeoKeywords { get; set; }

        public string SeoDescription { get; set; }

        public string Tags { get; set; }

        public string ZhaiYao { get; set; }

        public string Content { get; set; }

        public int SortId { get; set; }

        public int Click { get; set; }

        public int Status { get; set; }

        public int IsMsg { get; set; }

        public int iSTop { get; set; }

        public int IsRed { get; set; }

        public int IsHot { get; set; }

        public int IsSlide { get; set; }

        public int IsSys { get; set; }

        public string UserName { get; set; }

        public int LikeCount { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string Source { get; set; }

        public string Author { get; set; }
    }
}
