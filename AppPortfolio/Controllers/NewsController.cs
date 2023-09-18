using AppPortfolio.Models.DataModels;
using AppPortfolio.Models.DataModels.ViewModels;
using AppPortfolio.Models.DataModelsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {
    public class NewsController : Controller {
        // GET: News
        public ActionResult List() {
            var news_list = NewsModelManager.GetGroupedListSecond().ToList();
            return View(model: news_list);
        }

        public async Task<ActionResult> Details(int id = 0) {
            if (id <= 0)
                return RedirectToAction("List");
            var news = await new NewsModelManager(this).Find(id);
            if (news == null) return RedirectToAction("List");

            var model = new NewsCommentViewModel() {
                InValidCommentCount = CommentModelManager.InValidCommentCount(id),
                ValidCommentCount = CommentModelManager.ValidCommentCount(id),
                ResponseCommentCount = CommentModelManager.ResponseCommentCount(id),
                Comment = new Models.DataModels.Comment(),
                Comments = CommentModelManager.GetComments(id),
                News = news
            };

            return View(model: model);

        }

        public ActionResult NewsList(string id) {
            if (id == null) RedirectToAction("List");
            ViewBag.Date = id;
            var news_list = NewsModelManager.GetListByDate(id).ToList();
            return View(model: news_list);
        }

        public async Task<ActionResult> AddComment(int newsID, NewsCommentViewModel vm) {
            if (!ModelState.IsValid) return RedirectToAction("Details", new { id = newsID });
            var comment_manager = new CommentModelManager();
            if (await comment_manager.Add(vm.Comment, newsID)) {
                
            }
            return RedirectToAction("Details", new { id = newsID });
        }

        [Authorize(Roles = "FullAdministrator")]
        public async Task<ActionResult> ValidateComment(int newsID, int commentID) {
            if (newsID <= 0 || commentID <= 0) return RedirectToAction("Details", new { id = newsID });
            var comment_manager = new CommentModelManager();
            await comment_manager.Update(newsID, commentID);
            return RedirectToAction("Details", new { id = newsID });
        }

        [Authorize(Roles = "FullAdministrator")]
        [HttpGet]
        public async Task<ActionResult> Respond(int newsID = 0, int parentID = 0) {
            if (newsID <= 0 || parentID <= 0) return RedirectToAction("Details", new { id = newsID });
            var comment = await new CommentModelManager().Find(parentID);
            if (comment == null) return RedirectToAction("Details", new { id = newsID });
            return View(model: comment);
        }

        [Authorize(Roles = "FullAdministrator")]
        [HttpPost]
        public async Task<ActionResult> Respond(Comment comment, int newsID = 0, int parentID = 0) {
            if (newsID <= 0 || parentID <= 0 || comment.Text == null) return RedirectToAction("Details", new { id = newsID });
            if (await new CommentModelManager().AddChild(newsID, parentID, this, comment))
                return RedirectToAction("Details", new { id = newsID });
            return View(model: comment);
        }

    }
}