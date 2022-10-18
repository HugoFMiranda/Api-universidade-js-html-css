namespace Universidade_Api
{
    public class Nota
    {
        public long Id { get; set; }

        public Double Valor { get; set; }

        public UnidadeCurricular? UnidadeCurricular { get; set; }

        public Aluno? Aluno { get; set; }
    }
}