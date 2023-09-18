using AppPortfolio.Models;
using AppPortfolio.Models.DataModels.ViewModels.SearchViewModels;
using AppPortfolio.Models.DataModelsManager;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Controllers {

    [Authorize(Roles = "FullAdministrator")]
    public class CustomerManagerController : Controller {
        // GET: CustomerManager
        [Route("CustomerManager/Index/{page:int?}")]
        public ActionResult Index(int page = 1) {
            var model = CustomerModelManager.GetList().ToPagedList(page, 10);
            return View(model: model);
        }

        public ActionResult EPTList(int page = 1) {
            var model = CustomerModelManager.GetList(WorkType.EPT).ToPagedList(page, 10);
            return View(model: model);
        }

        public ActionResult OtherList(int page = 1) {
            var model = CustomerModelManager.GetList(WorkType.Other).ToPagedList(page, 10);
            return View(model: model);
        }

        [HttpGet]
        [Route("CustomerManager/Details/{id:int}/{page:int?}")]
        public ActionResult Details(int id = 0, int page = 1) {
            if (id <= 0)
                return Redirect("/CustomerManager/Index");
            var model = JCQ_ModelManager.SelectByCustomerId(id).ToPagedList(page, 20);
            return View(model: model);
        }

        [HttpGet]
        public ActionResult Search() {
            return View();
        }

        public ActionResult Search(CustomerSearchViewModel vm) {
            var model = new CustomerSearchViewModel() {
                Customer = vm.Customer,
                Customers = CustomerModelManager.Search(vm.Customer)
            };
            return View(model: model);
        }
    }
}