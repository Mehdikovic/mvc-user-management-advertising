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


    public class CommentModelManager {

        ApplicationDbContext context;

        public CommentModelManager() {
            context = new ApplicationDbContext();
        }

        public static IEnumerable<CommentViewModel> GetComments(int newsID) {
            var context = new ApplicationDbContext();
            return context.Comments.Where(m => m.NewsID == newsID && m.ParantID == 0).Select(S =>
            new CommentViewModel() {
                ParentComment = S,
                ChildComments = context.Comments.Where(w => w.NewsID == newsID && w.ParantID == S.ID).ToList()
            });

        }

        public async Task<bool> Valid(int id) {
            var model = await context.Comments.FindAsync(id);
            if (model == null) return false;
            model.IsValid = true;
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public static async Task<IEnumerable<Comment>> GetMainListAsync(ApplicationDbContext context) {
            return await context.Comments.Where(w => w.IsValid && w.ParantID == 0).OrderByDescending(o => o.Date).Take(20).ToListAsync();
        }

        public async Task<bool> Delete(int id) {
            var model = await context.Comments.FindAsync(id);
            if (model == null) return false;
            context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            foreach (var item in context.Comments.Where(m => m.ParantID == id))
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> InValid(int id) {
            var model = await context.Comments.FindAsync(id);
            if (model == null) return false;
            model.IsValid = false;
            context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public static IEnumerable<Comment> Search(Comment comment) {
            if (comment == null) return new List<Comment>();
            var context = new ApplicationDbContext();
            IQueryable<Comment> result = context.Comments.Where(w => w.IsValid == comment.IsValid && w.ParantID == 0);
            if (comment.Name != null)
                result = result.Where(w => w.Name.Contains(comment.Name));
            if (comment.Email != null)
                result = result.Where(w => w.Email.Contains(comment.Email));
            if (comment.Text != null)
                result = result.Where(w => w.Text.Contains(comment.Text));

            return result.ToList();
        }

        public static async Task<IEnumerable<Comment>> SearchAsync(string q, ApplicationDbContext context) {
            return await context.Comments.Where(w => w.IsValid == true && (w.Name.Contains(q) || w.Text.Contains(q))).ToListAsync();
        }

        public static IEnumerable<Comment> GetCommentsByNewsID(int newsID) {
            var context = new ApplicationDbContext();
            return context.Comments.Where(w => w.NewsID == newsID).ToList();

        }

        public static int ValidCommentCount(int newsID) {
            var context = new ApplicationDbContext();
            return context.Comments.Where(m => m.NewsID == newsID && m.IsValid == true && m.ParantID == 0).Count();
        }

        internal static int InValidCommentCount(int newsID) {
            var context = new ApplicationDbContext();
            return context.Comments.Where(m => m.NewsID == newsID && m.IsValid == false && m.ParantID == 0).Count();
        }

        internal static int ResponseCommentCount(int newsID) {
            var context = new ApplicationDbContext();
            return context.Comments.Where(m => m.NewsID == newsID && m.ParantID != 0).Count();
        }

        public async Task<bool> Add(Comment comment, int newsID) {
            comment.Text = System.Text.RegularExpressions.Regex.Replace(comment.Text, @"\s+", " ");
            comment.NewsID = newsID;
            comment.IsValid = false;
            comment.ParantID = 0;
            comment.Date = IranDateTime.Now;
            context.Comments.Add(comment);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> AddChild(int newsID, int parentID, Controller control, Comment comment) {
            var comment_2 = new Comment() {
                IsValid = true,
                NewsID = newsID,
                Name = "Admin",
                ParantID = parentID,
                Email = control.User.Identity.Name,
                Text = System.Text.RegularExpressions.Regex.Replace(comment.Text, @"\s+", " ")
            };
            comment_2.Date = IranDateTime.Now;
            context.Comments.Add(comment_2);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> Update(int newsID, int commentID) {
            var comment = await context.Comments.FindAsync(commentID);
            if (comment == null) return false;
            comment.IsValid = true;
            context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<Comment> Find(int parentID) {
            return await context.Comments.FindAsync(parentID);
        }
    }
}