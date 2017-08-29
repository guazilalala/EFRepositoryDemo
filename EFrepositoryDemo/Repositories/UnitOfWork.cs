using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using repositoryDemo.DAL;
using repositoryDemo.Models;

namespace repositoryDemo.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private XEngineContext _dbContext = new XEngineContext();
		private GenericRepository<SysUser> sysUserRepository;
		private GenericRepository<SysRole> sysRoleRepository;
		private GenericRepository<SysUserRole> sysUserRoleRepository;

		public GenericRepository<SysUser> SysUserRepository
		{
			get
			{
				if (this.sysUserRepository == null)
				{
					this.sysUserRepository = new GenericRepository<SysUser>(_dbContext);
				}
				return sysUserRepository;
			}
		}

		public GenericRepository<SysRole> SysRoleRepository
		{
			get
			{
				if (this.sysRoleRepository == null)
				{
					this.sysRoleRepository = new GenericRepository<SysRole>(_dbContext);
				}
				return sysRoleRepository;
			}
		}

		public GenericRepository<SysUserRole> SysUserRoleRepository
		{
			get
			{
				if (this.sysUserRoleRepository == null)
				{
					this.sysUserRoleRepository = new GenericRepository<SysUserRole>(_dbContext);
				}
				return sysUserRoleRepository;
			}
		}

		private bool disposed = false;
		public int Save()
		{
			return _dbContext.SaveChanges();
		}

		public Task<int> SaveAsync()
		{
			return _dbContext.SaveChangesAsync();
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}

			this.disposed = true;
		}


	}
}