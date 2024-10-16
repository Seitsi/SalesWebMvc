using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class VendasController : Controller
    {
        private readonly VendaService _vendaService;
        private readonly VendedorService _vendedorService;

        public VendasController(VendaService vendaService, VendedorService vendedorService)
        {
            _vendaService = vendaService;
            _vendedorService = vendedorService;
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

        public async Task<IActionResult> CadastroVenda()
        {
            var vendedores = await _vendedorService.FindAllAsync();
            var statusVendas = Enum.GetValues(typeof(StatusVenda)).Cast<StatusVenda>().ToList();

            var viewModel = new VendaViewModel { Vendedores = vendedores, Status = statusVendas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroVenda(Venda venda)
        {
            ModelState.Remove("Venda.Vendedor");
            if (!ModelState.IsValid)
            {
                var vendedores = await _vendedorService.FindAllAsync();
                var statusVendas = Enum.GetValues(typeof(StatusVenda)).Cast<StatusVenda>().ToList();

                var viewModel = new VendaViewModel { Vendedores = vendedores, Status = statusVendas };
                return View(viewModel);
            }

            await _vendaService.InsertAsync(venda);
            TempData["Sucesso"] = "Venda cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

    }
}
