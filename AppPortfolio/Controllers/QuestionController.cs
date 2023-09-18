using AppPortfolio.Models.DataModelsManager;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {
    public class QuestionController : Controller {
        [Route("Question/List/{id:int}/{page:int?}")]
        // GET: Question
        public ActionResult List(int id = 0, int page = 1) {
            if (id == 0) {
                var question_list = QuestionModelManager.GetListOfPastYears(Models.WorkType.Other).ToPagedList(page, 10);
                ViewBag.Page = page;
                ViewBag.Title = "سوالات استخدامی سال های گذشته";
                return View(model: question_list);
            } else {
                var question_list = QuestionModelManager.GetListOfCurrentYear(Models.WorkType.Other).ToPagedList(page, 10);
                ViewBag.Page = page;
                ViewBag.Title = "سوالات استخدامی سال جاری";
                return View(model: question_list);
            }
        }

        public async Task<ActionResult> Details(int id) {
            if (id <= 0) return RedirectToAction("List");
            var model = await QuestionModelManager.FindStatic(id);
            if (model == null) return RedirectToAction("List");
            return View(model: model);
        }
    }
}