using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using repositoryDemo.DAL;

namespace repositoryDemo.Repositories
{
	public class GenericRepository<TEntity>
		: IGenericRepository<TEntity> where TEntity : class
	{
		internal XEngineContext _dbContext;
		internal DbSet<TEntity> dbSet;

		public GenericRepository(XEngineContext context)
		{
			this._dbContext = context;
			this.dbSet = context.Set<TEntity>();
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Delete(object id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			dbSet.Remove(entityToDelete);
		}

		public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split(new char[] {',' },StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public TEntity GetByID(object id)
		{
			return dbSet.Find(id);
		}

		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="entity"></param>
		public void Add(TEntity entity)
		{
			dbSet.Add(entity);
		}

		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="entityToUpdate"></param>
		public void Update(TEntity entityToUpdate)
		{
			dbSet.Attach(entityToUpdate);
			_dbContext.Entry(entityToUpdate).State = EntityState.Modified;
		}

	}
}