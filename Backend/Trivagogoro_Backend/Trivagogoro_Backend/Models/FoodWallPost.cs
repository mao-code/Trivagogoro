using System;
namespace Trivagogoro_Backend.Models
{
    public class FoodWallPost
    {
        public int id { get; set; }
        public string description { get; set; } = "";
        public string title { get; set; } = "";
        public int archivedNum { get; set; } = 0;
        public PostType type { get; set; } = PostType.Rest;
        public int sourceId { get; set; } // Rest => favoriteId, FoodMap => foodmapId
    }

    public enum PostType
    {
        Rest = 1,
        FoodMap = 2
    }
}

