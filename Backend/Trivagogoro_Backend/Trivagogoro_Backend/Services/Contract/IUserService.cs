using System;
namespace Trivagogoro_Backend.Services.Contract
{
    public interface IUserService
    {
        ValueTask<int> SignUpUserAsync(User user, UserCredential userCredential);
        ValueTask<bool> SignInUserAsync(string account, string password); // 為求簡單就不用JWT了（不是重點）
        Task<User> GetTopIdUserAsync();
    }
}

