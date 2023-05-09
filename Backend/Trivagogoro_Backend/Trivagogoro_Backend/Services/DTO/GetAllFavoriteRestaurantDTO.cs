using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class GetAllFavoriteRestaurantDTO
    {
        public int favId { get; set; }
        public int selfRating { get; set; }
        public int resId { get; set; }
        public string resName { get; set; } = "";
        public string placeId { get; set; } = "";

        public List<string> images { get; set; } = new List<string>();
    }
}

