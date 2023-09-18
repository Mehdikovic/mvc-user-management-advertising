using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppPortfolio.Models.DataModelsManager {
    public class JAF_ModelManager {

        ApplicationDbContext context;
        public JAF_ModelManager() {
            context = new ApplicationDbContext();
        }

        public static IEnumerable<Junc_App_Feature> GetAppFeaturebyId(int appId) {
            if (appId <= 0) return new List<Junc_App_Feature>();
            var context = new ApplicationDbContext();
            return context.Junc_App_Feature.Where(m => m.AppID == appId).ToList();
        }

        public static IEnumerable<Feature> GetFeaturesAppDoesntHave(int appId) {
            if (appId <= 0) return new List<Feature>();
            var context = new ApplicationDbContext();
            var featureList = context.Feature.ToList();
            var featureAppHas = GetAppFeaturebyId(appId).Select(S =>
                new Feature { ID = S.FeatureID }).ToList();

            List<Feature> result = new List<Feature>();

            foreach (var first in featureList) {
                bool isValid = true;
                foreach (var second in featureAppHas) {
                    if (first.ID == second.ID) {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                    result.Add(first);
            }
            return result;
        }

        public async Task<bool> AddFeatureToApp(App app, Feature feature) {
            if (app == null || feature == null) return false;
            context.Junc_App_Feature.Add(new Junc_App_Feature() { AppID = app.ID, FeatureID = feature.ID });
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> DeleteFeatureFromApp(App app, Feature feature) {
            if (app == null || feature == null) return false;
            var obj = context.Junc_App_Feature.Where(m => m.AppID == app.ID && m.FeatureID == feature.ID).FirstOrDefault();
            if (obj == null) return false;
            context.Junc_App_Feature.Remove(obj);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }
    }
}