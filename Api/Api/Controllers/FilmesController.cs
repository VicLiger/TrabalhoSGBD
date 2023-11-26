using Api.Models;
using Api.NovaPasta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoSGBD.Controllers
{
    public class FilmesController : ControllerBase
    {
        private readonly ApiContext _context;

        public FilmesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("ObterTodosFilmes")]
        public async Task<ActionResult<IEnumerable<Filmes>>> ObterTodosFilmes()
        {
            try
            {
                if (_context.Filmes.Count() == 0)
                {
                    return NotFound("Não existem filmes na lista");
                }

                var filmes = await _context.Filmes.ToListAsync();

                return Ok(filmes);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }


        [HttpGet("ObterFilmesPor/{id:int}")]
        public async Task<ActionResult<IEnumerable<Filmes>>> ObterFilmePorId(int id)
        {
            try
            {
                if (_context.Filmes.Count() == 0)
                {
                    return NotFound("Não existem filmes na lista");
                }

                var filme = await _context.Filmes.FindAsync(id);

                if (filme == null)
                {
                    return NotFound("O filme não foi encontrado");
                }

                return Ok(filme);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }


        [HttpPost("AdicionarFilmes")]
        public async Task<ActionResult<IEnumerable<Filmes>>> AdicionarFilmes(Filmes filmes)
        {
            try
            {
                await _context.Filmes.AddAsync(filmes);
                await _context.SaveChangesAsync();
                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro na solicitação: {ex.InnerException?.Message}");
            }
        }



        [HttpDelete("DeletarFilmes")]
        public async Task<ActionResult<IEnumerable<Filmes>>> DeletarFilmes(int Id)
        {
            try
            {
                if (_context.Filmes.Count() == 0)
                {
                    return NotFound("Não existem filmes na lista");
                }

                var filme = await _context.Filmes.FindAsync(Id);

                if (filme == null)
                {
                    return NotFound("O filme não foi encontrado");
                }

                var deleteFilme = _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();

                return Ok(new { Mensagem = "Filme excluído com sucesso", FilmeExcluido = deleteFilme.Entity });
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }


        [HttpPut("AtualizarFilmes")]
        public async Task<ActionResult<IEnumerable<Filmes>>> AtualizarFilmes(int Id, Filmes filmes)
        {
            try
            {
                if (_context.Filmes.Count() == 0)
                {
                    return NotFound("A lista está vazia");
                }

                var procurarFilme = await _context.Filmes.FindAsync(Id);

                procurarFilme.FilmesId = filmes.FilmesId;
                procurarFilme.Nome = filmes.Nome;
                procurarFilme.Descricao = filmes.Descricao;
                procurarFilme.Duracao = filmes.Duracao;
                procurarFilme.AnoLancamento = filmes.AnoLancamento;

                _context.Filmes.Update(procurarFilme);
                await _context.SaveChangesAsync();

                return Ok(filmes);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }

    }
}