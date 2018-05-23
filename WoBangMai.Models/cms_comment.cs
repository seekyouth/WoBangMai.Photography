/**
* 命名空间: WoBangMai.Models
*
* 功 能： N/A
* 类 名： cms_message
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/8/30 22:11:51  张张  
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
    public  class cms_comment:Entity
    {
        public int ment_Id
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime PostDate
        {
            get;
            set;
        }

        public int CommentId
        {
            get;
            set;
        }

        public int ArticleId
        {
            get;
            set;
        }

        public bool IsDelete { get; set; }

        public string UserName { get; set; }


        public string Email { get; set; }


        // 实现 Predicate<T> 委托，搜索Id 等于当前评论的CommentId的评论
        public bool MatchRule(cms_comment cmt)
        {
            return (this.CommentId == cmt.ment_Id);
        }

        public static CommentComparer GetComparer(bool isAscending)
        {
            return new CommentComparer(isAscending);
        }

        public static CommentComparer GetComparer()
        {
            return GetComparer(true);
        }

        public class CommentComparer : IComparer<cms_comment>
        {
            private bool isAscending;

            public CommentComparer(bool isAscending)
            {
                this.isAscending = isAscending;
            }

            public int Compare(cms_comment x, cms_comment y)
            {
                if (isAscending)
                    return x.ment_Id.CompareTo(y.ment_Id);
                else
                    return y.ment_Id.CompareTo(x.ment_Id);
            }
        }
    }
}
