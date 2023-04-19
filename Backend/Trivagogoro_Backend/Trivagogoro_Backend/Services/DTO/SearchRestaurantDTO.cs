using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class SearchRestaurantDTO
    {
        public int? id { get; set; }
        public string name { get; set; } = "";
        public double lat { get; set; }
        public double lng { get; set; }
        public string address { get; set; } = "";
        public string placeId { get; set; } = "";
        public int priceLevel { get; set; }
        public List<string> images { get; set; } = new List<string>(); // from google map api
    }
}

