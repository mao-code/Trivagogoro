using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class GetPostedPostRestDTO
    {
        public int postId { get; set; }
        public int sourceId { get; set; }
        public string title { get; set; } = "";
        public string description { get; set; } = "";
        public int archivedNum { get; set; } = 0;

        public int favId { get; set; }
        public int restId { get; set; }
        public string placeId { get; set; } = "";

        public List<string> images { get; set; } = new List<string>();
    }
}

