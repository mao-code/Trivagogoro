using System;
namespace Trivagogoro_Backend.Models.DTO.Reses
{
    public class GetFollowedPostsRes
    {
        public List<GetFollowedPostDTO> FollowedPostDTOs { get; set; } = new List<GetFollowedPostDTO>();
    }
}

