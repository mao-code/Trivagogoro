using System;
namespace Trivagogoro_Backend.Models.DTO.Reses
{
    public class GetAllFavoriteRestaurantRes
    {
        public List<GetAllFavoriteRestaurantDTO> FavoriteRestaurants { get; set; } = new List<GetAllFavoriteRestaurantDTO>();
    }
}

