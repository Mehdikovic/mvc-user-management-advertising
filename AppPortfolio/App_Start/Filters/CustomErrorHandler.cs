using System;
using System.Web.Mvc;

namespace AppPortfolio.App_Start.Filters {
    public class CustomErrorHandler : HandleErrorAttribute {
        public override void OnException(ExceptionContext filterContext) {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var result = new ViewResult() {
                ViewName = "__Error_Handler"
            };
            result.ViewBag.Error = "خطایی هنگام عملیات رخ داده است، لطفاً به پشتیبانی اطلاع دهید";
            filterContext.Result = result;
        }
    }
}