using Microsoft.AspNetCore.Mvc;
using 学生干部考评管理系统模型.DTO;

namespace 学生干部考评管理系统API.Interface
{
    public interface IDashboardService
    {
        /// <summary>
        /// 获取学生的统计信息（教师）
        /// </summary>
        Task<GetDashboardInfo> GetStudentStatisticsInTeacher(int userId);

        /// <summary>
        /// 根据用户ID查询其所有评价
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的评价列表</returns>
        Task<ActionResult<IEnumerable<EvaluationDto>>> GetEvaluationsByUserId(int userId);

        /// <summary>
        /// 获取个人的统计信息（学生）
        /// </summary>
        Task<GetDashboardInfo> GetStudentStatisticsInStudent(int userId);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<GetDashboardInfo> GetAll();
    }
}
