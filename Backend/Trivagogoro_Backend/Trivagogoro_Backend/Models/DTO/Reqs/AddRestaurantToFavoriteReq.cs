using System;
namespace Trivagogoro_Backend.Models.DTO.Reqs
{
    public class AddRestaurantToFavoriteReq
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int Rating { get; set; }
    }
}

