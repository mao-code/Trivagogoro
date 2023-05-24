using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trivagogoro_Backend.Controllers.User
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpReq req)
        {
            var affectedRowNums = await this._userService.SignUpUserAsync(req);
            var res = new ResponseData<SignUpRes>(200, true, $"成功註冊！影響列數{affectedRowNums}", null);
            if(affectedRowNums == 0)
            {
                res = new ResponseData<SignUpRes>(403, false, "此帳號已被註冊！", null);
            }

            return StatusCode(res.Code, res);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInReq req)
        {
            SignInDTO dto = await this._userService.SignInUserAsync(req.Account, req.Password);
            var res = new ResponseData<SignInRes>(200, true, $"登入成功！", new SignInRes()
            {
                UserId = dto.UserId
            });

            if(!dto.IsSuccess)
            {
                res = new ResponseData<SignInRes>(404, false, "登入失敗！", null);
            }

            return StatusCode(res.Code, res);
        }

        [HttpGet("info/{userId}")]
        public async Task<IActionResult> GetUserInfo([FromRoute] int userId)
        {
            var user = await _userService.GetUserInfoAsync(userId);
            var res = new ResponseData<Models.User>(200, true, "Get user info successfully!", user);

            return StatusCode(res.Code, res);
        }

        [HttpGet("search/{userName}/{userId}")]
        public async Task<IActionResult> SearchUser([FromRoute] string userName, [FromRoute] int userId)
        {
            var dtos = await _userService.SearchUserAsync(userName, userId);
            var res = new ResponseData<List<SearchUserDTO>>(200, true, "Search user successfully!", dtos);

            return StatusCode(res.Code, res);
        }
    }
}

