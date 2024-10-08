namespace SalesWebMvc.Models.ViewModels
{
    public class VendedorViewModel
    {
        public Vendedor Vendedor { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}
