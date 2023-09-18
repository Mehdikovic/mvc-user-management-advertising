using AppPortfolio.Models;
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
    public class NewsManagerController : Controller {
        // GET: NewsManager
        [Route("NewsManager/Index/{page:int?}")]
        public ActionResult Index(ushort page = 1) {
            var list = NewsModelManager.GetList().ToPagedList(page, 5);
            return View(model: list);
        }

        // GET: NewsManager/Details/5
        public async Task<ActionResult> Details(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            var news = await new NewsModelManager(this).Find(id);
            if (news == null)
                return RedirectToAction("Index");
            return View(model: news);
        }

        // GET: NewsManager/Create
        public ActionResult Create() {
            return View();
        }

        // POST: NewsManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HiringNews news, short year, short month, short day) {
            var date = ValidateDate(year, month, day);
            if (!ModelState.IsValid || !date.Item1) {
                ViewBag.Error = "مقادیر صحیح وارد کنید";
                return View(model: news);
            }
            //try
            {
                var news_manager = new NewsModelManager(this);
                //Anti_XSS(ref news);
                if (await news_manager.Add(news, date.Item2)) {
                    ViewBag.Success = "با موفقیت ثبت شد";
                    ModelState.Clear();
                    return View();
                }
                ViewBag.Error = "با خطا همراه بود";
                return View(model: news);

            }
            /*
            catch
            {
                ViewBag.Error = "با خطا همراه بود";
                return View();
            }
            */
        }


        // GET: NewsManager/Edit/5
        public async Task<ActionResult> Edit(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            var news = await new NewsModelManager(this).Find(id);
            if (news == null)
                return RedirectToAction("Index");
            return View(model: news);
        }

        // POST: NewsManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, HiringNews news) {
            if (!ModelState.IsValid) {
                ViewBag.Error = "مقادیر صحیح وارد کنید";
                return View();
            }
            //try
            {
                var news_manager = new NewsModelManager(this);
                //Anti_XSS(ref news);
                if (await news_manager.Update(id, news)) {
                    ViewBag.Success = "با موفقیت بروز رسانی شد";
                    return View();
                }
                ViewBag.Error = "با خطا همراه بود";
                return View();
            }
            /*
            catch
            {
                ViewBag.Error = "با خطا همراه بود";
                return View();
            }
            */
        }

        // GET: NewsManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var news_manager = new NewsModelManager(this);
                if (await news_manager.Delete(id))
                    return View("ManagerSuccess", model: "/NewsManager/Index");
                return View("ManagerError", model: "/NewsManager/Index");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/NewsManager/Index");
            }
            */
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(NewsSearchViewModel vm) {
            var model = new NewsSearchViewModel() {
                News = vm.News,
                NewsList = NewsModelManager.Search(vm.News)
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

        private void Anti_XSS(ref HiringNews news) {
            news.Text = Sanitizer.GetSafeHtmlFragment(news.Text);
        }
    }
}
