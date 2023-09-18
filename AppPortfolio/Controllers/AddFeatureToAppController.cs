using AppPortfolio.Models;
using AppPortfolio.Models.DataModels.ViewModels;
using AppPortfolio.Models.DataModelsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {

    [Authorize(Roles = "FullAdministrator")]
    public class AddFeatureToAppController : Controller {
        // GET: AddFeatureToApp
        public async Task<ActionResult> AddFeatureToApp(int id = 0) {
            if (id <= 0) return Redirect("/AppManager/Index");
            var model = new JAF_ViewModel() {
                AppFound = await new AppModelManager(this).Find(id),
                FeaturesAppHas = JAF_ModelManager.GetAppFeaturebyId(id),
                FeaturesAppDoesntHas = JAF_ModelManager.GetFeaturesAppDoesntHave(id)
            };
            if (model.AppFound == null) return Redirect("/AppManager/Index");
            return View(model: model);
        }

        public async Task<ActionResult> Add(int appId = 0, int featureId = 0) {
            if (appId <= 0 || featureId <= 0) return Redirect("/AppManager/Index");
            bool JAF_manager_RESULT = await new JAF_ModelManager().AddFeatureToApp(
                await AppModelManager.FindStatic(appId),
                await FeatureModelManager.FindStatic(featureId));
            return RedirectToAction("AddFeatureToApp", new { id = appId });
        }

        public async Task<ActionResult> Delete(int appId = 0, int featureId = 0) {
            if (appId <= 0 || featureId <= 0) return Redirect("/AppManager/Index");
            bool JAF_manager_RESULT = await new JAF_ModelManager().DeleteFeatureFromApp(
                await AppModelManager.FindStatic(appId),
                await FeatureModelManager.FindStatic(featureId));
            return RedirectToAction("AddFeatureToApp", new { id = appId });
        }
    }
}