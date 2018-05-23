/**
* 命名空间: WoBangMai.Interface
*
* 功 能： N/A
* 类 名： InewRepository
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/7/24 19:42:01  张张  
*
* Copyright (c) 2015 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：我帮买　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoBangMai.Interface;
using WoBangMai.Models;

namespace WoBangMai.Repositories
{
    public partial class NewRepository : Repository<cms_news>, INewRepository
    {
      

       
        public List<cms_news> GetModelListWithPaging(string orderBy, int PagingCurrentPage, int PagingItemsPerPage, string WhereSql, string Having, string GroupBy, string select, out int total)
        {
            StringBuilder sql = new StringBuilder();
            string sqlCount = "";
            sql.AppendFormat("SELECT * from(");
            sql.AppendFormat(@"SELECT {0},ROW_NUMBER() OVER (ORDER BY cms_news.News_ID) AS rowId FROM  cms_news where 1=1 {1} ", (string.IsNullOrEmpty(select) == true ? "*" : select), (string.IsNullOrEmpty(WhereSql) == true ? "" : " and " + WhereSql));
            sql.AppendFormat("  ) as cms_news");
            sqlCount = sql.ToString();
            if (PagingItemsPerPage > 0)
            {
                sql.AppendFormat("  WHERE cms_news.rowId>{0} AND cms_news.rowId<={1}", ((PagingCurrentPage - 1) * PagingItemsPerPage), PagingCurrentPage * PagingItemsPerPage);
            }
            using (var db = GetReadDbContext())
            {
                total = db.Query<cms_news>(sqlCount.ToString(), new { }).Count();
                return db.Query<cms_news>(sql.ToString(), new { }).ToList();
            }
        }

    }
}
