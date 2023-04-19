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
                client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch/");
               
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
                        string sql = $@"INSERT INTO Restaurant(name, lat, lng, address, placeId, priceLevel)
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
    }
}

