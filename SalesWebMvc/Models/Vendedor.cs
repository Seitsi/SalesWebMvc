using System.Linq;
namespace SalesWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
        public Vendedor() 
        { 
        
        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
        }

        public void AdicionaVendas(Venda venda)
        {
            Vendas.Add(venda);
        }        
        public void RemoveVendas(Venda venda)
        {
            Vendas.Remove(venda);
        }

        public double TotalVendas(DateTime dataInicio, DateTime dataFim)
        {
            return Vendas.Where(venda => venda.Data >= dataInicio && venda.Data <= dataFim).Sum(venda => venda.Valor);
        }
    }
}
