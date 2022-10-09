using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade_Api.Models;

namespace Universidade_Api.Controllers
{
    [Route("api/[controller]/cursos")]
    [ApiController]
    public class UniversidadeController : ControllerBase
    {
        private readonly CursoContext _context;

        public UniversidadeController(CursoContext context)
        {
            _context = context;
        }
        [HttpGet("/")]
        public void PopulateCursos()
        {
            _context.Curso.Add(new Curso { Sigla = "LES", Nome = "Licenciatura em engenharia de sistemas" });
            _context.Curso.Add(new Curso { Sigla = "LEI", Nome = "Licenciatura em engenharia informatica" });
            _context.Curso.Add(new Curso { Sigla = "LEM", Nome = "Licenciatura em engenharia mecanica" });
            _context.Curso.Add(new Curso { Sigla = "LEA", Nome = "Licenciatura em engenharia aeronautica" });
            _context.Curso.Add(new Curso { Sigla = "LEQ", Nome = "Licenciatura em engenharia quimica" });
            _context.Curso.Add(new Curso { Sigla = "LEP", Nome = "Licenciatura em engenharia petrolifera" });
            _context.SaveChangesAsync();
        }

        // GET: api/Universidade/cursos/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {

            if (_context.Curso == null)
            {
                return NotFound();
            }
            return await _context.Curso.ToListAsync();
        }

        // GET: api/Universidade/cursos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Curso>> GetCurso(long id)
        {
            if (_context.Curso == null)
            {
                return NotFound();
            }
            var curso = await _context.Curso.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // GET: api/Universidade/cursos/LES
        [HttpGet("{sigla}")]
        public async Task<ActionResult<Curso>> GetCurso(string sigla)
        {
            if (_context.Curso == null)
            {
                return NotFound();
            }
            var curso = await _context.Curso.Where(c => c.Sigla == sigla).FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }


        // PUT: api/Universidade/cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(long id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Universidade/cursos/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            if (_context.Curso == null)
            {
                return Problem("Entity set 'UniversidadeContext.Curso'  is null.");
            }
            _context.Curso.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // DELETE: api/Universidade/cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            if (_context.Curso == null)
            {
                return NotFound();
            }
            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(long id)
        {
            return (_context.Curso?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
