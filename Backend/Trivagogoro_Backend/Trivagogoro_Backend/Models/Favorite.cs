using System;
namespace Trivagogoro_Backend.Models
{
    public class Favorite
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int restaurantId { get; set; }
        public int selfRating { get; set; }
    }
}

