/**
* 命名空间: WoBangMai.Models
*
* 功 能： N/A
* 类 名： cms_news_attach
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2017/12/5 22:37:20  张张  
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

namespace WoBangMai.Models
{
 public partial   class cms_news_attach
    {
        public int Attach_Id { get; set; }

        public int? New_Id { get; set; }

        [StringLength(255)]
        public string File_Name { get; set; }

        [StringLength(255)]
        public string File_Path { get; set; }

        public int? File_Size { get; set; }

        [StringLength(20)]
        public string File_Ext { get; set; }

        public int? Down_Num { get; set; }

        public int? Point { get; set; }

        public DateTime? Add_Time { get; set; }
    }
}
