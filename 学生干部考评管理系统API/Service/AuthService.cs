using System.Text;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;
using 学生干部考评管理系统数据库映射;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using 学生干部考评管理系统API.Helper;

namespace 学生干部考评管理系统API.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext context, ILogger<AuthService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果验证成功，返回用户信息；否则返回 null</returns>
        public async Task<User> ValidateUserAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user != null && VerifyPasswordHash(password, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return null;
             
            }
           
        }

        /// <summary>
        /// 修改密码（忘记密码时使用）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>如果修改成功，返回 true；否则返回 false</returns>
        public async Task<bool> ChangePasswordAsync(string username, string newPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.IsDeleted==false);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Username}", username);
                return false;
            }

            user.PasswordHash = CodeHelper.CreatePasswordHash(newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息</returns>
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstAsync(r => r.Id==userId);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // 使用适当的方法验证密码哈希
            // 这里仅作为示例，实际生产环境请使用更安全的哈希验证方法
            return storedHash ==CodeHelper.CreatePasswordHash(password);
        }

       
    }
}
