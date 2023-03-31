using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class RestaurantDTO
    {
        public int price_level { get; set; }
        public double rating { get; set; }
        public string place_id { get; set; } = "";
        public string name { get; set; } = "";
        public string vicinity { get; set; } = "";
    }
}

