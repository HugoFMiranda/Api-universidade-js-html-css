using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Universidade_Api.Models;

    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext (DbContextOptions<UniversidadeContext> options)
            : base(options)
        {
        }

        public DbSet<Universidade_Api.Models.Curso> Curso { get; set; } = default!;
    }
