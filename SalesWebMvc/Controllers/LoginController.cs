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
                TempData["Sucesso"] = "Usuário logado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (ValidateLoginException e)
            {
                TempData["Erro"] = e.Message; // Armazenar a mensagem de erro
            }

            return View(login); // Retornar à view com o modelo de login
        }
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }
}
