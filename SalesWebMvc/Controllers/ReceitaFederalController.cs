using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class ReceitaFederalController : Controller
    {
        private readonly ReceitaFederalService _receitaFederalService;
        public ReceitaFederalController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buscar(string cnpj)
        {
            var teste = cnpj;
            return RedirectToAction(nameof(Index));
        }
    }
}
