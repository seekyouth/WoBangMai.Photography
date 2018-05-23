/**
* 命名空间: WoBangMai.Repositories.CMSModule
*
* 功 能： N/A
* 类 名： CommentRepository
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/10/26 22:31:14  张张  
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
using WoBangMai.Interface;
using WoBangMai.Models;

namespace WoBangMai.Repositories.CMSModule
{
    public  class CommentRepository : Repository<cms_comment>, ICommentRepository
    {
        public List<cms_comment> GetModelListWithPaging(string orderBy, int PagingCurrentPage, int PagingItemsPerPage, string WhereSql, string Having, string GroupBy, string select, out int total)
        {
            StringBuilder sql = new StringBuilder();
            string sqlCount = "";
            sql.AppendFormat("SELECT * from(");
            sql.AppendFormat(@"SELECT {0},ROW_NUMBER() OVER (ORDER BY cms_comment.ment_Id) AS rowId FROM  cms_comment left join cms_UserInfo  on cms_comment.UserId=cms_UserInfo.user_Id   where 1=1 {1} ", (string.IsNullOrEmpty(select) == true ? "*" : select), (string.IsNullOrEmpty(WhereSql) == true ? "" : " and " + WhereSql));
            sql.AppendFormat("  ) as cms_comment");
            sqlCount = sql.ToString();
            if (PagingItemsPerPage > 0)
            {
                sql.AppendFormat("  WHERE cms_comment.rowId>{0} AND cms_comment.rowId<={1}", ((PagingCurrentPage - 1) * PagingItemsPerPage), PagingCurrentPage * PagingItemsPerPage);
            }
            using (var db = GetReadDbContext())
            {
                total = db.Query<cms_comment>(sqlCount.ToString(), new { }).Count();
                return db.Query<cms_comment>(sql.ToString(), new { }).ToList();
            }
        }
    }
}
