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
        // It creates a bunch of courses, units, students and grades
        [HttpGet("populate")]
        public void PopulateCursos()
        {
            _context.Cursos.Add(new Curso { Sigla = "LES", Nome = "Licenciatura em engenharia de sistemas" });
            _context.Cursos.Add(new Curso { Sigla = "LEI", Nome = "Licenciatura em engenharia informatica" });
            _context.Cursos.Add(new Curso { Sigla = "LEM", Nome = "Licenciatura em engenharia mecanica" });
            _context.Cursos.Add(new Curso { Sigla = "LEA", Nome = "Licenciatura em engenharia aeronautica" });
            _context.Cursos.Add(new Curso { Sigla = "LEQ", Nome = "Licenciatura em engenharia quimica" });
            _context.Cursos.Add(new Curso { Sigla = "LEP", Nome = "Licenciatura em engenharia petrolifera" });
            _context.Cursos.Add(new Curso { Sigla = "MIE", Nome = "Mestrado integrado em engenharia" });
            _context.Cursos.Add(new Curso { Sigla = "MEE", Nome = "Mestrado em engenharia" });
            _context.SaveChangesAsync();
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "SINF2", Nome = "Sistemas Informação", Ano = 3, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LES") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "APROGS", Nome = "Algo. e Prog. LES", Ano = 1, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEI") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "APROGM", Nome = "Algo. e Prog. LEM", Ano = 1, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEM") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "PPROG", Nome = "Parad. Prog.", Ano = 1, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEI") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P5", Nome = "Programação 5", Ano = 5, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEQ") });
            _context.UnidadesCurriculares.Add(new UnidadeCurricular { Sigla = "P6", Nome = "Programação 6", Ano = 6, Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEP") });
            _context.SaveChangesAsync();
            _context.Alunos.Add(new Aluno { Nome = "Berto", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LES") });
            _context.Alunos.Add(new Aluno { Nome = "Ana", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEI") });
            _context.Alunos.Add(new Aluno { Nome = "Carlos", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEM") });
            _context.Alunos.Add(new Aluno { Nome = "Maria", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEA") });
            _context.Alunos.Add(new Aluno { Nome = "Rui", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEQ") });
            _context.Alunos.Add(new Aluno { Nome = "Sara", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "LEP") });
            _context.Alunos.Add(new Aluno { Nome = "Paulo", Curso = _context.Cursos.FirstOrDefault(x => x.Sigla == "MIE") });
            _context.SaveChangesAsync();
            _context.Notas.Add(new Nota { Valor = 12, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "APROGS"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Berto") });
            _context.Notas.Add(new Nota { Valor = 15, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "APROGM"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Carlos") });
            _context.Notas.Add(new Nota { Valor = 11, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "PPROG"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Ana") });
            _context.Notas.Add(new Nota { Valor = 13, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P4"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Maria") });
            _context.Notas.Add(new Nota { Valor = 14, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P5"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Rui") });
            _context.Notas.Add(new Nota { Valor = 15, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P6"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Sara") });
            _context.Notas.Add(new Nota { Valor = 16, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P1"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Paulo") });
            _context.Notas.Add(new Nota { Valor = 16, UnidadeCurricular = _context.UnidadesCurriculares.FirstOrDefault(x => x.Sigla == "P1"), Aluno = _context.Alunos.FirstOrDefault(x => x.Nome == "Paulo") });
            _context.SaveChangesAsync();
        }

        // GET: api/cursos/dispose
        /// Used to dispose of the database context
        [HttpGet("dispose")]
        public void Dispose()
        {
            _context.Dispose();
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
