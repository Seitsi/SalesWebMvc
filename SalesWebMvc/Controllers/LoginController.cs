using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
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
            var loginAjustado = _loginService.ValidateLogin(login);
            if (login.Ativo == false)
            {
                return View(login);
            }
            await _loginService.InsertAsync(loginAjustado);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }
}
