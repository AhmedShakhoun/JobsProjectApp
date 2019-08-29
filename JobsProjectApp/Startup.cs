using JobsProjectApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobsProjectApp.Startup))]
namespace JobsProjectApp
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
            createUsers();
        }
        public void createRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole rol;
            if(!roleManager.RoleExists("Admins"))
            {
                rol = new IdentityRole();
                rol.Name = "Admins";
                roleManager.Create(rol);
            }
            if (!roleManager.RoleExists("Publisher"))
            {
                rol = new IdentityRole();
                rol.Name = "Publisher";
                roleManager.Create(rol);
            }
            if (!roleManager.RoleExists("Applicant"))
            {
                rol = new IdentityRole();
                rol.Name = "Applicant";
                roleManager.Create(rol);
            }
        }
        public void createUsers()
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "Asd123@gmail.com";
            user.UserName = "AhmedRamadan";
            user.PhoneNumber = "01000234320";
            user.UserType = "Admins";
            var check = usermanager.Create(user, "2282ASDasd@$1");
            if(check.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Admins");
            }
        }
    }
}
