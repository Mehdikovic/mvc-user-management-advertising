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
    public class MessageManagerController : Controller {
        // GET: MessageManager
        [Route("MessageManager/Index/{page:int?}")]
        public ActionResult Index(int page = 1) {
            var model = MessageModelManager.GetMessages().ToPagedList(page, 20);
            return View(model: model);
        }

        public async  Task<ActionResult> Delete(int id) {
            if (id <= 0) return RedirectToAction("Index");
            await new MessageModelManager().Delete(id);
            return RedirectToAction("Index");
        }
    }
}