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
    public class CursoController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public CursoController(UniversidadeContext context)
        {
            _context = context;
        }

        // GET: api/cursos/populate
        [HttpGet("populate")]
        public void PopulateCursos()
        {
            _context.Cursos.Add(new Curso { Sigla = "LES", Nome = "Licenciatura em engenharia de sistemas" });
            _context.Cursos.Add(new Curso { Sigla = "LEI", Nome = "Licenciatura em engenharia informatica" });
            _context.Cursos.Add(new Curso { Sigla = "LEM", Nome = "Licenciatura em engenharia mecanica" });
            _context.Cursos.Add(new Curso { Sigla = "LEA", Nome = "Licenciatura em engenharia aeronautica" });
            _context.Cursos.Add(new Curso { Sigla = "LEQ", Nome = "Licenciatura em engenharia quimica" });
            _context.Cursos.Add(new Curso { Sigla = "LEP", Nome = "Licenciatura em engenharia petrolifera" });
            _context.SaveChangesAsync();
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P1", Nome = "Programação 1", Ano = 1, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LES") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P2", Nome = "Programação 2", Ano = 2, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEI") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P3", Nome = "Programação 3", Ano = 3, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEM") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P4", Nome = "Programação 4", Ano = 4, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEA") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P5", Nome = "Programação 5", Ano = 5, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEQ") });
            _context.SaveChangesAsync();
            _context.Alunos.Add(new Aluno { Nome = "João", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LES") });
            _context.Alunos.Add(new Aluno { Nome = "Maria", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEI") });
            _context.Alunos.Add(new Aluno { Nome = "Pedro", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEM") });
            _context.Alunos.Add(new Aluno { Nome = "Ana", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEA") });
            _context.Alunos.Add(new Aluno { Nome = "Rui", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEQ") });
            _context.SaveChangesAsync();
            _context.Notas.Add(new Nota { Valor = 10, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P1"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "João") });
            _context.Notas.Add(new Nota { Valor = 11, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P2"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Maria") });
            _context.Notas.Add(new Nota { Valor = 12, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P3"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Pedro") });
            _context.Notas.Add(new Nota { Valor = 13, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P4"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Ana") });
            _context.Notas.Add(new Nota { Valor = 14, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P5"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Rui") });
            _context.SaveChangesAsync();
        }

        private static string getSigla(String curso)
        {
            string text = curso;
            string firstLetters = "";

            foreach (var part in text.Split(' '))
            {
                firstLetters += part.Substring(-2, 1);
            }
            return firstLetters;
        }

        // GET: api/cursos/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {

            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/cursos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Curso>> GetCurso(long id)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound("Não existe curso com esse id");
            }

            return curso;
        }

        // GET: api/cursos/LES
        [HttpGet("{sigla}")]
        public async Task<ActionResult<Curso>> GetCurso(string sigla)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.Where(c => c.Sigla == sigla).FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound("Não existe curso com essa sigla");
            }

            return curso;
        }


        // PUT: api/cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(long id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest("Id do curso não corresponde ao id do curso a alterar");
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
                    return NotFound("Não existe curso com esse id");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/cursos/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            if (_context.Cursos == null)
            {
                return Problem("Entity set 'UniversidadeContext.Cursos'  is null.");
            }
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // DELETE: api/cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound("Não existe curso com esse id");
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(long id)
        {
            return (_context.Cursos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
