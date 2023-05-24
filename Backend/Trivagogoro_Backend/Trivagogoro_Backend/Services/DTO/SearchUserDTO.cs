using System;
namespace Trivagogoro_Backend.Services.DTO
{
    public class SearchUserDTO
    {
        public int userId { get; set; }
        public string userName { get; set; } = "";
        public bool isFollow { get; set; }
    }
}

