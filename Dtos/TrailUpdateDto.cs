﻿using System.ComponentModel.DataAnnotations;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Dtos
{
    public class TrailUpdateDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }


        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
    }
}
