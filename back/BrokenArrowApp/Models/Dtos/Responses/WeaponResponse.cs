﻿namespace BrokenArrowApp.Models.Dtos.Responses
{
    public class WeaponResponse
    {
        public Guid WeaponId { get; set; }

        public string? Name { get; set; }

        public string? Builder { get; set; }

        public string? WeaponDescription { get; set; }

        public List<BrokenArrowShortResponse>? BrokenArrows { get; set; }
    }
}
