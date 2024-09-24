using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> list = new List<Departamento>();
            list.Add(new Departamento { Id = 1, Nome = "Eletrônico" });
            list.Add(new Departamento { Id = 2, Nome = "Moda" });
            return View(list);
        }
    }
}
