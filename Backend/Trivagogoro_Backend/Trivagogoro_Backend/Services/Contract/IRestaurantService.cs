using System;
using System.Text.Json;

namespace Trivagogoro_Backend.Services.Contract
{
    public interface IRestaurantService
    {
        ValueTask<int> CrawlAndSaveRestaurantsAsyncWithinTaipei();
        Task<List<string>> SearchRestaurantsImagesAsync(string placeId);
        Task<List<SearchRestaurantDTO>> SearchRestaurantAsync(string keyword);
        Task AddRastaurantToFavoriteAsync(AddRestaurantToFavoriteReq req);
    }
}

