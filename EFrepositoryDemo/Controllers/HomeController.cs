using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using repositoryDemo.DAL;

namespace repositoryDemo.Controllers
{
	public class HomeController : Controller
	{
		private XEngineContext db = new XEngineContext();
		public ActionResult Index()
		{
			var user = db.SysRoles.Count();
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}