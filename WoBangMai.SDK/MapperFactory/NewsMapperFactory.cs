
using AutoMapper;
/**
* 命名空间: WoBangMai.SDK.MapperFactory
*
* 功 能： N/A
* 类 名： NewsMapperFactory
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/4/24 23:09:24  张张  
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
using WoBangMai.CMS.Entity;
using WoBangMai.Models;

namespace WoBangMai.SDK.MapperFactory
{
  public class NewsMapperFactory
    {
        public News Get(dt_article_news model)
        {
            News newsModel=Mapper.Map<dt_article_news,News>(model);
            return newsModel;
        }
    }
}
