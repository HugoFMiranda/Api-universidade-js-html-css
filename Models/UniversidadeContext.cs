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
}