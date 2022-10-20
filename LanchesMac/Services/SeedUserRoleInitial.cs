using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles() //criação de perfil "membro" e "admin"
        {
            if (!_roleManager.RoleExistsAsync("Member").Result) //se o perfil não exister ele entra no if
            {
                IdentityRole role = new IdentityRole(); //cria uma nova role
                role.Name = "Member"; //atribui o nome a essa role
                role.NormalizedName = "MEMBER"; // atribui o mesmo nome em caixa alta a role
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result; //cria a role
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result) //se o perfil não exister ele entra no if
            {
                IdentityRole role = new IdentityRole(); //cria uma nova role
                role.Name = "Admin"; //atribui o nome a essa role
                role.NormalizedName = "ADMIN"; // atribui o mesmo nome em caixa alta a role
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result; //cria a role
            }
        }

        public void SeedUser()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait(); 
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

        }
    }
}
