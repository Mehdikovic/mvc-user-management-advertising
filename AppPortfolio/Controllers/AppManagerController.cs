using AppPortfolio.Models;
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
    public class AppManagerController : Controller {
        // GET: AppManager
        [Route("AppManager/Index/{page:int?}")]
        public ActionResult Index(ushort page = 1) {
            //try
            {
                var app_list = AppModelManager.GetList(WorkType.Other).ToPagedList(page, 5);
                return View(model: app_list);
            }
            /*
            catch
            {
                return View(new List<App>().ToPagedList<App>(page, 5));
            }
            */
        }

        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(App application) {
            // try
            {
                if (!ModelState.IsValid) {
                    ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                    return View(model: application);
                }
                var app_manager = new AppModelManager(this);

                if (await app_manager.Add(application, WorkType.Other)) {
                    ModelState.Clear();
                    ViewBag.Success = "با موفقیت اضافه شد";
                    return View();
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

        [HttpGet]
        public async Task<ActionResult> Edit(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var app_manager = new AppModelManager(this);
                var app = await app_manager.Find(id);
                if (app == null)
                    return RedirectToAction("Index");
                return View(model: app);
            }
            /*
            catch
            {
                ViewBag.Error = "با خطا مواجه شد";
                return View(model: new App());
            }
            */

        }

        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditApp(App newApp) {
            if (newApp.ID <= 0)
                return RedirectToAction("Index");
            //try
            {
                var app_manager = new AppModelManager(this);
                if (await app_manager.Update(newApp.ID, newApp)) {
                    ViewBag.Success = "با موفقیت بروز رسانی شد";
                    return View();
                }
                ViewBag.Error = "با خطا مواجه شد";
                return View(model: newApp);
            }
            /*
            catch (Exception)
            {
                ViewBag.Error = "با خطا مواجه شد";
                return View(model: newApp);
            }
            */

        }

        public async Task<ActionResult> Delete(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var app_manager = new AppModelManager(this);
                if (await app_manager.Delete(id))
                    return View("ManagerSuccess", model: "/AppManager/Index");
                return View("ManagerError", model: "/AppManager/Index");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/AppManager/Index");
            }
            */
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(AppSearchViewModel appvm) {
            if (appvm.App.Name == null) return RedirectToAction("Search");
            var model = new AppSearchViewModel() {
                App = appvm.App,
                Apps = AppModelManager.Search(appvm.App, WorkType.Other)
            };
            return View(model: model);
        }
    }
}