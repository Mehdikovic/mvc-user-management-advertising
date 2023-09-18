using AppPortfolio.Controllers.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppPortfolio.Models.DataModels.ViewModels;

using AppPortfolio.Controllers;
using System.Data.Entity;

namespace AppPortfolio.Models.DataModelsManager {
    public class QuestionModelManager {
        private ApplicationDbContext context;
        private Controller http_controller;

        public QuestionModelManager(Controller http_controller) {
            context = new ApplicationDbContext();
            this.http_controller = http_controller;
        }

        public static IEnumerable<Question> GetListOfPastYears(WorkType other) {
            var context = new ApplicationDbContext();
            List<Question> model = context.Question.Where(m => m.Type == other).ToList();
            return model.Where(m => m.CreatedDate.ToFullPersianDate().Year < IranDateTime.Now.ToFullPersianDate().Year);
        }

        public static async Task<IEnumerable<Question>> SearchAsync(string q, ApplicationDbContext context) {
            return await context.Question.Where(w => w.Name.Contains(q) || w.Tags.Contains(q) || w.Description.Contains(q)).ToListAsync();
        }

        public static IEnumerable<Question> GetListOfCurrentYear(WorkType other) {
            var context = new ApplicationDbContext();
            List<Question> model = context.Question.Where(m => m.Type == other).ToList();
            return model.Where(m => m.CreatedDate.ToFullPersianDate().Year == IranDateTime.Now.ToFullPersianDate().Year);
        }

        public static async Task<IEnumerable<Question>> GetMainListAsync(ApplicationDbContext context) {
            return await context.Question.OrderByDescending(o => o.CreatedDate).Take(20).ToListAsync();
        }

        public async Task<bool> Add(Question question, WorkType type) {
            question.Type = type;
            question.Name = System.Text.RegularExpressions.Regex.Replace(question.Name, @"\s+", " ");
            question.Description = System.Text.RegularExpressions.Regex.Replace(question.Description, @"\s+", " ");
            question.CreatedDate = IranDateTime.Now;
            question.Filepath = question.Filepath.Trim();
            context.Question.Add(question);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (!check)
                DeleteRedundantFiles(question);
            return check;
        }

        public static async Task<Question> FindStatic(int id) {
            if (id <= 0) return null;
            var context = new ApplicationDbContext();
            var oldQuestion = await context.Question.FindAsync(id);
            if (oldQuestion == null) return null;
            return oldQuestion;
        }

        public async Task<bool> Update(QuestionUpdateViewModel qvm) {
            var oldQuestion = await Find(qvm.ID);
            if (oldQuestion == null) return false;
            oldQuestion.Name = System.Text.RegularExpressions.Regex.Replace(qvm.Name, @"\s+", " ");
            oldQuestion.Description = System.Text.RegularExpressions.Regex.Replace(qvm.Description, @"\s+", " ");
            oldQuestion.Tags = qvm.Tags.Trim();
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public async Task<bool> Delete(int id) {
            var question = await context.Question.FindAsync(id);
            if (question == null) return false;
            context.Question.Remove(question);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (check)
                DeleteRedundantFiles(question);
            return check;
        }

        public async Task<Question> Find(int id) {
            var oldQuestion = await context.Question.FindAsync(id);
            if (oldQuestion == null) return null;
            return oldQuestion;
        }

        public static Question FindWithPath(string filePath) {
            if (filePath == null) return null;
            filePath = "~" + filePath;
            var context = new ApplicationDbContext();
            return context.Question.Where(m => m.Filepath == filePath).FirstOrDefault();
        }

        public async Task<QuestionUpdateViewModel> FindForViewModel(int id) {
            var question = await Find(id);
            if (question == null) return null;
            return new QuestionUpdateViewModel() { ID = id, Name = question.Name, Description = question.Description, Tags = question.Tags };
        }

        public static IEnumerable<Question> GetList() {
            var context = new ApplicationDbContext();
            return context.Question.ToList();
        }

        public static IEnumerable<Question> GetList(WorkType type) {
            var context = new ApplicationDbContext();
            return context.Question.Where(m => m.Type == type).ToList();
        }

        public static IEnumerable<Question> Search(Question question, WorkType type) {
            if (question == null) return new List<Question>();
            var context = new ApplicationDbContext();
            IQueryable<Question> result = context.Question.Where(w => w.Type == type);
            if (question.Name != null)
                result = result.Where(w => w.Name.Contains(question.Name));
            return result.ToList();
        }

        // PRIVATE SECTION
        private void DeleteRedundantFiles(Question question) {
            if (question.Filepath != null
                && question.Filepath.StartsWith("/")) {
                string path = http_controller.Server.MapPath("~" + question.Filepath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
    }
}