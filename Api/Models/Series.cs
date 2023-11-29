using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Series
    {
        [Key]
        public int SeriesId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [StringLength(2000)]
        public string? Descricao { get; set; }

        [Required]
        public int Episodios { get; set; }

        [Required]
        public int AnoLancamento { get; set; }
    }
}
