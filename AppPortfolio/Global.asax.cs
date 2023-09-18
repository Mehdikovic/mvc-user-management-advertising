using AppPortfolio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppPortfolio {
    public class MvcApplication : System.Web.HttpApplication {


        protected void Application_Start() {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
            MyDatabaeInitializer.Seed(new ApplicationDbContext());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            var culture = new CultureInfo("en-EN");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        protected void Application_PreSendRequestHeadres() {
            Response.Headers.Remove("Server");           //Remove Server Header   
            Response.Headers.Remove("X-AspNet-Version"); //Remove X-AspNet-Version Header
        }

        public class MyDatabaeInitializer {
            public static void Seed(ApplicationDbContext context) {
                if (!context.Users.Any(u => u.UserName == "SOME_DATA_WILL_BE_READ_LOCALLY_HERE")) {
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    var user1 = new ApplicationUser { UserName = "SOME_DATA_WILL_BE_READ_LOCALLY_HERE", Email = "SOME_DATA_WILL_BE_READ_LOCALLY_HERE" };
                    var user2 = new ApplicationUser { UserName = "SOME_DATA_WILL_BE_READ_LOCALLY_HERE", Email = "SOME_DATA_WILL_BE_READ_LOCALLY_HERE" };
                    IdentityResult result1 = userManager.Create(user1, "SOME_DATA_WILL_BE_READ_LOCALLY_HERE");
                    IdentityResult result2 = userManager.Create(user2, "SOME_DATA_WILL_BE_READ_LOCALLY_HERE");
                    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "FullAdministrator" });
                    context.SaveChanges();
                    userManager.AddToRole(user1.Id, "FullAdministrator");
                    userManager.AddToRole(user2.Id, "FullAdministrator");
                    context.SaveChanges();
                }
            }
        }
    }
}
