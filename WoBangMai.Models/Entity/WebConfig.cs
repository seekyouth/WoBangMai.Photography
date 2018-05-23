/**
* 命名空间: WoBangMai.Models.Entity
*
* 功 能： N/A
* 类 名： WebConfig
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/3/13 23:09:27  张张  
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
  public  class WebConfig
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebName { get; set; }
        
        /// <summary>
        /// 网站域名
        /// </summary>
        public string WebUrl { get; set; }
      
        /// <summary>
        /// 公司名称
        /// </summary>
        public string WebCompany { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        public string WebAddress { get; set; }
       
        /// <summary>
        /// 联系电话
        /// </summary>
        public string WebTel { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        public string WebFax { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public string WebMail { get; set; }

        /// <summary>
        /// 网站备案号
        /// </summary>
        public string WebCrod { get; set; }


        /// <summary>
        /// 网站安装目录
        /// </summary>
        public string WebPath { get; set; }

        /// <summary>
        /// 网站管理目录
        /// </summary>
        public string WebManagePath { get; set; }
        
        
        /// <summary>
        /// 开启会员功能
        /// </summary>
        public int memberstatus { get; set; }
        
        /// <summary>
        /// 开启评论审核
        /// </summary>
        public int commentstatus { get; set; }
         
        /// <summary>
        /// 后台管理日志
        /// </summary>
        public int LogStatus { get; set; }
        
        /// <summary>
        /// 是否关闭网站
        /// </summary>
        public int WebStatus { get; set; }
        
        /// <summary>
        /// 关闭原因描述
        /// </summary>
        public string WebCloseReason { get; set; }
        
        /// <summary>
        /// 网站统计代码
        /// </summary>
        public string WebCountCode { get; set; }
         
    }
}
