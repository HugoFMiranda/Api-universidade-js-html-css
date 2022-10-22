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

        /// It takes a Nota object and returns a NotaDTO
        private static NotaDTO NotaToDTO(Nota nota)
        {
            return new NotaDTO
            {
                Id = nota.Id,
                Valor = nota.Valor,
                SiglaUc = nota.UnidadeCurricular?.Sigla,
                NomeAluno = nota.Aluno?.Nome
            };
        }

        // GET: api/Nota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetNotas()
        {
            if (_context.Notas == null)
            {
                return NotFound("Notas not found");
            }
            return await _context.Notas.Include(x => x.Aluno).Include(x => x.UnidadeCurricular).Select(x => NotaToDTO(x)).ToListAsync();
        }

        // GET: api/Nota/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotaDTO>> GetNota(long id)
        {
            if (_context.Notas == null)
            {
                return NotFound("Notas is null");
            }
            var nota = await _context.Notas.Include(x => x.Aluno).Include(x => x.UnidadeCurricular).FirstOrDefaultAsync(x => x.Id == id);

            if (nota == null)
            {
                return NotFound("Nota não encontrada");
            }

            return NotaToDTO(nota);
        }
        // GET: api/Nota/SIGLA
        [HttpGet("{sigla}")]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetCurso(string sigla)
        {
            if (_context.Notas == null)
            {
                return NotFound("Notas is null");
            }

            return await _context.Notas.Include(x => x.Aluno).Include(x => x.UnidadeCurricular).Where(x => x.UnidadeCurricular != null && x.UnidadeCurricular.Sigla == sigla).Select(x => NotaToDTO(x)).ToListAsync();
        }

        // PUT: api/Nota/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNota(long id, NotaDTO notaDTO)
        {
            if (id != notaDTO.Id)
            {
                return BadRequest("Id da nota não corresponde");
            }

            var nota = await _context.Notas.Include(x => x.Aluno).Include(x => x.UnidadeCurricular).FirstOrDefaultAsync(x => x.Id == id);
            if (nota == null)
            {
                return NotFound("Nota não encontrada");
            }

            nota.Valor = notaDTO.Valor;
            nota.UnidadeCurricular = await _context.UnidadesCurriculares.Where(c => c.Sigla == notaDTO.SiglaUc).FirstOrDefaultAsync();
            nota.Aluno = await _context.Alunos.Where(a => a.Nome == notaDTO.NomeAluno).FirstOrDefaultAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaExists(id))
                {
                    return NotFound("Nota não encontrada");
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


            UnidadeCurricular? unidadeCurricular = await _context.UnidadesCurriculares.Where(uc => uc.Sigla == notaDTO.SiglaUc).FirstOrDefaultAsync();
            Aluno? aluno = await _context.Alunos.Where(a => a.Nome == notaDTO.NomeAluno).FirstOrDefaultAsync();

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado");
            }
            if (unidadeCurricular == null)
            {
                return BadRequest("Unidade curricular não encontrada");
            }
            if (aluno?.UnidadesCurriculares?.Contains(unidadeCurricular) == false)
            {
                return BadRequest("Aluno não está inscrito na unidade curricular");
            }
            if (NotaIsValid(notaDTO) == false)
            {
                return BadRequest("Nota inválida");
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
                return NotFound("Notas is null");
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

        /// If the context is null, return false. Otherwise, return the result of the Any() function
        private bool NotaExists(long id)
        {
            return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// It checks if the value of the note is between 0 and 20
        private bool NotaIsValid(NotaDTO notaDTO)
        {
            return notaDTO.Valor >= 0 && notaDTO.Valor <= 20;
        }
    }
}
