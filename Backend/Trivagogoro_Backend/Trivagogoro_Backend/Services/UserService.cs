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
                using (var tran = await conn.BeginTransactionAsync())
                {
                    // test if account and passwod registered
                    string testSql = $@"SELECT account FROM USERCREDENTIAL WHERE account='{req.Account}';";
                    if ((await conn.QueryFirstOrDefaultAsync(testSql)) != null)
                    {
                        return 0;
                    }

                    // insert user
                    string userSql = $@"INSERT INTO `USER`(name) VALUES('{req.Name}');";
                    rowAffected += await conn.ExecuteAsync(userSql, tran);


                    // hash password
                    string salt = PasswordService.GenerateSalt();
                    string hasedPassword = PasswordService.HashPassword(req.Password, salt);

                    // get top id user
                    User topIdUser = await this.GetTopIdUserAsync();

                    // insert user credential
                    string userCredentialSql = $@"
                    INSERT INTO USERCREDENTIAL(userId, account, password, salt)
                    VALUES({topIdUser.id}, '{req.Account}', '{hasedPassword}', '{salt}');
                    ";
                    rowAffected += await conn.ExecuteAsync(userCredentialSql, tran);

                    await tran.CommitAsync();
                }
                
            }

            return rowAffected;
        }

        public async Task<SignInDTO> SignInUserAsync(string account, string password)
        {
            bool isSuccess = false;
            UserCredential userCredential;

            using (var conn = new MySqlConnection(ConnectionString))
            {

                string sql = $@"SELECT * FROM UserCredential WHERE account='{account}';";
                userCredential = await conn.QueryFirstOrDefaultAsync<UserCredential>(sql);

                if(userCredential == null)
                {
                    return new SignInDTO()
                    {
                        IsSuccess = false,
                        UserId = 0
                    };
                }

                bool isValidate = PasswordService.ValidatePassword(password, userCredential.password);

                if (userCredential != null && isValidate)
                {
                    isSuccess = true;
                }
            }

            return new SignInDTO()
            {
                IsSuccess = isSuccess,
                UserId = userCredential!.userId ?? 0
            };
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

        public async Task<User> GetUserInfoAsync(int userId)
        {
            string sql = $"SELECT * FROM `USER` WHERE id = {userId}";
            using (var conn = new MySqlConnection(ConnectionString))
            {
                var user = (await conn.QueryAsync<User>(sql)).First();

                return user;
            }
        }
    }
}

