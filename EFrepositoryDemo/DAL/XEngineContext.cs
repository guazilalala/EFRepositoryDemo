using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using repositoryDemo.Models;

namespace repositoryDemo.DAL
{
	public class XEngineContext : DbContext
	{
		public XEngineContext() : base("XEngineContext")
		{
		}
		public DbSet<SysUser> SysUsers { get; set; }
		public DbSet<SysRole> SysRoles { get; set; }
		public DbSet<SysUserRole> SysUserRoles { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}