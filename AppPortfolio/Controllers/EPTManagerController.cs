using AppPortfolio.Models;
using AppPortfolio.Models.DataModels.ViewModels;
using AppPortfolio.Models.DataModels.ViewModels.SearchViewModels;
using AppPortfolio.Models.DataModelsManager;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {

    [Authorize(Roles = "FullAdministrator")]
    public class EPTManagerController : Controller {
        //EPT APP MANAGER

        // GET: EPTManager
        public ActionResult Index(string error) {
            ViewBag.Error = error;
            var model = AppModelManager.GetList(Models.WorkType.EPT).FirstOrDefault();
            if (model == null) return RedirectToAction("Create");
            return View(model: model);
        }

        // GET: EPTManager/Create
        [HttpGet]
        public ActionResult Create() {
            if (AppModelManager.GetList(Models.WorkType.EPT).Count() >= 1)
                return RedirectToAction("Index", new { error = "risky_operation" });
            return View();
        }

        // POST: EPTManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(App application) {
            // try
            {
                if (AppModelManager.GetList(Models.WorkType.EPT).Count() >= 1)
                    return RedirectToAction("Index", new { error = "risky_operation" });
                if (!ModelState.IsValid) {
                    ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                    return View(model: application);
                }
                var app_manager = new AppModelManager(this);

                if (await app_manager.Add(application, WorkType.EPT)) {
                    ModelState.Clear();
                    ViewBag.Success = "با موفقیت اضافه شد";
                    return RedirectToAction("Index");
                }
                ViewBag.Error = "با خطا مواجه شد";
                return View(model: application);
            }
            /*
            catch
            {
                ViewBag.Error = "با خطا مواجه شد";
                return View(model: application);
            }
            */
        }

        // GET: EPTManager/Delete/5
        public async Task<ActionResult> Delete(int id) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var app_manager = new AppModelManager(this);
                if (await app_manager.Delete(id))
                    return View("ManagerSuccess", model: "/EPTManager/Index");
                return View("ManagerError", model: "/EPTManager/Index");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/EPTManager/Index");
            }
            */
        }

        //EPT QUESTION MANAGER

        [Route("EPTManager/QList/{page:int?}")]
        public ActionResult QList(int page = 1) {
            var question_list = QuestionModelManager.GetList(WorkType.EPT).ToPagedList(page, 15);
            return View(model: question_list);
        }

        public ActionResult QCreate() {
            return View();
        }

        // POST: QuestionManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QCreate(Question question) {
            if (!ModelState.IsValid) {
                ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                return View(model: question);
            }
            //  try {
            var question_manager = new QuestionModelManager(this);
            if (await question_manager.Add(question, WorkType.EPT)) {
                ModelState.Clear();
                ViewBag.Success = "با موفقیت اضافه شد";
                return View();
            }
            ViewBag.Error = "با خطا مواجه شد";
            return View(model: question);
            /* 
            } catch {
                          ViewBag.Error = "با خطا مواجه شد";
                return View(model: question);
                      }
                      */
        }

        public async Task<ActionResult> QUpdate(int id = 0) {
            if (id <= 0) return RedirectToAction("QList");
            var question_manager = new QuestionModelManager(this);
            QuestionUpdateViewModel qeustionvm = await question_manager.FindForViewModel(id);
            if (qeustionvm == null) return RedirectToAction("QList");
            return View(model: qeustionvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QUpdate(QuestionUpdateViewModel qvm) {
            if (qvm.ID <= 0) return RedirectToAction("QList");
            if (!ModelState.IsValid) {
                ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                return View(model: qvm);
            }
            var question_manager = new QuestionModelManager(this);
            if (await question_manager.Update(qvm)) {
                ViewBag.Success = "با موفقیت بروز رسانی شد";
                return View(model: qvm);
            }
            ViewBag.Error = "با خطا مواجه شد";
            return View(model: qvm);
        }

        // GET: QuestionManager/Delete/5
        public async Task<ActionResult> QDelete(int id = 0) {
            if (id <= 0)
                return RedirectToAction("QList");
            //try
            {
                var question_manager = new QuestionModelManager(this);
                if (await question_manager.Delete(id))
                    return View("ManagerSuccess", model: "/EPTManager/QList");
                return View("ManagerError", model: "/EPTManager/QList");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/EPTManager/QList");
            }
            */
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(QuestionSearchViewModel vm) {
            var model = new QuestionSearchViewModel() {
                Question = vm.Question,
                Questions = QuestionModelManager.Search(vm.Question, WorkType.EPT)
            };
            return View(model: model);
        }
    }
}
