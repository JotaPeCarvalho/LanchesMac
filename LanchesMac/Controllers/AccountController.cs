using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid) //Se o ModelState não for valido, volta pra pagina de login
                return View(loginVm);

            var user = await _userManager.FindByNameAsync(loginVm.UserName); //Procura o usuario pelo nome

            if (user != null) //Se achar o usuario, entra no if
            {
                //no result valida a senha do usuario
                var result = await _singInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                if (result.Succeeded) //se der certo entra no if
                {
                    if (string.IsNullOrEmpty(loginVm.ReturnUrl)) //se o returnUrl vor nulo ele manda pra home logado
                    {
                        await _userManager.AddToRoleAsync(user, "Member");
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVm.ReturnUrl); //se não anda pra onde ele queria ir
                }

            }
            ModelState.AddModelError("", "Falha ao realizar o login!"); //se não achar o usuario, retorna esse erro
            return View(loginVm);

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Para evitar ataque (utiliza tokens de validação)
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid) //se a model state for valida, entra no if
            {
                //cria um nome de usuario (IdentityUser)
                var user = new IdentityUser { UserName = registroVM.UserName };
                //cria uma senha de usuario 
                var result = await _userManager.CreateAsync(user, registroVM.Password); ;

                if (result.Succeeded) // se der certo, entra no if
                {
                    RedirectToAction("Login", "Account");
                }
                else //se não, exibe erro
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar usuário");
                }
            }
            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear(); //limpa a sessão
            HttpContext.User = null; //define como nulo o usuário
            await _singInManager.SignOutAsync(); //usa o manager pra sair
            return RedirectToAction("Index", "Home"); //redireciona para a home

        }
    }
}
