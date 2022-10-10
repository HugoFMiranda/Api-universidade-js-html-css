using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Universidade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade_Api.Models;

namespace Universidade_Api.Controllers
{
    [Route("api/universidade/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoContext _context;

        public AlunoController(AlunoContext context)
        {
            _context = context;
        }

        private static AlunoDTO AlunoToDTO(Aluno aluno)
        {
            return new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                SiglaCurso = aluno.Curso
            };
        }

        private static string getSigla(String curso)
        {
            string text = curso;
            string firstLetters = "";

            foreach (var part in text.Split(' '))
            {
                firstLetters += part.Substring(0, 1);
            }
            return firstLetters;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAluno()
        {
            if (_context.Aluno == null)
            {
                return NotFound();
            }
            return await _context.Aluno.Select(x => AlunoToDTO(x)).ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(long id)
        {
            if (_context.Aluno == null)
            {
                return NotFound();
            }
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return AlunoToDTO(aluno);
        }

        // PUT: api/Aluno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(long id, AlunoDTO alunoDTO)
        {
            if (id != alunoDTO.Id)
            {
                return BadRequest();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.Nome = alunoDTO.Nome;
            aluno.Curso = alunoDTO.SiglaCurso;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AlunoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> PostAluno(AlunoDTO alunoDTO)
        {
            var aluno = new Aluno
            {
                Nome = alunoDTO.Nome,
                Curso = alunoDTO.SiglaCurso
            };

            _context.Aluno.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAluno),
                new { id = aluno.Id },
                AlunoToDTO(aluno));
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(long id)
        {
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(long id)
        {
            return (_context.Aluno?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
