using Api_Universidade_js_html_css.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Universidade_Api.Models
{
    public class AlunoContext : DbContext
    {
        public AlunoContext(DbContextOptions<AlunoContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Aluno { get; set; } = null!;
    }
    public class CursoContext : DbContext
    {
        public CursoContext(DbContextOptions<CursoContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Curso { get; set; } = null!;
    }

    public class UnidadeCurricularContext : DbContext
    {
        public UnidadeCurricularContext(DbContextOptions<UnidadeCurricularContext> options)
            : base(options)
        {
        }

        public DbSet<UnidadeCurricular> UnidadeCurricular { get; set; } = null!;
    }

    public class NotasContext : DbContext
    {
        public NotasContext(DbContextOptions<NotasContext> options)
            : base(options)
        {
        }

        public DbSet<Notas> Notas { get; set; } = null!;
    }


}