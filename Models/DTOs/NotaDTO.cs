namespace Universidade_Api
{
    public class NotaDTO
    {
        public long Id { get; set; }

        public Double Valor { get; set; }

        public string? SiglaUnidadeCurricular { get; set; }

        public string? NomeAluno { get; set; }
    }
}