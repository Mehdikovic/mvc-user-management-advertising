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
    public class CustomerController : Controller {
        // GET: Customer
        private enum MessageType {
            WrongData,
            OperationError
        }

        public ActionResult Register() {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Register(CustomerViewModel customerViewmodel) {
            var customer = customerViewmodel.Customer;
            if (!ModelState.IsValid) {
                FillRegisterViewBags(MessageType.WrongData);
                return View(model: customerViewmodel);
            }
            var customer_manager = new CustomerModelManager();
            if (await customer_manager.Add(customer)) {
                AddToCookies(customer);
                return Redirect("/Question/List/1");
            }
            FillRegisterViewBags(MessageType.OperationError);
            return View(model: customerViewmodel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerViewModel customerViewmodel) {
            var customerLogin = customerViewmodel.CustomerLogin;
            if (!ModelState.IsValid) {
                FillLogInViewBag(MessageType.WrongData);
                return View("Register", model: customerViewmodel);
            }
            var customer_manager = new CustomerModelManager();
            var customer = new Customer { Email = customerLogin.Email, Phone = customerLogin.Phone };

            if (customer_manager.Exist(customer)) {
                AddToCookies(customer);
                return Redirect("/Question/List/1");
            }
            FillLogInViewBag(MessageType.OperationError);
            return View("Register", model: customerViewmodel);
        }

        [Route("User/Customer/Logoff")]
        public ActionResult LogOff() {
            LogOut();
            return Redirect("/");
        }

        private void LogOut() {
            if (Request.Cookies["__customer_identifier"] != null) {
                var c = new HttpCookie("__customer_identifier");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            };
        }

        private bool AddToCookies(Customer customer) {
            try {
                var cookie = new HttpCookie("__customer_identifier", CustomerModelManager.EncryptCustomer(customer));
                cookie.Expires = DateTime.UtcNow.AddDays(15);
                cookie.HttpOnly = true;
                cookie.Secure = true;
                Response.Cookies.Set(cookie);
                return true;
            } catch (Exception) {
                return false;
            }

        }
        private void FillRegisterViewBags(MessageType type) {
            switch (type) {
                case MessageType.WrongData:
                    ViewBag.RegisterPrefix = "ثبت نام: ";
                    ViewBag.RegisterError = "لطفاً از صحیح بودن اطلاعات اطمینان حاصل نمایید";
                    break;
                case MessageType.OperationError:
                    ViewBag.RegisterPrefix = "ثبت نام: ";
                    ViewBag.RegisterError = "متاسفانه ثبت با خطا مواجه شد";
                    break;
                default:
                    break;
            }

        }

        private void FillLogInViewBag(MessageType type) {
            switch (type) {
                case MessageType.WrongData:
                    ViewBag.LogInPrefix = "ورود: ";
                    ViewBag.LogInError = "لطفاً از صحیح بودن اطلاعات اطمینان حاصل نمایید";
                    break;
                case MessageType.OperationError:
                    ViewBag.LogInPrefix = "ورود: ";
                    ViewBag.LogInError = "ورود با خطا مواجه شد";
                    break;
                default:
                    break;
            }
        }
    }
}