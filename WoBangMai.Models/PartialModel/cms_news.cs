/**
* 命名空间: WoBangMai.Models.PartialModel
*
* 功 能： N/A
* 类 名： cms_news
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2017/12/5 22:34:35  张张  
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
 public partial  class cms_news
    {
        public List<cms_news_attach> AttachList { get; set; }
    }
}
