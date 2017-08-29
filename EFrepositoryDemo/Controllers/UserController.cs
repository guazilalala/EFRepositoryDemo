using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using repositoryDemo.DAL;
using repositoryDemo.Models;
using repositoryDemo.Repositories;

namespace repositoryDemo.Controllers
{
    public class UserController : Controller
    {
        private XEngineContext db = new XEngineContext();
		
		private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: User
        public ActionResult Index()
        {
			//不加任何条件
			//var users = unitOfWork.SysUserRepository.Get();

			////加入过滤条件
			//var users = unitOfWork.SysUserRepository.Get(filter: u => u.Name == "ZS");

			//加入排序
			var users = unitOfWork.SysUserRepository.Get(orderBy:q=> q.OrderBy(u => u.Name));

			unitOfWork.Dispose();
			return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SysUser sysUser = unitOfWork.SysUserRepository.GetByID(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Password,CName,Description,ModifiedDate")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
				sysUser.ModifiedDate = DateTime.Now;
				unitOfWork.SysUserRepository.Add(sysUser);
				unitOfWork.Save();
				unitOfWork.Dispose();
                return RedirectToAction("Index");
            }

            return View(sysUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: User/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Password,CName,Description,ModifiedDate")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
				sysUser.ModifiedDate = DateTime.Now;
				unitOfWork.SysUserRepository.Update(sysUser);
				unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(sysUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			unitOfWork.SysUserRepository.Delete(id);
			unitOfWork.Save();
			unitOfWork.Dispose();
			return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		protected void Seed(XEngineContext context)
		{
			var sysUsers = new List<SysUser>
			{
				new SysUser {ID=1,Name="ZS",CName="张三",Email="za@xengine.com",
				Password="1",ModifiedDate=DateTime.Now},
				new SysUser {ID=1,Name="LS",CName="李四",Email="ls@xengine.com",
				Password="1",ModifiedDate=DateTime.Now},
				new SysUser {ID=1,Name="WW",CName="王五",Email="ww@xengine.com",
				Password="1",ModifiedDate=DateTime.Now},
			};
			sysUsers.ForEach(s => context.SysUsers.Add(s));
			context.SaveChanges();

			var sysRoles = new List<SysRole>
			{
				new SysRole {ID=1,Name="Administrators",CName="管理员",
				Description="Administrators have full authorization to perform system administration.",
				ModifiedDate = DateTime.Now },
				new SysRole {ID=2,Name="General User",CName="一般用户",
				Description="General Users can access the shared data they are authorized for.",
				ModifiedDate = DateTime.Now },
			};

			sysRoles.ForEach(s => context.SysRoles.Add(s));
			context.SaveChanges();

			var sysUserRoles = new List<SysUserRole>
			{
				new SysUserRole {SysUserID =1,SysRoleID=1,ModifiedDate=DateTime.Now },
				new SysUserRole {SysUserID =1,SysRoleID=2,ModifiedDate=DateTime.Now },
				new SysUserRole {SysUserID =2,SysRoleID=1,ModifiedDate=DateTime.Now },
				new SysUserRole {SysUserID =3,SysRoleID=2,ModifiedDate=DateTime.Now },
			};

			sysUserRoles.ForEach(s => context.SysUserRoles.Add(s));
			context.SaveChanges();
		}

	}
}
