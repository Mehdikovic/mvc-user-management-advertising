using AppPortfolio.Models.DataModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppPortfolio.Models.DataModels;
using System.Threading.Tasks;
using AppPortfolio.Controllers.Utilities;
using System.Web.Mvc;
using System.Data.Entity;

namespace AppPortfolio.Models.DataModelsManager {
    public class MessageModelManager {

        ApplicationDbContext context;

        public MessageModelManager() {
            context = new ApplicationDbContext();
        }

        public static IEnumerable<Message> GetMessages() {
            var context = new ApplicationDbContext();
            return context.Messages.OrderByDescending(o => o.Date).ToList();
        }

        public static async Task<IEnumerable<Message>> GetMessageListAsync(ApplicationDbContext context) {
            return await context.Messages.OrderByDescending(o => o.Date).ToListAsync();
        }

        public async Task<bool> Delete(int id) {
            var model = await context.Messages.FindAsync(id);
            if (model == null) return false;
            context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public static async Task<IEnumerable<Message>> SearchAsync(string q, ApplicationDbContext context) {
            if (q == null)
                return await GetMessageListAsync(context);
            else if (q.Trim() == "")
                return await GetMessageListAsync(context);
            return await context.Messages.Where(w => w.Subject.Contains(q) || w.Text.Contains(q)).ToListAsync();
        }

        public async Task<bool> Add(Message message) {
            message.Date = IranDateTime.Now;
            context.Messages.Add(message);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }
    }
}
