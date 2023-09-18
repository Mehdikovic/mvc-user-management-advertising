using AppPortfolio.Controllers.Utilities;
using AppPortfolio.Models.DataModelsManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {

    [Authorize(Roles = "FullAdministrator")]
    public class UploaderController : Controller {

        public ActionResult Index() {
            return View(model: PathLogModelManager.GetUnusedFilesList(this));
        }

        public ActionResult Delete(int id) {
            if (new PathLogModelManager(this).Delete(id))
                return RedirectToAction("Index");
            return View("Error");
        }

        public ActionResult DeleteAll() {
            new PathLogModelManager(this).DeleteAll();
            return RedirectToAction("Index");
        }

        public ActionResult Uploader() {
            return View();
        }

        [Authorize(Roles = "FullAdministrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Uploader")]
        public ActionResult UploaderX() {
            if (!FileIsValid(HttpContext.Request)) {
                ViewBag.Message = "فایل معتبری یافت نشد";
                return View("Uploader");
            }
            var file = Request.Files[0];
            string extension = Path.GetExtension(file.FileName);
            //string newName = MakeName(extension);
            string newName = file.FileName;
            var path_manager = new PathLogModelManager(this);
            switch (extension.ToUpper()) {
                case ".JPEG":
                case ".JPG":
                case ".PNG":
                case ".JFIF":
                case ".Exif":
                case ".GIF":
                case ".BMP":
                    string newImagePath = Path.Combine("~/wwwroot/uploads/image/", newName);
                    file.SaveAs(Server.MapPath(newImagePath));
                    path_manager.Add(new Models.PathLog { Path = newImagePath.Substring(1) });
                    ViewBag.Path = newImagePath.Substring(1);
                    return View("Uploader");

                case ".APK":
                    string newAPKPath = Path.Combine("~/wwwroot/uploads/apk/", newName);
                    file.SaveAs(Server.MapPath(newAPKPath));
                    path_manager.Add(new Models.PathLog { Path = newAPKPath.Substring(1) });
                    ViewBag.Path = newAPKPath.Substring(1);
                    return View("Uploader");

                case ".PDF":
                    string newPDFPath = Path.Combine("~/wwwroot/uploads/doc/", newName);
                    file.SaveAs(Server.MapPath(newPDFPath));
                    path_manager.Add(new Models.PathLog { Path = newPDFPath.Substring(1) });
                    ViewBag.Path = newPDFPath.Substring(1);
                    return View("Uploader");

                default:
                    ViewBag.Message = "فایل معتبری یافت نشد";
                    return View("Uploader");
            }
        }

        private string MakeName(string extension) {
            return new StringBuilder()
                .Append(IranDateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"))
                .Append("--")
                .Append(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString())
                .Append(extension).ToString();
        }

        private bool FileIsValid(HttpRequestBase request) {
            if (request.Files.Count != 1)
                return false;
            var file = request.Files[0];
            if (file != null && file.ContentLength > 0)
                return true;
            return false;
        }
    }
}