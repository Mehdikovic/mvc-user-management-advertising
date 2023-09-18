using AppPortfolio.Controllers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppPortfolio.Models.DataModelsManager {
    public class CustomerModelManager {
        ApplicationDbContext context;
        public CustomerModelManager() {
            context = new ApplicationDbContext();
        }

        public static IEnumerable<Customer> GetList() {
            var context = new ApplicationDbContext();
            return context.Customer.ToList();
        }

        public static IEnumerable<Customer> GetList(WorkType type) {
            var context = new ApplicationDbContext();
            var list = new List<Customer>();
            var customers = context.Customer.Where(w => w.Customer_Question.Count != 0).ToList();
            foreach (var customer in customers) {
                foreach (var customer_question in customer.Customer_Question) {
                    if (customer_question.Question.Type == type) {
                        list.Add(customer);
                        break;
                    }
                }
            }
            return list;
        }

        public async Task<bool> Add(Customer customer) {
            customer.DateOfRegister = IranDateTime.Now;
            customer.Email = customer.Email.Trim();
            customer.Phone = customer.Phone.Trim();
            context.Customer.Add(customer);
            return Convert.ToBoolean(await context.SaveChangesAsync());
        }

        public static bool IsCustomerExist(string cookieValue) {
            if (cookieValue == null) return false;
            var context = new ApplicationDbContext();
            string email = Encryption.Decrypt(cookieValue, "@#-7486-$#-34956");
            return context.Customer.Any(m => m.Email == email);
        }

        public static string EncryptCustomer(Customer customer) {
            if (customer == null) throw new NullReferenceException();
            if (customer.Email == null) throw new NullReferenceException();
            return Encryption.Encrypt(customer.Email, "@#-7486-$#-34956");
        }

        public static IEnumerable<Customer> Search(Customer customer) {
            if (customer == null) return new List<Customer>();
            var context = new ApplicationDbContext();
            IQueryable<Customer> result = context.Customer;
            if (customer.Email != null)
                result = result.Where(w => w.Email.Contains(customer.Email));
            if (customer.Phone != null)
                result = result.Where(w => w.Phone.Contains(customer.Phone));
            return result.ToList();
        }

        public bool Exist(Customer customer) {
            if (customer == null) throw new NullReferenceException();
            if (customer.Email == null) throw new NullReferenceException();
            return context.Customer.Any(m => m.Email == customer.Email && m.Phone == customer.Phone);
        }

        public static Customer Find(string cookieValue) {
            if (cookieValue == null) return null;
            var context = new ApplicationDbContext();
            string email = Encryption.Decrypt(cookieValue, "@#-7486-$#-34956");
            return context.Customer.FirstOrDefault(m => m.Email == email);
        }
    }
}