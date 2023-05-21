using System;
namespace Trivagogoro_Backend.Services.Contract
{
    public interface ISocialService
    {
        Task PostingRestaurantPostAsync(PostRestaurantReq req);
        Task<List<GetPostedPostRestDTO>> GetPostedPostRestAsync(int userId);
        Task<List<GetFollowedPostDTO>> GetFollowedPostAsync(int userId);
        Task FollowAsync(FollowActionReq req);
    }
}

