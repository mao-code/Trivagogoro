using System;

namespace Trivagogoro_Backend.Models.DTO.Reqs
{
    public class SignUpReq
    {
        [Required]
        public Trivagogoro_Backend.Models.User User { get; set; } = null!;

        [Required]
        public UserCredential UserCredential { get; set; } = null!;
    }
}

