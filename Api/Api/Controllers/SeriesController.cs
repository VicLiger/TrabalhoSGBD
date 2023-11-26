using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TrabalhoSGBD.Context;
using TrabalhoSGBD.Models;

namespace TrabalhoSGBD.Controllers
{
    public class SeriesController : ControllerBase
    {
        private readonly ApiContext _context;

        public SeriesController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("Series")]
        public async Task<ActionResult<IEnumerable<Series>>> ObterTodosSeries()
        {
            try
            {
                if (_context.Series.Count() == 0)
                {
                    return NotFound("Não existem series na lista");
                }

                var series = await _context.Series.ToListAsync();

                return Ok(series);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }

        [HttpGet("ObterSeriesPor/{id:int}")]
        public async Task<ActionResult<IEnumerable<Series>>> ObterSeriesPorId(int id)
        {
            try
            {
                if (_context.Series.Count() == 0)
                {
                    return NotFound("Não existem series na lista");
                }

                var serie = await _context.Series.FindAsync(id);

                if (serie == null)
                {
                    return NotFound("A serie não foi encontrado");
                }

                return Ok(serie);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação...");
            }
        }

        [HttpPost("AdicionarSeries")]
        public async Task<ActionResult<IEnumerable<Series>>> AdicionarSeries(Series series)
        {
            try
            {
                await _context.Series.AddAsync(series);
                await _context.SaveChangesAsync();
                return Ok(series);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro na solicitação: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("DeletarSeries")]
        public async Task<ActionResult<IEnumerable<Series>>> DeletarSeries(int Id)
        {
            try
            {
                if (_context.Series.Count() == 0)
                {
                    return NotFound("Não existem filmes na lista");
                }

                var serie = await _context.Series.FindAsync(Id);

                if (serie == null)
                {
                    return NotFound("O filme não foi encontrado");
                }

                var deleteSerie = _context.Series.Remove(serie);
                await _context.SaveChangesAsync();

                return Ok(new { Mensagem = "Filme excluído com sucesso", FilmeExcluido = deleteSerie.Entity });
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }


        [HttpPut("AtualizarSeries")]
        public async Task<ActionResult<IEnumerable<Series>>> AtualizarSerie(int Id, Series series)
        {
            try
            {
                if (_context.Series.Count() == 0)
                {
                    return NotFound("A lista está vazia");
                }

                var procurarSerie = await _context.Series.FindAsync(Id);

                procurarSerie.SeriesId = series.SeriesId;
                procurarSerie.Nome = series.Nome;
                procurarSerie.Descricao = series.Descricao;
                procurarSerie.Episodios = series.Episodios;
                procurarSerie.AnoLancamento = series.AnoLancamento;

                _context.Series.Update(procurarSerie);
                await _context.SaveChangesAsync();

                return Ok(series);
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }
        }
    }
}