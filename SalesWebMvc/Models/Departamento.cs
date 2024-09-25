namespace SalesWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();
        public Departamento() 
        { 

        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AdicionaVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }

        public double TotalVendas(DateTime dataInicio, DateTime dataFim)
        {
            //var totalVendas = 0.0;
            //foreach (var item in Vendedores)
            //{
            //    totalVendas += item.TotalVendas(dataInicio, dataFim);
            //}
            //return totalVendas;
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(dataInicio, dataFim));
        }
    }
}
