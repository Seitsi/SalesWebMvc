using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;

        public VendedoresController(VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        public IActionResult Index()
        {
            var lista = _vendedorService.FindAll();

            return View(lista);
        }
    }
}
