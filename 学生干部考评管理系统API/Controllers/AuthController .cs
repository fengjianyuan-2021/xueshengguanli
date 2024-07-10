using Microsoft.AspNetCore.Mvc;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginRequest">包含用户名和密码的登录请求</param>
        /// <returns>用户信息或未授权</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _authService.ValidateUserAsync(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { token = "dummy_token" , userId  = user.Id}); // 返回一个示例令牌
        }

        /// <summary>
        /// 忘记密码时修改密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>操作结果</returns>
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword)
        {
            var result = await _authService.ChangePasswordAsync(username, newPassword);
            if (!result)
            {
                return BadRequest("Password change failed");
            }
            return Ok("Password changed successfully");
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息或错误信息</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("用户未找到");
            }

            var userDto = new UserDto
            {
                Id = user.Id.ToString(),
                Name = user.Fullname,
                Avatar = Convert.ToBase64String(user.Avatar) // 将字节数组转换为 Base64 字符串
            };

            return Ok(userDto);
        }
    }
}
