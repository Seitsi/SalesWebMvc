using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public LoginController(LoginService loginService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _loginService = loginService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CriarLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarLogin(Login login)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = login.Email,
                    Email = login.Email
                };

                var result = await _userManager.CreateAsync(user, login.PasswordHash);

                if (result.Succeeded)
                {
                    var loginAjustado = _loginService.ValidateCadastroLogin(login);
                    await _loginService.InsertAsync(loginAjustado);
                    TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (ValidateLoginException e)
            {
                TempData["Erro"] = "Falha ao Cadastrar Novo Usuário!";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(Login login)
        {
            try
            {
                var valid = _loginService.ValidateLogin(login);

                // aki autentica
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.PasswordHash, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    TempData["Sucesso"] = "Usuário logado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Erro"] = "Falha na autenticação";
                }
            }
            catch (ValidateLoginException e)
            {
                TempData["Erro"] = e.Message; 
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Sucesso"] = "Você saiu com sucesso!";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }
}
