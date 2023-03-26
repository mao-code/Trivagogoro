using System;
namespace Trivagogoro_Backend.Models.DTO.Reqs
{
    public class SignInReq
    {
        [Required]
        public string Account { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

