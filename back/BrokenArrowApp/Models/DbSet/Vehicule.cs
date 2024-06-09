﻿using System.ComponentModel.DataAnnotations;

namespace BrokenArrowApp.Models.Entities
{
    public class Vehicule
    {

        [Key]
        public Guid VehiculeId { get; set; }

        public string? Type { get; set; }

        public string? Builder { get; set; }

        public string? Name { set; get; }

        public string? VehiculeDescription { set; get; }

        public List<BrokenArrow>? BrokenArrows { get; set; }

    }
}