using Api.Models;
using Api.NovaPasta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoSGBD.Controllers
{
    public class AnimesSeriesController : ControllerBase
    {
        private readonly ApiContext _context;

        public AnimesSeriesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("AnimesSeries")]
        public async Task<ActionResult<IEnumerable<AnimesSeries>>> ObterTodosAnimesSeries()
        {
            try
            {
                if (_context.Animes_Series.Count() == 0)
                {
                    return NotFound("Não existem series na lista");
                }

                var animesSeries = await _context.Animes_Series.ToListAsync();

                return Ok(animesSeries);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }

        [HttpGet("ObterAnimesComoSeriesPor/{id:int}")]
        public async Task<ActionResult<IEnumerable<AnimesSeries>>> ObterAnimesComoSeriesPorId(int id)
        {
            try
            {
                if (_context.Animes_Series.Count() == 0)
                {
                    return NotFound("Não existem animes na lista");
                }

                var animeSerie = await _context.Animes_Series.FindAsync(id);

                if (animeSerie == null)
                {
                    return NotFound("O anime não foi encontrado");
                }

                return Ok(animeSerie);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }

        [HttpPost("AdicionarAnimesComoSeries")]
        public async Task<ActionResult<IEnumerable<AnimesSeries>>> AdicionarSeries(AnimesSeries animeSerie)
        {
            try
            {
                await _context.Animes_Series.AddAsync(animeSerie);
                await _context.SaveChangesAsync();
                return Ok(animeSerie);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro na solicitação: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("DeletarAnimesComoSerie")]
        public async Task<ActionResult<IEnumerable<AnimesSeries>>> DeletarAnimesComoSeries(int Id)
        {
            try
            {
                if (_context.Animes_Series.Count() == 0)
                {
                    return NotFound("Não existem animes na lista");
                }

                var animeSerie = await _context.Animes_Series.FindAsync(Id);

                if (animeSerie == null)
                {
                    return NotFound("A serie não foi encontrada");
                }

                var deleteAnimeComoSerie = _context.Animes_Series.Remove(animeSerie);
                await _context.SaveChangesAsync();

                return Ok(new { Mensagem = "Anime excluído com sucesso", FilmeExcluido = deleteAnimeComoSerie.Entity });
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }

        [HttpPut("AtualizarAnimesComoSeries")]
        public async Task<ActionResult<IEnumerable<AnimesSeries>>> AtualizarAnimesComoSerie(int Id, AnimesSeries animeSerie)
        {
            try
            {
                if (_context.Animes_Series.Count() == 0)
                {
                    return NotFound("A lista está vazia");
                }

                var procurarAnimeComoSerie = await _context.Animes_Series.FindAsync(Id);

                procurarAnimeComoSerie.AnimesSeriesId = animeSerie.AnimesSeriesId;
                procurarAnimeComoSerie.Nome = animeSerie.Nome;
                procurarAnimeComoSerie.Descricao = animeSerie.Descricao;
                procurarAnimeComoSerie.Episodios = animeSerie.Episodios;
                procurarAnimeComoSerie.AnoLancamento = animeSerie.AnoLancamento;

                _context.Animes_Series.Update(procurarAnimeComoSerie);
                await _context.SaveChangesAsync();

                return Ok(animeSerie);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }
    }
}