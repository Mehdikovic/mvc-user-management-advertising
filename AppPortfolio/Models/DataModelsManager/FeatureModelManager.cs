using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppPortfolio.Models.DataModelsManager {
    public class FeatureModelManager {
        private ApplicationDbContext context;
        public FeatureModelManager() {
            context = new ApplicationDbContext();
        }


        public static IEnumerable<Feature> GetList() {
            var context = new ApplicationDbContext();
            return context.Feature.ToList();
        }

        public async Task<bool> Add(Feature feature) {
            validate(feature);
            context.Feature.Add(feature);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> Update(int id, Feature feature) {
            var oldFeature = await context.Feature.FindAsync(id);
            if (oldFeature == null) return false;
            validate(feature);
            oldFeature.Title = feature.Title.Trim();
            oldFeature.GlyphIconName = feature.GlyphIconName.Trim();
            oldFeature.Text = feature.Text.Trim();
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> Delete(int id) {
            var oldFeature = await context.Feature.FindAsync(id);
            if (oldFeature == null) return false;
            context.Feature.Remove(oldFeature);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<Feature> Find(int id) {
            var feature = await context.Feature.FindAsync(id);
            if (feature == null) return null;
            return feature;
        }

        public static async Task<Feature> FindStatic(int id) {
            var context = new ApplicationDbContext();
            var feature = await context.Feature.FindAsync(id);
            if (feature == null) return null;
            return feature;
        }

        private void validate(Feature feature) {
            feature.Title = System.Text.RegularExpressions.Regex.Replace(feature.Title, @"\s+", " ");
            feature.Text = System.Text.RegularExpressions.Regex.Replace(feature.Text, @"\s+", " ");
            if (feature.GlyphIconName == null) feature.GlyphIconName = "glyphicon glyphicon-star";
            if (string.IsNullOrEmpty(feature.GlyphIconName.Trim()) ||
                string.IsNullOrWhiteSpace(feature.GlyphIconName.Trim())) feature.GlyphIconName = "glyphicon glyphicon-star";
        }
    }
}