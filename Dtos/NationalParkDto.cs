﻿using System.ComponentModel.DataAnnotations;

namespace ParkyAPI.Dtos
{
    public class NationalParkDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }

        public byte[]? Image { get; set; }

        public DateTime Created { get; set; }

        public DateTime Established { get; set; }
    }
}
