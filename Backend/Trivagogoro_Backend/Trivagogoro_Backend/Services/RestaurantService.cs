using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace Trivagogoro_Backend.Services
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        public RestaurantService(IConfiguration config) : base(config)
        {
        }

        public async Task AddRastaurantToFavoriteAsync(AddRestaurantToFavoriteReq req)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT selfRating FROM FAVORITE WHERE userId={req.UserId} AND restaurantId={req.RestaurantId}";
                var tryFav = (await conn.QueryAsync<Favorite>(sql)).FirstOrDefault();

                await conn.OpenAsync();
                using (var tran = await conn.BeginTransactionAsync())
                {                   
                    if(tryFav != null)
                    {
                        sql = $@"
DELETE FROM FAVORITE
WHERE userId={req.UserId} AND restaurantId={req.RestaurantId}
";
                    }
                    else
                    {
                        sql = $@"
INSERT INTO FAVORITE(userId, restaurantId, selfRating)
VALUES({req.UserId}, {req.RestaurantId}, {req.Rating});
";
                    }
                    
                    await conn.ExecuteAsync(sql, transaction: tran);
                    await tran.CommitAsync();
                }
            }
        }

        public async ValueTask<int> CrawlAndSaveRestaurantsAsyncWithinTaipei()
        {
            #region Google Map API
            double lng = 25.03746;
            double lat = 121.564558;
            double radius = 13500; // meters
            string type = "restaurant";
            string lang = "zh-TW";
            string placeApi = $@"https://maps.googleapis.com/maps/api/place/nearbysearch/json?language={lang}&location={lng},{lat}&radius={radius}&type={type}&key={ApiKey}";

            var res = new List<JsonElement>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch/json");
               
                var response = await client.GetAsync(placeApi);
                
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    JsonDocument responseJson = JsonDocument.Parse(responseBody);
                    JsonElement result;
                    if (responseJson.RootElement.TryGetProperty("results", out result))
                    {
                        // add first result
                        res.Add(result);

                        // next page
                        JsonElement nextPageToken;
                        while (responseJson.RootElement.TryGetProperty("next_page_token", out nextPageToken))
                        {
                            placeApi = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken={nextPageToken}&key={ApiKey}";
                            response = await client.GetAsync(placeApi);
                            if (response.IsSuccessStatusCode)
                            {
                                responseBody = await response.Content.ReadAsStreamAsync();
                                responseJson = JsonDocument.Parse(responseBody);
                                if(responseJson.RootElement.TryGetProperty("results", out result))
                                {
                                    // add next results
                                    res.Add(result);
                                }
                            }
                        }
                    }
                }               
               
            }
            #endregion

            #region Store in DB
            var dataObjects = new List<Restaurant>();
            foreach(var batch in res)
            {
                if(batch.ValueKind == JsonValueKind.Array)
                {
                    foreach (var data in batch.EnumerateArray())
                    {
                        double dataLat = data.GetProperty("geometry").GetProperty("location").GetProperty("lat").GetDouble();
                        double dataLng = data.GetProperty("geometry").GetProperty("location").GetProperty("lng").GetDouble();

                        string name = data.GetProperty("name").GetString()!;
                        string placeId = data.GetProperty("place_id").GetString()!;
                        string address = data.GetProperty("vicinity").GetString()!;

                        JsonElement priceLevelElement = new JsonElement();
                        data.TryGetProperty("price_level", out priceLevelElement);

                        int priceLevel = 0;
                        if(priceLevelElement.ValueKind == JsonValueKind.Number && priceLevelElement.TryGetInt32(out int value))
                        {
                            priceLevel = value;
                        }

                        var dataObj = new Restaurant()
                        {
                            name = name,
                            lat = dataLat,
                            lng = dataLng,
                            address = address,
                            placeId = placeId,
                            priceLevel = priceLevel
                        };
                        dataObjects.Add(dataObj);
                    }
                }
            }

            int affectedRowNums = 0;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var tran = await conn.BeginTransactionAsync())
                {
                    string deleteAllSql = "DELETE FROM Restaurant;";
                    await conn.ExecuteAsync(deleteAllSql, transaction: tran);

                    foreach (var dataObj in dataObjects)
                    {
                        dataObj.name = dataObj.name.Replace("'", "\\'");
                        string sql = $@"INSERT INTO RESTAURANT(name, lat, lng, address, placeId, priceLevel)
                                    VALUES('{dataObj.name}', {dataObj.lat}, {dataObj.lng}, '{dataObj.address}', '{dataObj.placeId}', {dataObj.priceLevel});";

                        int num = await conn.ExecuteAsync(sql, transaction: tran);
                        affectedRowNums += num;
                        
                    }

                    await tran.CommitAsync();
                }
                    
            }
            #endregion

            return affectedRowNums;
        }

        public async Task<List<GetAllFavoriteRestaurantDTO>> GetAllFavoriteRestaurantsAsync(int userId)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = $@"
SELECT f.id AS favId, f.selfRating AS selfRating, r.id AS resId, r.name AS resName
FROM FAVORITE f
INNER JOIN RESTAURANT r
ON f.restaurantId = r.id
WHERE f.userId = {userId};";

                var dto = (await conn.QueryAsync<GetAllFavoriteRestaurantDTO>(sql)).ToList();

                return dto;
            }
        }

        public async Task<List<SearchRestaurantDTO>> SearchRestaurantAsync(string keyword)
        {
            var resList = new List<SearchRestaurantDTO>();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                // search DB
                string sql = $@"
                    SELECT r.*, f.id AS favId
                    FROM RESTAURANT r
                    LEFT JOIN FAVORITE f
                    ON r.id = f.restaurantId
                    WHERE name LIKE '%{keyword}%';
                ";
                var restaurants = (await conn.QueryAsync<RestaurantResDTO>(sql)).ToList();

                // search Google Map for images
                foreach(var restaurant in restaurants)
                {
                    var dto = new SearchRestaurantDTO();
                    string placeId = restaurant.placeId;
                    var images = await SearchRestaurantsImagesAsync(placeId);

                    // parse dto
                    dto.id = restaurant.id;
                    dto.name = restaurant.name;
                    dto.lat = restaurant.lat;
                    dto.lng = restaurant.lng;
                    dto.priceLevel = restaurant.priceLevel;
                    dto.placeId = restaurant.placeId;
                    dto.address = restaurant.address;
                    dto.favId = restaurant.favId;
                    dto.images = images;

                    resList.Add(dto);
                }
            }

            return resList;
        }

        public async Task<List<string>> SearchRestaurantsImagesAsync(string placeId)
        {
            var images = new List<string>();
            
            string placeDetailApi = $@"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={ApiKey}";
            using (var client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromMinutes(10);
                    var response = await client.GetAsync(placeDetailApi);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStreamAsync();
                        JsonDocument responseJson = JsonDocument.Parse(responseBody);
                        JsonElement result;
                        if (responseJson.RootElement.TryGetProperty("result", out result))
                        {
                            // add image
                            var photos = result.GetProperty("photos");
                            if (photos.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var image in photos.EnumerateArray())
                                {
                                    string imgReference = image.GetProperty("photo_reference").ToString();
                                    string imgUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={imgReference}&key={ApiKey}";
                                    images.Add(imgUrl);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                
            }


            return images;
        }
    }
}

