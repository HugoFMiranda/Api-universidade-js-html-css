using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Universidade_Api.Models;
using Api_Universidade_js_html_css.Models;

    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext (DbContextOptions<UniversidadeContext> options)
            : base(options)
        {
        }

        public DbSet<Universidade_Api.Models.Curso> Curso { get; set; } = default!;

        public DbSet<Api_Universidade_js_html_css.Models.Notas>? Notas { get; set; }

        public DbSet<Api_Universidade_js_html_css.Models.UnidadeCurricular>? UnidadeCurricular { get; set; }

        public DbSet<Api_Universidade_js_html_css.Models.UnidadeCurricularDTO>? UnidadeCurricularDTO { get; set; }
    }
