using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models.ViewModels
{
    public class VendaViewModel
    {
        public Venda Venda { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; }
        public List<StatusVenda> Status { get; set; }
    }
}
