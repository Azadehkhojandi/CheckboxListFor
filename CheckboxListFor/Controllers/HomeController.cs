using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckboxListFor.Models;

namespace CheckboxListFor.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			ViewBag.Message = "Please fill the following the form?";			
			return View(new MyModel());
		}

		[HttpPost]
		public ActionResult Index(MyModel model)
		{
			if (ModelState.IsValid)
			{
				string lanuguages = model.ListofSelectedMyType.Aggregate("", (current, item) => (String.IsNullOrEmpty(current)? current : current+",") + model.ListofDisplayCaptionsOfMyType[item].ToString());
                string numbers = model.ListofSelectedInt.Aggregate("", (current, item) => (String.IsNullOrEmpty(current) ? current : current + ",") + model.ListofDisplayCaptionsOfInt[item].ToString());

                ViewBag.Message = string.Format("Hi {0}, Language{1} you know: {2}. Selected number{3}: {4} ", model.Name, model.ListofSelectedMyType.Count() > 1 ? "s" : "", lanuguages, model.ListofSelectedInt.Count() > 1 ? "s" : "", numbers);
			}
			return View(model);
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
