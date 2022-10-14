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
    public class NotasController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public NotasController(UniversidadeContext context)
        {
            _context = context;
        }
        private static NotasDTO NotasToDTO(Notas notas)
        {
            return new NotasDTO
            {
                Id = notas.Id,
                Valor = notas.Valor,
                SiglaUnidadeCurricular = notas.UnidadeCurricular
            };
        }

        // GET: api/Universidade/Notas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotasDTO>>> GetNotas()
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            return await _context.Notas.Select(x => NotasToDTO(x)).ToListAsync();
        }

        // GET: api/Universidade/Notas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotasDTO>> GetNotas(long id)
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            var notas = await _context.Notas.FindAsync(id);

            if (notas == null)
            {
                return NotFound();
            }

            return NotasToDTO(notas);
        }

        // PUT: api/Universidade/Notas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotas(long id, NotasDTO notasDTO)
        {
            if (id != notasDTO.Id)
            {
                return BadRequest();
            }

            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }

            notas.Valor = notasDTO.Valor;
            notas.UnidadeCurricular = notasDTO.SiglaUnidadeCurricular;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasExists(id))
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

        // POST: api/Universidade/Notas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotasDTO>> PostNotas(NotasDTO notasDTO)
        {
            var notas = new Notas
            {
                Valor = notasDTO.Valor,
                UnidadeCurricular = notasDTO.SiglaUnidadeCurricular
            };

            if (_context.Notas == null)
            {
                return Problem("Entity set 'UniversidadeContext.Notas'  is null.");
            }
            _context.Notas.Add(notas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotas", new { id = notas.Id }, notas);
        }

        // DELETE: api/Universidade/Notas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotas(long id)
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(notas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotasExists(long id)
        {
            return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
