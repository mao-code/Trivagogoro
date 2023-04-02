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
        public async ValueTask<int> SignUpUserAsync(SignUpReq req)
        {
            int rowAffected = 0;

            using (var conn = new MySqlConnection(ConnectionString))
            {
                // test if account and passwod registered
                string testSql = $@"SELECT account FROM UserCredential WHERE account='{req.Account}';";
                if((await conn.QueryFirstOrDefaultAsync(testSql)) != null)
                {
                    return 0;
                }

                // insert user
                string userSql = $@"INSERT INTO `User`(name) VALUES('{req.Name}');";
                rowAffected += await conn.ExecuteAsync(userSql);


                // hash password
                string salt = PasswordService.GenerateSalt();
                string hasedPassword = PasswordService.HashPassword(req.Password, salt);

                // get top id user
                User topIdUser = await this.GetTopIdUserAsync();

                // insert user credential
                string userCredentialSql = $@"
                    INSERT INTO UserCredential(userId, account, password, salt)
                    VALUES({topIdUser.id}, '{req.Account}', '{hasedPassword}', '{salt}');
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

                if(userCredential == null)
                {
                    return false;
                }

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

