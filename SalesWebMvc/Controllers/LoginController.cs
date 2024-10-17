using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CriarLogin()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CriarLogin(Login login)
        {
            bool validLogin = _loginService.ValidateLogin(login);
            return View();
        }
        public IActionResult EsqueciSenha()
        {
            return View();
        }
    }
}
