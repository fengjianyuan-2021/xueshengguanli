﻿using AutoMapper;
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
        public async Task<ActionResult<GetDashboardInfo>> GetDashboardTeacher()
        {
            var dashboardTeachers = await _dashboardService.GetStudentStatistics();
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

    }
}
