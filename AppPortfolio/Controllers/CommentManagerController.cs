using AppPortfolio.Models.DataModels.ViewModels;
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
    public class CommentManagerController : Controller {
        // GET: CommentManager
        [Route("CommentManager/Index/{newsID}/{page:int?}")]
        public ActionResult Index(int page = 1, int newsID = 0) {
            if (newsID <= 0) return Redirect("/NewsManager/Index");
            var model = CommentModelManager.GetCommentsByNewsID(newsID).ToPagedList(page, 20);
            return View(model: model);
        }

        public async Task<ActionResult> Valid(int id = 0, int newsID = 0) {
            if (id <= 0 || newsID <= 0) return RedirectToAction("Index", new { newsID = newsID });
            await new CommentModelManager().Valid(id);
            return RedirectToAction("Index", new { newsID = newsID });
        }

        public async Task<ActionResult> InValid(int id = 0, int newsID = 0) {
            if (id <= 0 || newsID <= 0) return RedirectToAction("Index", new { newsID = newsID });
            await new CommentModelManager().InValid(id);
            return RedirectToAction("Index", new { newsID = newsID });
        }

        public async Task<ActionResult> Delete(int id = 0, int newsID = 0) {
            if (id <= 0 || newsID <= 0) return RedirectToAction("Index", new { newsID = newsID });
            await new CommentModelManager().Delete(id);
            return RedirectToAction("Index", new { newsID = newsID });
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(CommentSearchViewModel vm) {
            var model = new CommentSearchViewModel() {
                Comment = vm.Comment,
                Comments = CommentModelManager.Search(vm.Comment)
            };
            return View(model: model);
        }
    }
}