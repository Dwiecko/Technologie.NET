using DeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeliverySystem.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {

            _context.Database.Migrate();


            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    Roles.Administrator,
                    Roles.User,

                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = roleName.ToUpper() };
                    _context.Roles.Add(role);
                }
            }


            if (!_context.ApplicationUsers.Any())
            {
                const string userName = "admin@admin.com";
                const string userPass = "Test!23";

                var user = new ApplicationUser { UserName = userName, Email = userName };
                _userManager.CreateAsync(user, userPass).Wait();
                _userManager.AddToRoleAsync(user, Roles.Administrator).Wait();

                const string userName2 = "user@user.com";
                const string userPass2 = "Test!23";

                var user2 = new ApplicationUser { UserName = userName2, Email = userName2 };
                _userManager.CreateAsync(user2, userPass2).Wait();
                _userManager.AddToRoleAsync(user2, Roles.User).Wait();
            }

            _context.SaveChanges();
        }

    }

}
