using AppPortfolio.Models;
using AppPortfolio.Models.DataModels;
using AppPortfolio.Models.DataModelsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {
    public class HomeController : Controller {
        public async Task<ActionResult> Index() {
            var model = await AIOManager.GetList();
            return View(model: model);
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Message message) {
            try
            {
                if (!ModelState.IsValid) {
                    ViewBag.Error = "لطفاً از صحیح بودن فیلد ها اطمینان حاصل کنید";
                    return View(model: message);
                }
                var message_manager = new MessageModelManager();

                if (await message_manager.Add(message)) {
                    ModelState.Clear();
                    ViewBag.Success = "پیام شما با موفقیت دریافت شد";
                    return View();
                }
                ViewBag.Error = "ارسال پیام با خطا مواجه شد";
                return View(model: message);
            } catch {
                ViewBag.Error = "ارسال پیام با خطا مواجه شد";
                return View(model: message);
            }
        }

        public ActionResult Error() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string q) {
            ViewBag.Query = q;
            var model = await AIOManager.Search(q);
            return View(model: model);
        }
    }
}