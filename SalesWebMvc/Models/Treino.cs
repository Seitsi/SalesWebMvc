using SalesWebMvc.Models.Enums;
using System.Drawing;

namespace SalesWebMvc.Models
{
    public class Treino
    {
        public int Id { get; set; }
        public TipoTreino TipoTreino { get; set; }
        public int Repeticoes { get; set; }
        public int Series { get; set; }
        public double Peso { get; set; }
        public DateTime Data { get; set; }

        public Treino() 
        { 
        }

        public Treino(int id, TipoTreino tipoTreino, int repeticoes, int series, double peso, DateTime data)
        {
            Id = id;
            TipoTreino = tipoTreino;
            Repeticoes = repeticoes;
            Series = series;
            Peso = peso;
            Data = data;
        }
    }
}
