using System;
namespace Trivagogoro_Backend.Models
{
    public class UserCredential
    {
        public int? id { get; set; }
        public int? userId { get; set; }
        public string account { get; set; } = null!;
        public string password { get; set; } = null!;
        public string? salt { get; set; }
    }
}

