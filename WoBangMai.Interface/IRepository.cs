
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoBangMai.Models;

namespace WoBangMai.Interface
{
    public interface IRepository<T> where T : Entity
    {

        int Add(T entity);


        bool Delete(Expression<Func<T, bool>> expression);


        bool Update(Expression<Func<T, bool>> expression, Expression<Func<T, T>> updateExpression);


        int Count(Expression<Func<T, bool>> expression);


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();


        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression);


        /// <summary>
        /// 多表查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetMultiList<TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> orderBy);


        /// <summary>
        /// 获取的一个实体对象
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> expression);


        /// <summary>
        ///  实体是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> whereExpression);


        /// <summary>
        ///  获取一个仅包含主键字段的实体（主键字段名为ID）
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        T GetEntityOnlyWithKey(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression);


        /// <summary>
        ///  获取一个仅包含制定字段的实体 
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        T GetEntityWithCustomField(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression);


        /// <summary>
        ///  获取一个仅包含指定字段的实体列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="fieldExpressions"></param>
        /// <returns></returns>
        List<T> GetEntityListWithCustomField(System.Linq.Expressions.Expression<Func<T, bool>> whereExpression, IEnumerable<System.Linq.Expressions.Expression<Func<T, object>>> fieldExpressions);



        /// <summary>
        /// 实体分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        List<T> GetList<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex, out int total);


        List<TModel> GetList<TModel>(string sql, List<SqlParameter> parms, CommandType cmdType = CommandType.Text);

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <returns></returns>
        List<T> GetMultiList<TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex, out int total);


        /// <summary>
        /// 多表查询
        /// </summary>
        /// <returns></returns>
        List<T> GetMultiList<TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> otherexpression, Expression<Func<T, TKey>> orderBy, int pageSize, int pageIndex, out int total);
    }
}
