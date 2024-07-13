using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统API.Helper;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统数据库映射;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enum;

namespace 学生干部考评管理系统API.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DashboardService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取学生的统计信息（教师）
        /// </summary>
        public async Task<GetDashboardInfo> GetStudentStatistics()
        {
            // 学生总数
            var totalStudentsTask = _context.Users.CountAsync(r => !r.IsDeleted && r.Role == UserRole.StudentCadre);

            // 已评价的学生总数
            var evaluatedStudentsTask = _context.Users
                .Include(r => r.OtherEvaluations)
                .Where(r => r.OtherEvaluations != null && r.OtherEvaluations.Any(e => e.EvaluationType == EvaluationType.Teacher))
                .CountAsync();

            // 平均分数
            var averageScoreTask = _context.Evaluations
                .Where(e => e.EvaluationType == EvaluationType.Teacher && e.Score.HasValue)
                .AverageAsync(e => e.Score.Value);

            // 获取评价类别占比的总数
            var totalCountTask = _context.Evaluations.CountAsync();

            await Task.WhenAll(totalStudentsTask, evaluatedStudentsTask, averageScoreTask, totalCountTask);

            var totalStudents = totalStudentsTask.Result;
            var evaluatedStudents = evaluatedStudentsTask.Result;
            var averageScore = averageScoreTask.Result;
            var totalCount = totalCountTask.Result;

            // 评价类别占比
            int selfCount = 0;
            int peerCount = 0;
            int teacherCount = 0;

            if (totalCount > 0)
            {
                var selfCountTask = _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Self);
                var peerCountTask = _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Peer);
                var teacherCountTask = _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Teacher);

                await Task.WhenAll(selfCountTask, peerCountTask, teacherCountTask);

                selfCount = selfCountTask.Result;
                peerCount = peerCountTask.Result;
                teacherCount = teacherCountTask.Result;
            }

            // 学生统计评价查询 查询教师对学生的评价
            var evaluations = await _context.Evaluations
                .Include(e => e.StudentCadreInfo)
                .Where(e => e.StudentCadreInfo != null && e.StudentCadreInfo.Role == UserRole.StudentCadre && e.EvaluationType == EvaluationType.Teacher)
                .ToListAsync();

            var studentEvaluationSummaries = evaluations
                .GroupBy(e => e.StudentCadreInfo.Username)
                .Select(g => new StudentEvaluationSummaryDto
                {
                    Id = g.First().StudentCadreId,
                    Username = g.Key,
                    Fullname = g.First().StudentCadreInfo?.Fullname ?? string.Empty,
                    AverageScore = g.Average(e => e.Score.HasValue ? e.Score.Value : 0)
                })
                .ToList();

            // 创建并返回统计信息
            GetDashboardInfo getDashboardInfo = new()
            {
                StudentSums = totalStudents,
                EvaluatedStudentCount = evaluatedStudents,
                UnevaluatedStudentCount = totalStudents - evaluatedStudents,
                AverageScore = averageScore.ToString("F2"),
                EvaluatedStudentPercentage = totalStudents > 0 ? ((float)evaluatedStudents / totalStudents * 100).ToString("F2") : "0",
                UnevaluatedStudentPercentage = totalStudents > 0 ? ((float)(totalStudents - evaluatedStudents) / totalStudents * 100).ToString("F2") : "0",
                SelfPercentage = totalCount > 0 ? (int)((selfCount / (double)totalCount) * 100) : 0,
                PeerPercentage = totalCount > 0 ? (int)((peerCount / (double)totalCount) * 100) : 0,
                TeacherPercentage = totalCount > 0 ? (int)((teacherCount / (double)totalCount) * 100) : 0,
                StudentEvaluationSummaryDtos = studentEvaluationSummaries
            };

            return getDashboardInfo;
        }


        /// <summary>
        /// 获取个人的统计信息（学生）
        /// </summary>
        public async Task<GetDashboardInfo> GetStudentStatistics(int userId)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            if (currentUser == null)
            {
                return null;
            }

            // 学生总数
            var totalStudents = await _context.Users.CountAsync(r => !r.IsDeleted && r.Role == UserRole.StudentCadre);

            // 已评价的学生总数
            var evaluatedStudents = await _context.Evaluations
                .Where(e => e.EvaluationType == EvaluationType.Peer && e.EvaluatorId == userId)
                .Select(e => e.StudentCadreId)
                .Distinct()
                .CountAsync();

            // 平均分数
            var averageScore = await _context.Evaluations
                .Where(e => e.EvaluationType == EvaluationType.Peer && e.Score.HasValue && e.EvaluatorId == userId)
                .AverageAsync(e => e.Score.Value);

            // 评价类别占比
            var totalCount = await _context.Evaluations.CountAsync(e => e.EvaluatorId == userId);
            var selfCount = await _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Self && e.EvaluatorId == userId);
            var peerCount = await _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Peer && e.EvaluatorId == userId);
            var teacherCount = await _context.Evaluations.CountAsync(e => e.EvaluationType == EvaluationType.Teacher && e.EvaluatorId == userId);

            // 学生统计评价查询
            var evaluations = await _context.Evaluations
                .Where(r => r.EvaluatorId == userId && r.EvaluationType == EvaluationType.Peer)
                .Include(e => e.StudentCadreInfo)
                .Where(e => e.StudentCadreInfo != null && e.StudentCadreInfo.Role == UserRole.StudentCadre)
                .ToListAsync();

            var studentEvaluationSummaries = evaluations
                .GroupBy(e => e.StudentCadreInfo.Username)
                .Select(g => new StudentEvaluationSummaryDto
                {
                    Id = g.First().StudentCadreId,
                    Username = g.Key,
                    Fullname = g.First().StudentCadreInfo?.Fullname ?? string.Empty,
                    AverageScore = g.Average(e => e.Score.HasValue ? e.Score.Value : 0)
                })
                .ToList();

            // 创建并返回统计信息
            GetDashboardInfo getDashboardInfo = new()
            {
                StudentSums = totalStudents,
                EvaluatedStudentCount = evaluatedStudents,
                UnevaluatedStudentCount = totalStudents - evaluatedStudents,
                AverageScore = averageScore.ToString("F2"),
                EvaluatedStudentPercentage = totalStudents > 0 ? ((float)evaluatedStudents / totalStudents * 100).ToString("F2") : "0",
                UnevaluatedStudentPercentage = totalStudents > 0 ? ((float)(totalStudents - evaluatedStudents) / totalStudents * 100).ToString("F2") : "0",
                SelfPercentage = totalCount > 0 ? (int)((selfCount / (double)totalCount) * 100) : 0,
                PeerPercentage = totalCount > 0 ? (int)((peerCount / (double)totalCount) * 100) : 0,
                TeacherPercentage = totalCount > 0 ? (int)((teacherCount / (double)totalCount) * 100) : 0,
                StudentEvaluationSummaryDtos = studentEvaluationSummaries
            };

            return getDashboardInfo;
        }



        /// <summary>
        /// 根据用户ID查询其所有评价
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户的评价列表</returns>
        public async Task<ActionResult<IEnumerable<EvaluationDto>>> GetEvaluationsByUserId(int userId)
        {
            var evaluations = await _context.Evaluations
                .Include(e => e.StudentCadreInfo)
                .Include(e => e.User)
                .Where(e => e.StudentCadreId == userId || e.EvaluatorId == userId)
                .ToListAsync();

            var evaluationDtos = _mapper.Map<List<EvaluationDto>>(evaluations);

            return evaluationDtos;
        }

    }
}
