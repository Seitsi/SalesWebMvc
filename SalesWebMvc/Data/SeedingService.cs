using Microsoft.CodeAnalysis.CSharp.Syntax;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departamento.Any() 
                || _context.Vendedor.Any()
                || _context.Venda.Any())
                
            {
                return; //Base possui dado.
            }
            
            Departamento d1 = new Departamento(1, "Livros");
            Departamento d2 = new Departamento(2, "Roupas");
            Departamento d3 = new Departamento(3, "Computadores");
            Departamento d4 = new Departamento(4, "Celulares");
            
            Vendedor v1 = new Vendedor(1, "Lucas Araujo", "seitsi2010@gmail.com", new DateTime(2002, 5, 1), 5900, d3);
            Vendedor v2 = new Vendedor(2, "Claudio Nunes", "claudionunes@gmail.com", new DateTime(1968, 12, 27), 3200, d4);
            Vendedor v3 = new Vendedor(3, "Neymar Jr", "njr@gmail.com", new DateTime(1992, 2, 5), 9000, d2);
            Vendedor v4 = new Vendedor(4, "Cristiano Ronaldo", "cr7@gmail.com", new DateTime(1985, 2, 5), 30000, d1);

            Venda vd1 = new Venda(1, DateTime.Now, 5000, StatusVenda.PENDENTE, v1);
            Venda vd2 = new Venda(2, DateTime.Now, 5000, StatusVenda.PAGO, v2);
            Venda vd3 = new Venda(3, DateTime.Now, 5000, StatusVenda.CANCELADO, v3);
            Venda vd4 = new Venda(4, DateTime.Now, 5000, StatusVenda.PAGO, v4);
            Venda vd5 = new Venda(5, DateTime.Now, 5000, StatusVenda.PAGO, v1);
            Venda vd6 = new Venda(6, new DateTime(2024, 5, 3), 5000, StatusVenda.PENDENTE, v2);
            Venda vd7 = new Venda(7, new DateTime(2024, 5, 3), 5000, StatusVenda.PAGO, v3);
            Venda vd8 = new Venda(8, new DateTime(2024, 5, 3), 5000, StatusVenda.CANCELADO, v4);
            Venda vd9 = new Venda(9, new DateTime(2024, 5, 3), 5000, StatusVenda.PAGO, v1);
            Venda vd10 = new Venda(10, new DateTime(2024, 6, 7), 5000, StatusVenda.PAGO, v2);
            Venda vd11 = new Venda(11, new DateTime(2024, 6, 6), 5000, StatusVenda.PAGO, v3);
            Venda vd12 = new Venda(12, new DateTime(2024, 6, 8), 5000, StatusVenda.PAGO, v4);
            Venda vd13 = new Venda(13, new DateTime(2024, 6, 27), 5000, StatusVenda.PAGO, v1);
            Venda vd14 = new Venda(14, new DateTime(2024, 6, 10), 5000, StatusVenda.PENDENTE, v2);
            Venda vd15 = new Venda(15, new DateTime(2024, 8, 10), 5000, StatusVenda.PAGO, v3);
            Venda vd16 = new Venda(16, new DateTime(2024, 8, 13), 5000, StatusVenda.PAGO, v4);
            Venda vd17 = new Venda(17, new DateTime(2024, 8, 17), 5000, StatusVenda.CANCELADO, v1);
            Venda vd18 = new Venda(18, new DateTime(2024, 8, 30), 5000, StatusVenda.PAGO, v2);
            Venda vd19 = new Venda(19, new DateTime(2024, 9, 22), 5000, StatusVenda.PAGO, v3);
            Venda vd20 = new Venda(20, new DateTime(2024, 9, 1), 5000, StatusVenda.PAGO, v4);

            _context.Departamento.AddRange(d1, d2, d3, d4);
            _context.Vendedor.AddRange(v1, v2, v3, v4);
            _context.Venda.AddRange(vd1, vd2, vd3, vd4, vd5, vd6, vd7, vd8, 
                                    vd9, vd10, vd11, vd12, vd13, vd14, 
                                    vd15, vd16, vd17, vd18, vd19, vd20);

            _context.SaveChanges();
        }                              
    }                                  
}
