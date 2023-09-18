using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Models.DataModelsManager {
    public class PathLogModelManager {

        private Controller http_controller;
        private ApplicationDbContext context;
        public PathLogModelManager(Controller http_controller) {
            context = new ApplicationDbContext();
            this.http_controller = http_controller;
        }

        public bool Add(PathLog log) {
            context.PathLog.Add(log);
            return Convert.ToBoolean(context.SaveChanges());
        }

        public static IEnumerable<PathLog> GetUnusedFilesList(Controller http_controller) {
            var context = new ApplicationDbContext();
            List<string> usedFiles = new List<string>();

            List<PathLog> allFiles = context.PathLog.ToList();
            foreach (var item in allFiles) {
                if (!File.Exists(http_controller.Server.MapPath("~" + item.Path)))
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }

            context.SaveChanges();
            allFiles = context.PathLog.ToList();

            usedFiles.AddRange(context.Apps.Select(S => S.Filepath).ToList());
            usedFiles.AddRange(context.Apps.Select(S => S.Imagepath).ToList());

            usedFiles.AddRange(context.Question.Select(S => S.Filepath).ToList());
            usedFiles.AddRange(context.HiringNews.Select(S => S.Imagepath).ToList());

            var result = new List<PathLog>();

            foreach (var first in allFiles) {
                bool isValid = true;
                foreach (var second in usedFiles) {
                    if (first.Path == second) {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                    result.Add(first);
            }
            return result;
        }

        public bool Delete(int id) {
            var log = context.PathLog.Find(id);
            if (log == null) return false;
            string path = this.http_controller.Server.MapPath("~" + log.Path);
            if (File.Exists(path))
                File.Delete(path);
            context.Entry(log).State = System.Data.Entity.EntityState.Deleted;
            return Convert.ToBoolean(context.SaveChanges());
        }

        public void DeleteAll() {
            foreach (var item in GetUnusedFilesList(this.http_controller)) {
                string path = this.http_controller.Server.MapPath("~" + item.Path);
                if (File.Exists(path))
                    File.Delete(path);
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}