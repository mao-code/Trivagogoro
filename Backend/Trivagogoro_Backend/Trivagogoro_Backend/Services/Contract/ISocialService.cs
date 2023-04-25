using System;
namespace Trivagogoro_Backend.Services.Contract
{
    public interface ISocialService
    {
        Task PostingRestaurantPostAsync(PostRestaurantReq req);
    }
}

