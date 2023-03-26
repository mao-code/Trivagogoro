using System;
using Trivagogoro_Backend.Models;

namespace Trivagogoro_Backend.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IConfiguration config) : base(config)
        {
        }

        /// <summary>
        /// Return the numbers of Affected rows
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        public async ValueTask<int> SignUpUserAsync(User user, UserCredential userCredential)
        {
            int rowAffected = 0;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                // test if account and passwod registered
                string testSql = $@"SELECT account FROM UserCredential WHERE account='{userCredential.account}';";
                if((await conn.QueryFirstOrDefaultAsync(testSql)) != null)
                {
                    return 0;
                }

                // insert user
                string userSql = $@"INSERT INTO `User`(name) VALUES('{user.name}');";
                rowAffected += await conn.ExecuteAsync(userSql);


                // hash password
                string salt = PasswordService.GenerateSalt();
                string hasedPassword = PasswordService.HashPassword(userCredential.password, salt);

                // get top id user
                User topIdUser = await this.GetTopIdUserAsync();

                // insert user credential
                string userCredentialSql = $@"
                    INSERT INTO UserCredential(userId, account, password, salt)
                    VALUES({topIdUser.id}, '{userCredential.account}', '{hasedPassword}', '{salt}');
                ";
                rowAffected += await conn.ExecuteAsync(userCredentialSql);

            }

            return rowAffected;
        }

        public async ValueTask<bool> SignInUserAsync(string account, string password)
        {
            bool isSuccess = false;

            using (var conn = new MySqlConnection(ConnectionString))
            {

                string sql = $@"SELECT * FROM UserCredential WHERE account='{account}';";
                UserCredential userCredential = await conn.QueryFirstOrDefaultAsync<UserCredential>(sql);

                bool isValidate = PasswordService.ValidatePassword(password, userCredential.password);

                if (userCredential != null && isValidate)
                {
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        public async Task<User> GetTopIdUserAsync()
        {
            User user = new User();
            using (var conn = new MySqlConnection(ConnectionString))
            {
                string sql = $@"SELECT * FROM `User` WHERE id = (SELECT MAX(id) FROM `User`);";
                user = await conn.QueryFirstAsync<User>(sql);
            }

            return user;
        }
    }
}

