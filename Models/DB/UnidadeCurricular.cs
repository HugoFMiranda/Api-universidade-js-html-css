namespace Universidade_Api
{
    public class UnidadeCurricular
    {
        public long Id { get; set; }

        public string? Sigla { get; set; }

        public string? Nome { get; set; }

        public Curso? Curso { get; set; }

        public int? Ano { get; set; }

    }
}