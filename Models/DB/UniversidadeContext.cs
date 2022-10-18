using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Universidade_Api;

namespace Universidade_Api
{
    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext(DbContextOptions<UniversidadeContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; } = null!;

        public DbSet<Curso> Cursos { get; set; } = null!;

        public DbSet<UnidadeCurricular> UnidadesCurriculares { get; set; } = null!;

        public DbSet<Nota> Notas { get; set; } = null!;
    }

}