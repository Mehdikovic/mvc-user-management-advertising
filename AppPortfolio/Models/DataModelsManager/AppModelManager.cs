using AppPortfolio.Controllers.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Models.DataModelsManager {
    public class AppModelManager {
        private ApplicationDbContext context;
        private Controller http_controller;

        public AppModelManager(Controller http_controller) {
            context = new ApplicationDbContext();
            this.http_controller = http_controller;
        }

        public async Task<bool> Add(App application, WorkType type) {
            application.Name = System.Text.RegularExpressions.Regex.Replace(application.Name, @"\s+", " ");
            application.Type = type;
            application.CreatedDate = IranDateTime.Now;
            application.UpdatedDate = IranDateTime.Now;
            application.Name = application.Name.Trim();
            application.Imagepath = application.Imagepath?.Trim();
            application.Filepath = application.Filepath.Trim();
            ValidateOnAdd(application);
            context.Apps.Add(application);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (!check)
                DeleteRedundantFiles(application);
            return check;
        }

        public static async Task<IEnumerable<App>> GetMainListAsync(ApplicationDbContext context) {
            var apps = await context.Apps.Where(w => w.Type == WorkType.EPT).ToListAsync();
            apps.AddRange(await context.Apps.Where(w => w.Type == WorkType.Other).Take(2).ToListAsync());
            return apps;
        }

        public static async Task<IEnumerable<App>> SearchAsync(string q, ApplicationDbContext context) {
            return await context.Apps.Where(w => w.Tags.Contains(q) || w.Name.Contains(q)).ToListAsync();
        }

        public async Task<bool> Update(int id, App NewApp) {
            var oldApplication = await context.Apps.FindAsync(id);
            if (oldApplication == null) return false;
            ValidateOnUpdate(oldApplication, NewApp);
            oldApplication.Name = System.Text.RegularExpressions.Regex.Replace(NewApp.Name, @"\s+", " ");
            oldApplication.Name = NewApp.Name.Trim();
            oldApplication.Imagepath = NewApp.Imagepath?.Trim();
            oldApplication.Filepath = NewApp.Filepath.Trim();
            oldApplication.Tags = NewApp.Tags.Trim();
            oldApplication.UpdatedDate = IranDateTime.Now;
            ValidateOnAdd(oldApplication);
            context.Entry(oldApplication).State = System.Data.Entity.EntityState.Modified;
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (!check)
                DeleteRedundantFiles(oldApplication);
            return check;
        }

        public async Task<bool> Delete(int id) {
            var application = await context.Apps.FindAsync(id);
            if (application == null) return false;
            context.Apps.Remove(application);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (check)
                DeleteRedundantFiles(application);
            return check;
        }

        public async Task<App> Find(int id) {
            var oldApplication = await context.Apps.FindAsync(id);
            if (oldApplication == null) return null;
            return oldApplication;
        }

        public static async Task<App> FindStatic(int id) {
            var context = new ApplicationDbContext();
            var oldApplication = await context.Apps.FindAsync(id);
            if (oldApplication == null) return null;
            return oldApplication;
        }

        public static IEnumerable<App> GetList() {
            var context = new ApplicationDbContext();
            return context.Apps.ToList();
        }

        public static IEnumerable<App> GetList(WorkType type) {
            var context = new ApplicationDbContext();
            return context.Apps.Where(m => m.Type == type).ToList();
        }

        // PRIVATE SECTION - METHODS
        private void ValidateOnAdd(App app) {
            if (app == null) return;
            if (String.IsNullOrEmpty(app.Imagepath) || String.IsNullOrWhiteSpace(app.Imagepath))
                app.Imagepath = "/wwwroot/img/no-image/1920x700.png";
        }

        private void ValidateOnUpdate(App OldApp, App NewApp) {
            if (OldApp.Filepath != NewApp.Filepath
                && OldApp.Filepath.StartsWith("/")) {
                string path = http_controller.Server.MapPath("~" + OldApp.Filepath);
                if (File.Exists(path))
                    File.Delete(path);
            }

            if ((OldApp.Imagepath != NewApp.Imagepath)
                && OldApp.Imagepath.StartsWith("/")
                && !OldApp.Imagepath.Contains("no-image")) {
                string path = http_controller.Server.MapPath("~" + OldApp.Imagepath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public static IEnumerable<App> Search(App application, WorkType type) {
            var context = new ApplicationDbContext();
            return context.Apps.Where(m => m.Type == type && m.Name.Contains(application.Name)).ToList();
        }

        private void DeleteRedundantFiles(App application) {
            if (application.Filepath != null
                && application.Filepath.StartsWith("/")) {
                string path = http_controller.Server.MapPath("~" + application.Filepath);
                if (File.Exists(path))
                    File.Delete(path);
            }

            if (application.Imagepath != null
                && application.Imagepath.StartsWith("/")
                && !application.Imagepath.Contains("no-image")) {
                string path = http_controller.Server.MapPath("~" + application.Imagepath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

    }
}