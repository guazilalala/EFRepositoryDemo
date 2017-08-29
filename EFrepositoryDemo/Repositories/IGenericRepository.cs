using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace repositoryDemo.Repositories
{
	interface IGenericRepository<TEntity> where TEntity:class
	{
		/// <summary>
		/// 获取数据
		/// </summary>
		/// <param name="filter">过滤条件</param>
		/// <param name="orderBy">排序</param>
		/// <param name="includeProperties">贪婪加载</param>
		/// <returns></returns>
		IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>> filter = null,
			Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy=null,
			string includeProperties = "");

		/// <summary>
		/// 通过ID获取对象
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TEntity GetByID(object id);

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="entity"></param>
		void Add(TEntity entity);

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		void Delete(object id);

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="entity"></param>
		void Update(TEntity entity);
	}
}
