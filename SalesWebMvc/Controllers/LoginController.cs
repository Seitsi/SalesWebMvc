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

        public LoginController(LoginService loginService, SignInManager<IdentityUser> signInManager)
        {
            _loginService = loginService;
            _signInManager = signInManager;
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
            var loginAjustado = _loginService.ValidateCadastroLogin(login);
            if (login.Ativo == false)
            {
                return View(login);
            }

            await _loginService.InsertAsync(loginAjustado);
            TempData["Sucesso"] = "Usuário cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar(Login login)
        {
            try
            {
                var valid = _loginService.ValidateLogin(login);

                // Autenticar o usuário
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.PasswordHash, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    TempData["Sucesso"] = "Usuário logado com sucesso!";
                    return RedirectToAction(nameof(Index));
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
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }
}
