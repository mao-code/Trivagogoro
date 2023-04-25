using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trivagogoro_Backend.Controllers.Social
{
    public class SocialController : BaseController
    {
        private ISocialService _socialService;

        public SocialController(ISocialService socialService)
        {
            _socialService = socialService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostFavoriteRestaurant([FromBody] PostRestaurantReq req)
        {
            await _socialService.PostingRestaurantPostAsync(req);
            var res = new ResponseData<SignUpRes>(200, true, $"發文成功！", null);
            return StatusCode(res.Code, res);
        }
    }
}

