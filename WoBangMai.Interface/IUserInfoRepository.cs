﻿/**
* 命名空间: WoBangMai.Interface
*
* 功 能： N/A
* 类 名： IuserInfoRepository
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/11/26 17:04:48  张张  
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
using WoBangMai.Models;

namespace WoBangMai.Interface
{
    public interface IUserInfoRepository : IRepository<cms_userInfo>
    {

    }
}
