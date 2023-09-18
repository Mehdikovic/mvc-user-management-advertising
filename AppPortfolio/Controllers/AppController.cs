using AppPortfolio.Models.DataModelsManager;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {
    public class AppController : Controller {
        // GET: App
        [Route("App/List/{page:int?}")]
        public ActionResult List(int page = 1) {
            var list = AppModelManager.GetList(Models.WorkType.Other).ToPagedList(page, 5);
            return View(model: list);
        }

        public async Task<ActionResult> Details(int id) {
            if (id <= 0) return RedirectToAction("List");
            var app = await AppModelManager.FindStatic(id);
            if (app == null) return RedirectToAction("List");
            return View(model: app);
        }
    }
}