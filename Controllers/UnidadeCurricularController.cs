using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_Universidade_js_html_css.Models;

namespace Universidade_Api.Controllers
{
    [Route("api/Universidade/[controller]")]
    [ApiController]
    public class UnidadeCurricularController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public UnidadeCurricularController(UniversidadeContext context)
        {
            _context = context;
        }

        private static UnidadeCurricularDTO UnidadeCurricularToDTO(UnidadeCurricular unidadeCurricular)
        {
            return new UnidadeCurricularDTO
            {
                Id = unidadeCurricular.Id,
                Nome = unidadeCurricular.Nome,
                Sigla = unidadeCurricular.Sigla,
                SiglaCurso = unidadeCurricular.Curso
            };
        }
        // GET: api/Universidade/UnidadeCurricular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadeCurricularDTO>>> GetUnidadeCurricular()
        {
            if (_context.UnidadeCurricular == null)
            {
                return NotFound();
            }
            return await _context.UnidadeCurricular.Select(x => UnidadeCurricularToDTO(x)).ToListAsync();
        }

        // GET: api/Universidade/UnidadeCurricular/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadeCurricularDTO>> GetUnidadeCurricular(long id)
        {
            if (_context.UnidadeCurricular == null)
            {
                return NotFound();
            }
            var unidadeCurricular = await _context.UnidadeCurricular.FindAsync(id);

            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            return UnidadeCurricularToDTO(unidadeCurricular);
        }

        // PUT: api/Universidade/UnidadeCurricular/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadeCurricular(long id, UnidadeCurricularDTO unidadeCurricularDTO)
        {
            if (id != unidadeCurricularDTO.Id)
            {
                return BadRequest();
            }

            var unidadeCurricular = await _context.UnidadeCurricular.FindAsync(id);
            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            unidadeCurricular.Nome = unidadeCurricularDTO.Nome;
            unidadeCurricular.Sigla = unidadeCurricularDTO.Sigla;
            unidadeCurricular.Curso = unidadeCurricularDTO.SiglaCurso;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadeCurricularExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Universidade/UnidadeCurricular
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnidadeCurricularDTO>> PostUnidadeCurricular(UnidadeCurricularDTO unidadeCurricularDTO)
        {
            var unidadeCurricular = new UnidadeCurricular
            {
                Nome = unidadeCurricularDTO.Nome,
                Sigla = unidadeCurricularDTO.Sigla,
                Curso = unidadeCurricularDTO.SiglaCurso
            };
            if (_context.UnidadeCurricular == null)
            {
                return Problem("Entity set 'UniversidadeContext.UnidadeCurricular'  is null.");
            }
            _context.UnidadeCurricular.Add(unidadeCurricular);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnidadeCurricular", new { id = unidadeCurricular.Id }, unidadeCurricular);
        }

        // DELETE: api/Universidade/UnidadeCurricular/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadeCurricular(long id)
        {
            if (_context.UnidadeCurricular == null)
            {
                return NotFound();
            }
            var unidadeCurricular = await _context.UnidadeCurricular.FindAsync(id);
            if (unidadeCurricular == null)
            {
                return NotFound();
            }

            _context.UnidadeCurricular.Remove(unidadeCurricular);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnidadeCurricularExists(long id)
        {
            return (_context.UnidadeCurricular?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
