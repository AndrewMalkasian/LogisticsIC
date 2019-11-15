using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using IndividualCapstone.Models;

[assembly: OwinStartup(typeof(IndividualCapstone.Startup))]
namespace IndividualCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }
        private void CreateRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole
                {
                    Name = "Customer"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.Create(role);
            }
        }
    }
}

