using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} Obrigatório")]
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Range(100.0, 5000000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
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
