using ParkyAPI.Models;
using System.ComponentModel.DataAnnotations;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Dtos
{
    public class TrailDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }

        public enum DifficultyType { Easy, Moderate, Expert }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        public NationalPark NationalPark { get; set; }

    }
}
