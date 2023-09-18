using AppPortfolio.Controllers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModelsManager {
    public class JCQ_ModelManager {

        private ApplicationDbContext context;

        public JCQ_ModelManager() {
            context = new ApplicationDbContext();
        }

        public static IEnumerable<Junc_Customer_Question> SelectByCustomerId(int id) {
            if (id <= 0) return new List<Junc_Customer_Question>();
            var context = new ApplicationDbContext();
            return context.Junc_Customer_Question.Where(m => m.CustomerID == id).ToList();
        }

        public bool Add(Customer customer, Question question) {
            if (customer == null || question == null) return false;
            var Junc_C_Q = new Junc_Customer_Question() {
                CustomerID = customer.ID,
                QuestionID = question.ID,
                DateOfDownload = IranDateTime.Now
            };
            var last_attempt = context.Junc_Customer_Question
                .Where(m => m.CustomerID == customer.ID && m.QuestionID == question.ID)
                .OrderByDescending(m => m.DateOfDownload)
                .FirstOrDefault();
            if (last_attempt == null) {
                context.Junc_Customer_Question.Add(Junc_C_Q);
                return Convert.ToBoolean(context.SaveChanges());
            }
            else if (last_attempt.DateOfDownload.AddMinutes(30) < Junc_C_Q.DateOfDownload) {
                context.Junc_Customer_Question.Add(Junc_C_Q);
                return Convert.ToBoolean(context.SaveChanges());
            }
            return true;
        }
    }
}