using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web_MVC.Models;

namespace Web_MVC.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleAdmin = context.Roles.Find("");
            if (roleAdmin == null)
            {
                roleAdmin = new ApplicationRole("Admin");
                context.Roles.Add(roleAdmin);
            }
            if (context.Roles.Find("User") == null)
                context.Roles.Add(new ApplicationRole("User"));

            var userId = Guid.NewGuid().ToString();
            var hashPassword = new PasswordHasher().HashPassword("admin");
            context.Users.Add(new ApplicationUser{
                Id = userId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PasswordHash = hashPassword,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Roles = { new IdentityUserRole{UserId = userId, RoleId = roleAdmin.Id} } });
//            SaveChanges(context);
            SaveChangesIngoreExceptions(context);
        }

        private void SaveChangesIngoreExceptions(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch
            {
                //ignore
            }
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" + sb, ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
