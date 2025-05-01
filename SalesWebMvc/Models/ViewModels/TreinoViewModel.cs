using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Models.ViewModels
{
    public class TreinoViewModel
    {
        public class VendaViewModel
        {
            public Treino Treino { get; set; }
            public List<TipoTreino> TipoTreino { get; set; }
        }
    }
}
