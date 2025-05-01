using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class TreinoController : Controller
    {
        private readonly TreinoService _treinoService;

        public TreinoController(TreinoService treinoService)
        {
            _treinoService = treinoService;
        }
        public async Task<IActionResult> Index()
        {
            var listaTreino = await _treinoService.FindAllAsync();
            return View(listaTreino);
        }
    }
}
