﻿/**
* 命名空间: WoBangMai.Models
*
* 功 能： N/A
* 类 名： cms_userInfo
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/11/26 16:52:51  张张  
*
* Copyright (c) 2015 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：我帮买　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WoBangMai.Models
{
    public class view_userInfo
    {
        public int user_Id { get; set; }

        [Required]
        [Remote("CheckUserName", "Account")]
        public string UserName { get; set; }

        public string NickName { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [StringLength(11)]
        public string CellPhone { get; set; }


        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
