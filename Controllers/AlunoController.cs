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
    public class AlunoController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public AlunoController(UniversidadeContext context)
        {
            _context = context;
        }

        private static AlunoDTO AlunoToDTO(Aluno aluno)
        {
            return new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                SiglaCurso = aluno.Curso?.Sigla
            };
        }

        // // GET: api/Universidade/alunos/populate
        // [HttpGet("populate")]
        // public void PopulateAluno()
        // {
        //     _context.Alunos.Add(new Aluno { Nome = "Tiago", Curso = "LES" });
        //     _context.Alunos.Add(new Aluno { Nome = "Diogo", Curso = "LEI" });
        //     _context.Alunos.Add(new Aluno { Nome = "Bernardo", Curso = "LEM" });
        //     _context.Alunos.Add(new Aluno { Nome = "João", Curso = "LEA" });
        //     _context.Alunos.Add(new Aluno { Nome = "Hugo", Curso = "LEQ" });
        //     _context.Alunos.Add(new Aluno { Nome = "Bea", Curso = "LEP" });
        //     _context.SaveChangesAsync();
        // }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAluno()
        {
            if (_context.Alunos == null)
            {
                return NotFound();
            }
            return await _context.Alunos.Select(x => AlunoToDTO(x)).ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(long id)
        {
            if (_context.Alunos == null)
            {
                return NotFound();
            }
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return AlunoToDTO(aluno);
        }

        [HttpGet("{sigla}")]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetCurso(string sigla)
        {
            if (_context.Alunos == null)
            {
                return NotFound();
            }
            return await _context.Alunos.Select(n => AlunoToDTO(n)).Where(n => n.SiglaCurso == sigla).ToListAsync();
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

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.Nome = alunoDTO.Nome;
            aluno.Curso = await _context.Cursos.Where(c => c.Sigla == alunoDTO.SiglaCurso).FirstOrDefaultAsync();
            if (alunoDTO.SiglasUcs != null)
            {
                aluno.UnidadesCurriculares = AddAlunoToUc(await _context.UnidadesCurriculares.Where(u => alunoDTO.SiglasUcs.Contains(u.Sigla)).ToListAsync(), alunoDTO);
            }
            // if (alunoDTO.SiglasUcs != null)
            // {
            //     aluno.UnidadesCurriculares = new List<UnidadeCurricular>();
            //     ICollection<UnidadeCurricular>? ucs = await _context.UnidadesCurriculares.ToListAsync();
            //     if (ucs != null)
            //     {
            //         foreach (UnidadeCurricular uc in ucs)
            //         {
            //             if (uc.Sigla != null)
            //             {
            //                 if (alunoDTO.SiglasUcs.Contains(uc.Sigla))
            //                 {
            //                     aluno.UnidadesCurriculares.Add(uc);
            //                 }
            //             }
            //             else
            //             {
            //                 return NotFound();
            //             }

            //         }
            //     }
            //     else
            //     {
            //         return NotFound();
            //     }
            // }
            // else
            // {
            //     return NotFound();
            // }
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
            if (alunoDTO == null)
            {
                return BadRequest();
            }

            var aluno = new Aluno
            {
                Nome = alunoDTO.Nome,
                Curso = await _context.Cursos.Where(c => c.Sigla == alunoDTO.SiglaCurso).FirstOrDefaultAsync(),
                UnidadesCurriculares = AddAlunoToUc(await _context.UnidadesCurriculares.Where(u => alunoDTO.SiglasUcs.Contains(u.Sigla)).ToListAsync(), alunoDTO)
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAluno),
                new { id = aluno.Id },
                AlunoToDTO(aluno));
        }

        private ICollection<UnidadeCurricular> AddAlunoToUc(ICollection<UnidadeCurricular> ucs, AlunoDTO alunoDTO)
        {
            return ucs.Where(uc => uc.Curso.Sigla == alunoDTO.SiglaCurso).ToList();
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(long id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(long id)
        {
            return (_context.Alunos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
