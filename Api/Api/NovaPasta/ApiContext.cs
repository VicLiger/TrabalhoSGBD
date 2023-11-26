using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.NovaPasta
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        {
            
        }

        public DbSet<Filmes> Filmes { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<AnimesFilmes> Animes_Filmes { get; set; }
        public DbSet<AnimesSeries> Animes_Series { get; set; }
    }
}
