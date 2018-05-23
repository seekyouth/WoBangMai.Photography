/**
* 命名空间: DoMain.DataModel.CMSModel
*
* 功 能： N/A
* 类 名： cms_webconfig
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/7/4 14:01:14  张张  
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
  public  class cms_webconfig : Entity
    {
        public int Web_ID
        { get; set; }

        public string WebTitle
        { get; set; }

        public string WebName
        { get; set; }

        public string WebKeyWords
        { get; set; }

        public string  Content
        { get; set; }

        public string  Description
        { get; set; }

        public string  LicenseNumber
        { get; set; }

        public string NetworkNumber
        { get; set; }

       
    }
}
