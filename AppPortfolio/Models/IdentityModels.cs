using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AppPortfolio.Models.DataModels;

namespace AppPortfolio.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<App> Apps { set; get; }
        public DbSet<PathLog> PathLog { set; get; }
        public DbSet<Customer> Customer { set; get; }
        public DbSet<Feature> Feature { set; get; }
        public DbSet<HiringNews> HiringNews { set; get; }
        public DbSet<Question> Question { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Message> Messages { set; get; }
        public DbSet<Junc_App_Feature> Junc_App_Feature { set; get; }
        public DbSet<Junc_Customer_Question> Junc_Customer_Question { set; get; }
    }
}