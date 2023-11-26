using Api.Models;
using Api.NovaPasta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoSGBD.Controllers
{
    public class AnimesFilmesController : ControllerBase
    {
        private readonly ApiContext _context;

        public AnimesFilmesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("obter_todos_animes_com_filmes")]
        public async Task<ActionResult<IEnumerable<AnimesFilmes>>> ObterTodosAnimes()
        {
            try
            {
                if (_context.Animes_Filmes.Count() == 0)
                {
                    return NotFound("Não existem animes na lista");
                }

                var animesFilmes = await _context.Animes_Filmes.ToListAsync();

                return Ok(animesFilmes);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }


        [HttpGet("ObterAnimesComoFilmesPor/{id:int}")]
        public async Task<ActionResult<IEnumerable<AnimesFilmes>>> ObterAnimesComoFilmesPorId(int id)
        {
            try
            {
                if (_context.Animes_Filmes.Count() == 0)
                {
                    return NotFound("Não existem animes na lista");
                }

                var animes_filmes = await _context.Animes_Filmes.FindAsync(id);

                if (animes_filmes == null)
                {
                    return NotFound("O anime não foi encontrado");
                }

                return Ok(animes_filmes);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }


        [HttpPost("AdicionarAnimesComoFilmes")]
        public async Task<ActionResult<IEnumerable<AnimesFilmes>>> AdicionarFilmes(AnimesFilmes animesFilmes)
        {
            try
            {
                await _context.Animes_Filmes.AddAsync(animesFilmes);
                await _context.SaveChangesAsync();
                return Ok(animesFilmes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro na solicitação: {ex.InnerException?.Message}");
            }
        }



        [HttpDelete("DeletarAnimesComoFilmes")]
        public async Task<ActionResult<IEnumerable<AnimesFilmes>>> DeletarAnimesComoFilmes(int Id)
        {
            try
            {
                if (_context.Animes_Filmes.Count() == 0)
                {
                    return NotFound("Não existem aniems na lista");
                }

                var animesFilmes = await _context.Animes_Filmes.FindAsync(Id);

                if (animesFilmes == null)
                {
                    return NotFound("O anime não foi encontrado");
                }

                var deleteAnimesFilmes = _context.Animes_Filmes.Remove(animesFilmes);
                _context.SaveChanges();

                return Ok(new { Mensagem = "Anime excluído com sucesso", AnimeExcluido = deleteAnimesFilmes.Entity });

            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }

        [HttpPut("AtualizarAnimesComoFilmes")]
        public async Task<ActionResult<IEnumerable<AnimesFilmes>>> AtualizarFilmes(AnimesFilmes animesFilmes)
        {
            try
            {
                if (_context.Animes_Filmes.Count() == 0)
                {
                    return NotFound("A lista está vazia");
                }

                var procurarAnimeComoFilme = await _context.Animes_Filmes.FindAsync(animesFilmes.AnimesFilmesId);

                procurarAnimeComoFilme.AnimesFilmesId = animesFilmes.AnimesFilmesId;
                procurarAnimeComoFilme.Nome = animesFilmes.Nome;
                procurarAnimeComoFilme.Descricao = animesFilmes.Descricao;
                procurarAnimeComoFilme.Duracao = animesFilmes.Duracao;
                procurarAnimeComoFilme.AnoLancamento = animesFilmes.AnoLancamento;

                _context.Animes_Filmes.Update(procurarAnimeComoFilme);
                await _context.SaveChangesAsync();

                return Ok(animesFilmes);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }
    }
}