using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trivagogoro_Backend.Controllers.Restaurant
{
    public class RestaurantController : BaseController
    {
        private IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("crawler/taipei")]
        public async Task<IActionResult> CrawlGoogleMapAndSaveRestaurant()
        {
            var res = new ResponseData<object>(200, true, "成功紀錄所有台北市內的餐廳！", await _restaurantService.CrawlAndSaveRestaurantsAsyncWithinTaipei());
            return StatusCode(res.Code, res);
        }
    }
}

