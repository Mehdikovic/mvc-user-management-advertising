using AppPortfolio.Models;
using AppPortfolio.Models.DataModels.ViewModels;
using AppPortfolio.Models.DataModels.ViewModels.SearchViewModels;
using AppPortfolio.Models.DataModelsManager;
using Microsoft.Security.Application;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {

    [Authorize(Roles = "FullAdministrator")]
    public class QuestionManagerController : Controller {
        // GET: QuestionManager
        [Route("QuestionManager/Index/{page:int?}")]
        public ActionResult Index(int page = 1) {
            var question_list = QuestionModelManager.GetList(WorkType.Other).ToPagedList(page, 15);
            return View(model: question_list);
        }

        // GET: QuestionManager/Create
        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        // POST: QuestionManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Question question, short year, short month, short day) {
            var date = ValidateDate(year, month, day);
            if (!ModelState.IsValid || !date.Item1) {
                ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                return View(model: question);
            }
            //  try {
            var question_manager = new QuestionModelManager(this);
            //Anti_XSS(ref question);
            question.CreatedDate = PersianTime.GetMiladi(year, month, day);
            if (await question_manager.Add(question, WorkType.Other)) {
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

        public async Task<ActionResult> Update(int id = 0) {
            if (id <= 0) return RedirectToAction("Index");
            var question_manager = new QuestionModelManager(this);
            QuestionUpdateViewModel qeustionvm = await question_manager.FindForViewModel(id);
            if (qeustionvm == null) return RedirectToAction("Index");
            return View(model: qeustionvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(QuestionUpdateViewModel qvm) {
            if (qvm.ID <= 0) return RedirectToAction("Index");
            if (!ModelState.IsValid) {
                ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                return View(model: qvm);
            }
            var question_manager = new QuestionModelManager(this);
            //Anti_XSS(ref qvm);
            if (await question_manager.Update(qvm)) {
                ViewBag.Success = "با موفقیت بروز رسانی شد";
                return View(model: qvm);
            }
            ViewBag.Error = "با خطا مواجه شد";
            return View(model: qvm);
        }

        // GET: QuestionManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var question_manager = new QuestionModelManager(this);
                if (await question_manager.Delete(id))
                    return View("ManagerSuccess", model: "/QuestionManager/Index");
                return View("ManagerError", model: "/QuestionManager/Index");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/QuestionManager/Index");
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
                Questions = QuestionModelManager.Search(vm.Question, WorkType.Other)
            };
            return View(model: model);
        }

        private Tuple<bool, string> ValidateDate(short year, short month, short day) {
            var pc = new System.Globalization.PersianCalendar();
            int currentYear = pc.GetYear(Utilities.IranDateTime.Now);
            if (!(currentYear - 10 <= year && year <= currentYear))
                return new Tuple<bool, string>(false, "");
            if (!(1 <= month && month <= 12))
                return new Tuple<bool, string>(false, "");
            if (!(1 <= day && day <= 31))
                return new Tuple<bool, string>(false, "");
            if (year % 4 == 3) {
                if (month <= 6) {
                    if (day <= 31)
                        return new Tuple<bool, string>(true, getdateString(year, month, day));
                    else
                        return new Tuple<bool, string>(false, "");
                } else {
                    if (day <= 30)
                        return new Tuple<bool, string>(true, getdateString(year, month, day));
                    else
                        return new Tuple<bool, string>(false, "");
                }
            } else {
                if (month <= 6) {
                    if (day <= 31)
                        return new Tuple<bool, string>(true, getdateString(year, month, day));
                    else
                        return new Tuple<bool, string>(false, "");
                } else if (7 <= month && month <= 11) {
                    if (day <= 30)
                        return new Tuple<bool, string>(true, getdateString(year, month, day));
                    else
                        return new Tuple<bool, string>(false, "");
                } else {
                    if (day <= 29)
                        return new Tuple<bool, string>(true, getdateString(year, month, day));
                    else
                        return new Tuple<bool, string>(false, "");
                }
            }
        }
        private string getdateString(short year, short month, short day) {
            var dateString = new StringBuilder();
            return dateString.Append(year.ToString("0000"))
                .Append("/")
                .Append(month.ToString("00"))
                .Append("/")
                .Append(day.ToString("00"))
                .ToString();
        }

        private void Anti_XSS(ref Question question) {
            question.Description = Sanitizer.GetSafeHtmlFragment(question.Description);
        }

        private void Anti_XSS(ref QuestionUpdateViewModel qvm) {
            qvm.Description = Sanitizer.GetSafeHtmlFragment(qvm.Description);
        }
    }
}
