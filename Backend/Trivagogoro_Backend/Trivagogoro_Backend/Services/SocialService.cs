using System;

namespace Trivagogoro_Backend.Services
{
    public class SocialService : BaseService, ISocialService
    {
        public SocialService(IConfiguration config) : base(config)
        {
        }

        public async Task PostingRestaurantPostAsync(PostRestaurantReq req)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                await conn.OpenAsync();
                using (var tran = await conn.BeginTransactionAsync())
                {
                    string sql = $@"
INSERT INTO FOODWALLPOST(title, description, archivedNum, type, sourceId)
VALUES('{req.Title}', '{req.Description}', 0, {req.Type}, {req.SourceId})
";
                    await conn.ExecuteAsync(sql);
                    await tran.CommitAsync();
                }
            }
        }
    }
}

