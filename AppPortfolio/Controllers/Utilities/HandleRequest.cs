using AppPortfolio.Models;
using AppPortfolio.Models.DataModelsManager;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace AppPortfolio.Controllers.Utilities {
    public class HandleRequest : IHttpModule {

        public void Init(HttpApplication httpApp) {
            httpApp.BeginRequest += OnBeginRequest;
            httpApp.EndRequest += OnEndRequest;
        }

        // Record the time of the begin request event.
        public void OnBeginRequest(Object sender, EventArgs e) {
            var httpApp = (HttpApplication)sender;
            if (httpApp.Request.Cookies[".AspNet.ApplicationCookie"] != null)
                return;

            var cookie = httpApp.Request.Cookies.Get("__customer_identifier");
            var isCustomerExist = CustomerModelManager.IsCustomerExist(cookie?.Value);
            if ((httpApp.Request.Path.StartsWith("/wwwroot/uploads/doc/")
                || (httpApp.Request.Path.StartsWith("/Question/Details/"))
                //|| (httpApp.Request.Path.StartsWith("/EPT/Questions"))
                )
                && !isCustomerExist) {
                httpApp.Response.Redirect("/Customer/Register");
            } else if (httpApp.Request.Path.Contains("/Customer/Register")
                && isCustomerExist) {
                httpApp.Response.Redirect("/");
            }

        }

        public void OnEndRequest(object sender, EventArgs e) {
            var httpApp = (HttpApplication)sender;
            var cookie = httpApp.Request.Cookies.Get("__customer_identifier");
            var customer = CustomerModelManager.Find(cookie?.Value);
            if (httpApp.Request.Path.Contains("/wwwroot/uploads/doc/") && customer != null) {
                var question = QuestionModelManager.FindWithPath(httpApp.Request.Path);
                if (!(new JCQ_ModelManager().Add(customer, question)))
                    httpApp.Response.Redirect("/Home/Error");

            }
        }

        public void Dispose() { /* Not needed */ }
    }

}
