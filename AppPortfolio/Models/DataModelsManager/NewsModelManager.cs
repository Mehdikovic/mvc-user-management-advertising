using AppPortfolio.Models.DataModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Models.DataModelsManager {
    public class NewsModelManager {
        private ApplicationDbContext context;
        private Controller http_controller;
        public NewsModelManager(Controller http_controller) {
            context = new ApplicationDbContext();
            this.http_controller = http_controller;
        }

        public async Task<bool> Add(HiringNews news, string date) {
            news.Date = date;
            context.HiringNews.Add(news);
            ValidateOnAdd(news);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (!check)
                DeleteRedundantFiles(news);
            return check;
        }

        private void DeleteRedundantFiles(HiringNews news) {
            if (news.Imagepath != null
                && news.Imagepath.StartsWith("/")
                && !news.Imagepath.Contains("no-image")) {
                string path = http_controller.Server.MapPath("~" + news.Imagepath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public static async Task<IEnumerable<HiringNews>> GetMainListAsync(ApplicationDbContext context) {
            return await context.HiringNews.OrderByDescending(o => o.Date).Take(20).ToListAsync();
        }

        private void ValidateOnAdd(HiringNews news) {
            if (news == null) return;
            news.Name = System.Text.RegularExpressions.Regex.Replace(news.Name, @"\s+", " ");
            news.Text = System.Text.RegularExpressions.Regex.Replace(news.Text, @"\s+", " ");
            if (String.IsNullOrEmpty(news.Imagepath) || String.IsNullOrWhiteSpace(news.Imagepath))
                news.Imagepath = "/wwwroot/img/no-image/700x300.png";
        }

        public async Task<bool> Update(int id, HiringNews news) {
            var oldNews = await context.HiringNews.FindAsync(id);
            if (oldNews == null) return false;
            ValidateOnUpdate(oldNews, news);
            oldNews.Imagepath = news.Imagepath?.Trim();
            oldNews.Name = news.Name.Trim();
            oldNews.Text = news.Text.Trim();
            oldNews.Tags = news.Tags.Trim();
            ValidateOnAdd(news);
            context.Entry(oldNews).State = System.Data.Entity.EntityState.Modified;
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (!check)
                DeleteRedundantFiles(news);
            return check;
        }

        private void ValidateOnUpdate(HiringNews oldNews, HiringNews news) {
            if ((oldNews.Imagepath != news.Imagepath)
                && oldNews.Imagepath.StartsWith("/")
                && !oldNews.Imagepath.Contains("no-image")) {
                string path = http_controller.Server.MapPath("~" + oldNews.Imagepath);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public async Task<bool> Delete(int id) {
            var oldNews = await context.HiringNews.FindAsync(id);
            if (oldNews == null) return false;
            context.HiringNews.Remove(oldNews);
            bool check = Convert.ToBoolean(await context.SaveChangesAsync());
            if (check)
                DeleteRedundantFiles(oldNews);
            return check;
        }

        public async Task<HiringNews> Find(int id) {
            var news = await context.HiringNews.FindAsync(id);
            if (news == null) return null;
            return news;
        }

        public static IEnumerable<HiringNews> GetListByDate(string date) {
            var context = new ApplicationDbContext();
            date = date.Replace('-', '/');
            return context.HiringNews.Where(w => w.Date == date).ToList();
        }

        public static HiringNews FindStatic(int id) {
            var context = new ApplicationDbContext();
            var news = context.HiringNews.Find(id);
            if (news == null) return null;
            return news;
        }

        public static IEnumerable<HiringNews> GetList() {
            var context = new ApplicationDbContext();
            return context.HiringNews.OrderByDescending(m => m.Date).ToList();
        }

        public static IEnumerable<NewsViewModel> GetGroupedListSecond() {
            var context = new ApplicationDbContext();
            return context.HiringNews.GroupBy(m => m.Date.Substring(0, 7)).OrderByDescending(o => o.Key).Select(s =>
            new NewsViewModel() {
                YearMonth = s.Key,
                NewsInDays = s.GroupBy(g => g.Date.Substring(8, 2)).OrderByDescending(O => O.Key).Select(SS =>
                new NewsInDaysViewModel() { Day = SS.Key, Count = SS.Count() }).ToList()
            });
        }

        public static IEnumerable<HiringNews> Search(HiringNews news) {
            if (news == null) return new List<HiringNews>();
            var context = new ApplicationDbContext();
            IQueryable<HiringNews> result = context.HiringNews;
            if (news.Name != null)
                result = result.Where(w => w.Name.Contains(news.Name));
            return result.OrderByDescending(o => o.Date).ToList();
        }

        public static async Task<IEnumerable<HiringNews>> SearchAsync(string q, ApplicationDbContext context) {
            return await context.HiringNews.Where(w => w.Name.Contains(q) || w.Tags.Contains(q) || w.Text.Contains(q)).ToListAsync();
        }
    }
}