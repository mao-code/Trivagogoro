using System;
namespace Trivagogoro_Backend.Models.DTO.Reqs
{
    public class PostRestaurantReq
    {
        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public PostType Type { get; set; } = PostType.Rest;

        [Required]
        public int SourceId { get; set; }
    }
}

