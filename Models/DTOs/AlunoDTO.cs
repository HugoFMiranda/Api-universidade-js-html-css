namespace Universidade_Api
{
    public class AlunoDTO
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? SiglaCurso { get; set; }
        public ICollection<string?>? SiglasUcs { get; set; }
    }
}
