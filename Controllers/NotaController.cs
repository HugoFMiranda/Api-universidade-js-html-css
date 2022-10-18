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
    [Route("api/[controller]s")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public NotaController(UniversidadeContext context)
        {
            _context = context;
        }

        private static NotaDTO NotaToDTO(Nota nota)
        {
            return new NotaDTO
            {
                Id = nota.Id,
                Valor = nota.Valor,
                SiglaUnidadeCurricular = nota.UnidadeCurricular?.Sigla,
                NomeAluno = nota.Aluno?.Nome
            };
        }

        // GET: api/Nota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetNotas()
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            return await _context.Notas.Select(x => NotaToDTO(x)).ToListAsync();
        }

        // GET: api/Nota/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotaDTO>> GetNota(long id)
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
            {
                return NotFound();
            }

            return NotaToDTO(nota);
        }

        [HttpGet("{sigla}")]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetCurso(string sigla)
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            return await _context.Notas.Select(n => NotaToDTO(n)).Where(n => n.SiglaUnidadeCurricular == sigla).ToListAsync();

        }

        // PUT: api/Nota/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNota(long id, NotaDTO notaDTO)
        {
            if (id != notaDTO.Id)
            {
                return BadRequest();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }

            nota.Valor = notaDTO.Valor;
            nota.UnidadeCurricular = await _context.UnidadesCurriculares.Where(c => c.Sigla == notaDTO.SiglaUnidadeCurricular).FirstOrDefaultAsync();
            nota.Aluno = await _context.Alunos.Where(a => a.Nome == notaDTO.NomeAluno).FirstOrDefaultAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(id))
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

        // POST: api/Nota
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotaDTO>> PostNota(NotaDTO notaDTO)
        {
            if (_context.Alunos == null || _context.UnidadesCurriculares == null)
            {
                return BadRequest();
            }

            Aluno? aluno = await _context.Alunos.Where(a => a.Nome == notaDTO.NomeAluno).FirstOrDefaultAsync();
            UnidadeCurricular? unidadeCurricular = await _context.UnidadesCurriculares.Where(c => c.Sigla == notaDTO.SiglaUnidadeCurricular).FirstOrDefaultAsync();
            
            if (aluno == null || unidadeCurricular == null)
            {
                return BadRequest();
            }
            if (aluno?.UnidadesCurriculares?.Contains(unidadeCurricular) == false)
            {
                return BadRequest();
            }

            var nota = new Nota
            {
                Valor = notaDTO.Valor,
                UnidadeCurricular = unidadeCurricular,
                Aluno = aluno
            };

            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetNota),
                new { id = nota.Id },
                NotaToDTO(nota));
        }

        // DELETE: api/Nota/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNota(long id)
        {
            if (_context.Notas == null)
            {
                return NotFound();
            }
            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotaExists(long id)
        {
            return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
