using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web_MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
//            Database.SetInitializer(new ApplicationDbInitializer());
//            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Article> Articles { get; set; }
//        public DbSet<Comment> Comments { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    }
}