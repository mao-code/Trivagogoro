using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet("search/{keywords}")]
        public async Task<IActionResult> SearchRestaurant([FromRoute] string keywords)
        {
            var data = await _restaurantService.SearchRestaurantAsync(keywords);
            var res = new ResponseData<object>(200, true, "成功搜尋餐廳！", data);
            return StatusCode(res.Code, res);
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> AddRestaurantToFavorite([FromBody] AddRestaurantToFavoriteReq req)
        {
            await _restaurantService.AddRastaurantToFavoriteAsync(req);
            var res = new ResponseData<object>(200, true, "成功執行加入或移除最愛！", null);
            return StatusCode(res.Code, res);
        }

        [HttpGet("favorite/all/{userId}")]
        public async Task<IActionResult> GetAllFavoriteRestaurant([FromRoute] int userId)
        {
            var dto = await _restaurantService.GetAllFavoriteRestaurantsAsync(userId);
            var res = new ResponseData<GetAllFavoriteRestaurantRes>(200, true, "成功獲得所有最愛餐廳！", new GetAllFavoriteRestaurantRes()
            {
                FavoriteRestaurants = dto
            });
            return StatusCode(res.Code, res);
        }

        // remember use my iphone network to do this
        [HttpGet("test")]
        public async Task<string> Test()
        {
            var api = "https://maps.googleapis.com/maps/api/place/details/json?place_id=ChIJk9KTC8arQjQRjnNY8f7CNhw&key=AIzaSyD27yNCh8H_qqCxK_m9fLiK3k42KV7MIi0";
            //api = "http://140.119.19.77/nccupass/Crawler/departments";

            using (var c = new HttpClient())
            {
                try
                {
                    var res = await c.GetAsync(api);
                    return res.ToString();
                }
                catch (HttpRequestException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return e.Message;
                }

            }
        }
    }
}

