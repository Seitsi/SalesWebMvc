using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendaService _vendaService;

        public VendasController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BuscaSimples(DateTime? minData, DateTime? maxData)
        {
            if (!minData.HasValue)
            {
                minData = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxData.HasValue)
            {
                maxData = DateTime.Now;
            }

            ViewData["DataMin"] = minData;
            ViewData["DataMax"] = maxData;

            var result = await _vendaService.FindByDateAsync(minData, maxData);

            return View(result);
        }
        public async Task<IActionResult> BuscaAgrupada(DateTime? minData, DateTime? maxData)
        {
            if (!minData.HasValue)
            {
                minData = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxData.HasValue)
            {
                maxData = DateTime.Now;
            }

            ViewData["DataMin"] = minData;
            ViewData["DataMax"] = maxData;

            var result = await _vendaService.FindByDateGroupingAsync(minData, maxData);

            return View(result);
        }
        
    }
}
