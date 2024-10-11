using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _vendedorService.FindAllAsync();

            return View(lista);
        }

        public async Task<IActionResult> Criar()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorViewModel { Departamentos = departamentos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Vendedor vendedor)
        {
            ModelState.Remove("Vendedor.Departamento");
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorViewModel { Departamentos = departamentos, Vendedor = vendedor };
                return View(viewModel);
            }

            await _vendedorService.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido!" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado!" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vendedorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido!" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado!" });
            }
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não fornecido!" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado!" });
            }
            var departamentos = await _departamentoService.FindAllAsync();

            VendedorViewModel viewModel = new VendedorViewModel { Vendedor = obj, Departamentos = departamentos};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();

                var viewModel = new VendedorViewModel { Departamentos= departamentos, Vendedor = vendedor};

                return View(viewModel);
            }

            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não corresponde!" });
            }
            try
            {
                await _vendedorService.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
