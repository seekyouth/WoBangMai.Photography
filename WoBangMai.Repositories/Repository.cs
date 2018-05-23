using Dapper;
using Dapper.Rainbow;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WoBangMai.Models;
using WoBangMai.Interface;
using WoBangMai.Repositories;
using System.Collections;

namespace WoBangMai.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {

        public string ModuleName { get; set; }

        public Repository()
        {
            ModuleName = "CMS";
        }


        /// <summary>
        /// 操作数据库连接
        /// </summary>
        private DbConnection ReadConnection
        {
            get
            {
                var connectionStr = DbSnapConfigs.GetReadConnectionString(ModuleName);
                //var conn = new MySqlConnection(connectionStr);
                var conn = new SqlConnection(connectionStr);
                conn.Open();
                return conn;
            }
        }

        private DbConnection WriteConnection
        {
            get
            {
                var connectionStr = DbSnapConfigs.GetWriteConnectionString(ModuleName);
                var conn = new SqlConnection(connectionStr);
                conn.Open();
                return conn;
            }
        }





        /// <summary>
        /// 读取数据库上下文
        /// </summary>
        /// <returns></returns>
        protected DbContext GetReadDbContext()
        {
            return DbContext.Init(ReadConnection, DatabaseType.SqlServer);
        }

        /// <summary>
        /// 写数据库上下文
        /// </summary>
        /// <returns></returns>
        protected DbContext GetWriteDbContext()
        {
            return DbContext.Init(WriteConnection, DatabaseType.SqlServer);
        }
        /// <summary>
        /// 表名
        /// </summary>
        protected static string TableName
        {

            get
            {
                var fullName = typeof(T).FullName;
                var index = fullName.LastIndexOf(".");
                if (index >= 0)
                    return fullName.Substring(index + 1, fullName.Length - (index + 1));
                return fullName;
            }
        }

        /// <summary>
        /// Builds the orderby string.
        /// </summary>
        /// <param name="orderbys">The orderbys.如:(ID asc,Category desc)</param>
        /// <param name="alias">表别名</param>
        protected string BuildOrderbyString(Dictionary<System.Linq.Expressions.Expression<Func<T, object>>, OrderByType> orderbys, string alias = "")
        {
            List<string> orderbyList = new List<string>();
            var conditionBuilder = new ConditionBuilder();

            foreach (var item in orderbys)
            {
                var alis = string.IsNullOrEmpty(alias) ? string.Empty : alias + ".";
                orderbyList.Add(alis + conditionBuilder.GetField(item.Key) + " " + (item.Value == OrderByType.Asc ? "asc" : "desc"));
            }
            string orderby = string.Join(",", orderbyList);
            if (string.IsNullOrEmpty(orderby))
                return string.Empty;
            else
                return string.Concat(" order by ", orderby);
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(dynamic entity)
        {
            using (var db = GetWriteDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.Insert(entity);
            }
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            using (var db = GetWriteDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.Delete(expression);
            }
        }

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <param name="expression">更新条件表达式</param>
        /// <param name="data">更新内容匿名对象</param>
        /// <returns></returns>
        public bool Update(System.Linq.Expressions.Expression<Func<T, bool>> expression, dynamic data)
        {
            using (var db = GetWriteDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.Update(expression, data) > 0;
            }
        }


        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns></returns>
        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.Count(expression);
            }
        }

        /// <summary>
        /// 获取所有记录的列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return GetList();
        }

        /// <summary>
        /// 获取记录列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> expression = null)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.GetList(expression);
            }
        }


      


        /// <summary>
        /// 获取一个实体（所有字段）
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns></returns>
        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.Get(expression);
            }
        }


        /// <summary>
        /// Determines whether the specified where expression is exist.
        /// </summary>
        /// <param name="whereExpression">The where expression.</param>
        /// <returns><c>true</c> if the specified where expression is exist; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        public bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.EntityIsExist(whereExpression);
            }
        }




        /// <summary>
        /// Gets the entity only with key.
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public T GetEntityOnlyWithKey(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.GetEntityOnlyWithKey(whereExpression);
            }
        }

        /// <summary>
        /// Gets the entity with custom field.
        /// </summary>
        /// <param name="whereExpression">The where expression.</param>
        /// <param name="fieldExpressions">The field expressions.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public T GetEntityWithCustomField(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression, IEnumerable<System.Linq.Expressions.Expression<Func<T, object>>> fieldExpressions)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.GetEntityWithCustomField(whereExpression, fieldExpressions);
            }
        }



        /// <summary>
        /// 获取一个仅包含指定字段的实体列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="fieldExpressions">字段表达式</param>
        /// <returns></returns>
        public List<T> GetEntityListWithCustomField(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression, IEnumerable<System.Linq.Expressions.Expression<Func<T, object>>> fieldExpressions)
        {
            using (var db = GetReadDbContext())
            {
                var tb = new Database<DbContext>.Table<T>(db, TableName);
                return tb.GetEntityListWithCustomField(whereExpression, fieldExpressions);

            }
        }


        public T GetEntityWithCustomField(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }


      



    }

    public enum OrderByType
    {
        /// <summary>
        /// 倒序
        /// </summary>
        Desc,

        /// <summary>
        /// 升序
        /// </summary>
        Asc
    }
}
