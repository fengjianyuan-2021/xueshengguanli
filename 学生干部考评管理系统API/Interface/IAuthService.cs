using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">用户密码</param>
        /// <returns>如果验证成功，返回用户信息；否则返回 null</returns>
        Task<User> ValidateUserAsync(string username, string password);

        /// <summary>
        /// 修改密码（忘记密码时使用）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>如果修改成功，返回 true；否则返回 false</returns>
        Task<bool> ChangePasswordAsync(string username, string newPassword);

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息</returns>
        Task<User> GetUserByIdAsync(int userId);
    }
}
