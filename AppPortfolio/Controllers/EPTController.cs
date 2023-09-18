using AppPortfolio.Models.DataModelsManager;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {
    public class EPTController : Controller {
        // GET: EPT
        public ActionResult App() {
            var model = AppModelManager.GetList(Models.WorkType.EPT).FirstOrDefault();
            if (model == null) return Redirect("/");
            return View(model: model);
        }

        [Route("EPT/Questions/{page:int?}")]
        public ActionResult Questions(int page = 1) {
            var question_list = QuestionModelManager.GetList(Models.WorkType.EPT).ToPagedList(page, 10);
            ViewBag.Page = page;
            return View(model: question_list);
        }
    }
}