using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class VendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BuscaSimples()
        {
            return View();
        }
        public IActionResult BuscaAgrupada()
        {
            return View();
        }
    }
}
