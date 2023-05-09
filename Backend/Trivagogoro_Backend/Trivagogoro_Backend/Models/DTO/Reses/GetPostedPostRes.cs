using System;
namespace Trivagogoro_Backend.Models.DTO.Reses
{
    public class GetPostedPostResRest
    {
        public List<GetPostedPostRestDTO> PostedPostRest { get; set; } = new List<GetPostedPostRestDTO>();
    }
}

