using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enum;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API.Interface
{
    /// <summary>
    /// 用户服务接口，定义用户操作的基本方法
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        Task<IEnumerable<UserDto>> GetAllUsersAsync( UserRole userRole);

        /// <summary>
        /// 通过ID获取用户
        /// </summary>
        /// <param name="id">用户ID</param>
        Task<UserDto> GetUserByIdAsync(int id);

        /// <summary>
        /// 通过ID获取用户
        /// </summary>
        /// <param name="id">用户ID</param>
        Task<UserDto> GetUserAndEvaluationAsync(int id);

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="user">用户对象</param>
        Task<UserDto?> CreateUserAsync(CreateUserDto user);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        Task<User> UpdateUserAsync(UserDto user);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        Task<bool> DeleteUserAsync(int id);
    }
}
