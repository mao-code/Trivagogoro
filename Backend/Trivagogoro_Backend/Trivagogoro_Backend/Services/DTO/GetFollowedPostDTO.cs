using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class GetFollowedPostDTO
    {
        // for followed user
        public int flId { get; set; }
        public string flName { get; set; } = "";

        // for the post
        public int fwpId { get; set; }
        public string fwpDescription { get; set; } = "";
        public string fwpTitle { get; set; } = "";
        public int fwpArchivedNum { get; set; } = 0;
        public PostType fwpType { get; set; } = PostType.Rest;
        public int fwpSourceId { get; set; } // Rest => favoriteId, FoodMap => foodmapId

        // for restaurant
        public string placeId { get; set; } = "";
        public List<string> images { get; set; } = new List<string>();
    }
}

