using System;
namespace Trivagogoro_Backend.Models.DTO.Reqs
{
    public class FollowActionReq
    {
        [Required]
        public int FromUserId { get; set; }

        [Required]
        public int ToUserId { get; set; }

        [Required]
        public bool FollowValue { get; set; }
    }
}

