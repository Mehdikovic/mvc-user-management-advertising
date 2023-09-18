using AppPortfolio.Models.DataModels;
using AppPortfolio.Models.DataModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppPortfolio.Models.DataModelsManager {
    public class AIOManager {
        public static async Task<AIOViewModel> Search(string q) {
            if (q == "") return new AIOViewModel() {
                Apps = new List<App>(),
                Comments = new List<Comment>(),
                News = new List<HiringNews>(),
                Questions = new List<Question>()
            };
            var context = new ApplicationDbContext();
            return new AIOViewModel() {
                Apps = await AppModelManager.SearchAsync(q, context),
                Comments = await CommentModelManager.SearchAsync(q, context),
                News = await NewsModelManager.SearchAsync(q, context),
                Questions = await QuestionModelManager.SearchAsync(q, context)
            };
        }

        public static async Task<AIOViewModel> GetList() {
            var context = new ApplicationDbContext();
            return new AIOViewModel() {
                Apps = await AppModelManager.GetMainListAsync(context),
                Comments = await CommentModelManager.GetMainListAsync(context),
                News = await NewsModelManager.GetMainListAsync(context),
                Questions = await QuestionModelManager.GetMainListAsync(context)
            };
        }
    }
}