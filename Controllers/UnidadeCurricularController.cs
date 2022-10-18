using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade_Api;

namespace Universidade_Api.Controllers
{
    [Route("api/Ucs")]
    [ApiController]
    public class UnidadeCurricularController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public UnidadeCurricularController(UniversidadeContext context)
        {
            _context = context;
        }

        private static UnidadeCurricularDTO UcToDTO(UnidadeCurricular unidadeCurricular)
        {
            return new UnidadeCurricularDTO
            {
                Id = unidadeCurricular.Id,
                Nome = unidadeCurricular.Nome,
                Sigla = unidadeCurricular.Sigla,
                SiglaCurso = unidadeCurricular.Curso?.Sigla,
                Ano = unidadeCurricular.Ano
            };
        }

        // GET: api/UnidadeCurricular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadeCurricularDTO>>> GetUnidadeCurricular()
        {
            if (_context.UnidadesCurriculares == null)
            {
                return NotFound();
            }
            return await _context.UnidadesCurriculares.Select(x => UcToDTO(x)).ToListAsync();
        }

        // GET: api/UnidadeCurricular/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UnidadeCurricularDTO>> GetUnidadeCurricular(long id)
        {
            if (_context.UnidadesCurriculares == null)
            {
                return NotFound();
            }
            var unidadeCurricular = await _context.UnidadesCurriculares.FindAsync(id);

            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            return UcToDTO(unidadeCurricular);
        }

        [HttpGet("{sigla}")]
        public async Task<ActionResult<UnidadeCurricularDTO>> GetCurso(string sigla)
        {
            if (_context.UnidadesCurriculares == null)
            {
                return NotFound();
            }
            var uc = await _context.UnidadesCurriculares.Where(uc => uc.Sigla == sigla).FirstOrDefaultAsync();

            if (uc == null)
            {
                return NotFound();
            }

            return UcToDTO(uc);
        }

        // PUT: api/UnidadeCurricular/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadeCurricular(long id, UnidadeCurricularDTO unidadeCurricularDTO)
        {
            if (id != unidadeCurricularDTO.Id)
            {
                return BadRequest();
            }

            var uc = await _context.UnidadesCurriculares.FindAsync(id);
            if (uc == null)
            {
                return NotFound();
            }

            uc.Nome = unidadeCurricularDTO.Nome;
            uc.Sigla = unidadeCurricularDTO.Sigla;
            uc.Curso = await _context.Cursos.Where(c => c.Sigla == unidadeCurricularDTO.SiglaCurso).FirstOrDefaultAsync();
            uc.Ano = unidadeCurricularDTO.Ano;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UnidadeCurricularExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/UnidadeCurricular
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnidadeCurricular>> PostUnidadeCurricular(UnidadeCurricularDTO unidadeCurricularDTO)
        {
            var uc = new UnidadeCurricular
            {
                Nome = unidadeCurricularDTO.Nome,
                Sigla = unidadeCurricularDTO.Sigla,
                Curso = await _context.Cursos.Where(c => c.Sigla == unidadeCurricularDTO.SiglaCurso).FirstOrDefaultAsync(),
                Ano = unidadeCurricularDTO.Ano
            };

            _context.UnidadesCurriculares.Add(uc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUnidadeCurricular),
                new { id = uc.Id },
                UcToDTO(uc));
        }

        // DELETE: api/UnidadeCurricular/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadeCurricular(long id)
        {
            if (_context.UnidadesCurriculares == null)
            {
                return NotFound();
            }
            var unidadeCurricular = await _context.UnidadesCurriculares.FindAsync(id);
            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            _context.UnidadesCurriculares.Remove(unidadeCurricular);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnidadeCurricularExists(long id)
        {
            return (_context.UnidadesCurriculares?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
