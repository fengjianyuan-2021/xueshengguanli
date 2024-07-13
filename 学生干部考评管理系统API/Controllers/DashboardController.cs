using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using 学生干部考评管理系统API.Interface;
using static 学生干部考评管理系统API.Controllers.UserController;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enum;
using Microsoft.EntityFrameworkCore;

namespace 学生干部考评管理系统API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService dashboardService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取统计信息
        /// </summary>
        [HttpPost("GetDashboardTeacher")]
        public async Task<ActionResult<GetDashboardInfo>> GetDashboardTeacher([FromBody] GetStudentStatisticsInStudentParam getStudentStatisticsInStudent)
        {
            var dashboardTeachers = await _dashboardService.GetStudentStatisticsInTeacher( getStudentStatisticsInStudent.userId);
            return Ok(dashboardTeachers);
        }

        /// <summary>
        /// 根据用户ID查询其所有评价
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的评价列表</returns>
        [HttpGet("GetEvaluationsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetEvaluationsByUserId(int userId)
        {
            var evaluations = await _dashboardService.GetEvaluationsByUserId(userId);
            return Ok(evaluations);
        }

        public class GetStudentStatisticsInStudentParam { 

            public int userId { get; set; }
        }

        /// <summary>
        /// 根据用户ID查询其所有评价
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的评价列表</returns>
        [HttpPost("GetStudentStatisticsInStudent")]
        public async Task<ActionResult<IEnumerable<GetDashboardInfo>>> GetStudentStatisticsInStudent([FromBody] GetStudentStatisticsInStudentParam getStudentStatisticsInStudent)
        {
            var evaluations = await _dashboardService.GetStudentStatisticsInStudent(getStudentStatisticsInStudent.userId);
            return Ok(evaluations);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的评价列表</returns>
        [HttpPost("GetAll")]
        public async Task<ActionResult<IEnumerable<GetDashboardInfo>>> GetAll( )
        {
            var evaluations = await _dashboardService.GetAll();
            return Ok(evaluations);
        }

    }
}
