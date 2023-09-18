using AppPortfolio.Models;
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
    public class FeatureManagerController : Controller {
        // GET: FeatureManager
        [Route("FeatureManager/Index/{page:int?}")]
        public ActionResult Index(int page = 1) {
            var model = FeatureModelManager.GetList().ToPagedList(page, 20);
            return View(model: model);
        }

        // GET: FeatureManager/Create
        public ActionResult Create() {
            return View();
        }

        // POST: FeatureManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Feature feature) {
            if (!ModelState.IsValid) {
                ViewBag.Error = "مقادیر صحیح وارد کنید";
                return View(model: feature);
            }
            // try 
            {
                var feature_manager = new FeatureModelManager();
                if (await feature_manager.Add(feature)) {
                    ViewBag.Success = "با موفقیت ثبت شد";
                    ModelState.Clear();
                    return View();
                }
                ViewBag.Error = "با خطا همراه بود";
                return View(model: feature);
            }
            /*
            catch {
                return View();
            }
            */
        }

        // GET: FeatureManager/Edit/5
        public async Task<ActionResult> Edit(int id) {
            if (id <= 0)
                return RedirectToAction("Index");
            var news = await new FeatureModelManager().Find(id);
            if (news == null)
                return RedirectToAction("Index");
            return View(model: news);
        }

        // POST: FeatureManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Feature feature) {
            if (!ModelState.IsValid) {
                ViewBag.Error = "مقادیر صحیح وارد کنید";
                return View();
            }
            //try
            {
                var feature_manager = new FeatureModelManager();

                if (await feature_manager.Update(id, feature)) {
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

        // GET: FeatureManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0) {
            if (id <= 0)
                return RedirectToAction("Index");
            //try
            {
                var news_manager = new FeatureModelManager();
                if (await news_manager.Delete(id))
                    return View("ManagerSuccess", model: "/FeatureManager/Index");
                return View("ManagerError", model: "/FeatureManager/Index");
            }
            /*
            catch
            {
                return View("ManagerError", model: "/FeatureManager/Index");
            }
            */
        }

    }
}
