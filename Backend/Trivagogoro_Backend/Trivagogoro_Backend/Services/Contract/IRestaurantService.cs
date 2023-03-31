using System;
using System.Text.Json;

namespace Trivagogoro_Backend.Services.Contract
{
    public interface IRestaurantService
    {
        ValueTask<int> CrawlAndSaveRestaurantsAsyncWithinTaipei();
    }
}

