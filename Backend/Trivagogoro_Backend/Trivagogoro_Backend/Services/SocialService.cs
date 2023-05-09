using System;

namespace Trivagogoro_Backend.Services
{
    public class SocialService : BaseService, ISocialService
    {
        private IRestaurantService _restService;

        public SocialService(IConfiguration config, IRestaurantService restaurantService) : base(config)
        {
            _restService = restaurantService;
        }

        public async Task<List<GetPostedPostRestDTO>> GetPostedPostRestAsync(int userId)
        {
            string sql = $@"
                SELECT fwp.id AS postId, fwp.sourceId, fwp.title, fwp.description, fwp.archivedNum, fav.id AS favId, res.id AS restId, res.placeId AS placeId
                FROM FOODWALLPOST fwp
                INNER JOIN FAVORITE fav
                ON fwp.sourceId = fav.id
                INNER JOIN RESTAURANT res
                ON fav.restaurantId = res.id
                WHERE fwp.userId = {userId};
            ";

            using (var conn = new MySqlConnection(ConnectionString))
            {
                var dtos = await conn.QueryAsync<GetPostedPostRestDTO>(sql);


                // images
                foreach (var dto in dtos)
                {
                    dto.images = await _restService.SearchRestaurantsImagesAsync(dto.placeId);
                }

                return dtos.ToList();
            }     
        }

        public async Task PostingRestaurantPostAsync(PostRestaurantReq req)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var tran = await conn.BeginTransactionAsync())
                {
                    string sql = $@"
INSERT INTO FOODWALLPOST(title, description, archivedNum, type, sourceId, userId)
VALUES('{req.Title}', '{req.Description}', 0, {((int)req.Type)}, {req.SourceId}, {req.userId})
";
                    await conn.ExecuteAsync(sql, transaction: tran);
                    await tran.CommitAsync();
                }
            }
        }
    }
}

