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
        public async Task<GetDashboardInfo> GetStudentStatisticsInTeacher(int userId)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            if (currentUser == null)
            {
                return null;
            }
            // 我的历史评价查询
            var myevaluations = await _context.Evaluations.Where(e => e.EvaluatorId == userId && e.IsDeleted == false).OrderByDescending(r => r.CreateOn).Include(r => r.StudentCadreInfo).Include(r => r.User).ToListAsync();

            //学时查询
            var suerList = await _context.Users.Where(e => e.IsDeleted==false && e.Role==UserRole.StudentCadre).ToListAsync();
            var nameList = suerList.OrderByDescending(r => r.Id).Select(r => r.Fullname).ToList();
            var hourList = suerList.OrderByDescending(r => r.Id).Select(r => r.ClassHour ?? 0).ToList();

            //学生总数
            var totalStudents = await _context.Users.CountAsync(r => !r.IsDeleted && r.Role == UserRole.StudentCadre && r.Id != userId);

            // 我已评价的学生总数
            var evaluatedStudents = myevaluations.Where(e => e.EvaluationType == EvaluationType.Teacher).Select(e => e.StudentCadreId).Distinct().Count();

            // 我评价的平均分数
            var averageScore = myevaluations
                .Where(e => e.EvaluationType == EvaluationType.Teacher && e.Score.HasValue)
                .Average(e => e.Score.Value);

            //我的评价类别占比
            var totalCount = myevaluations.Count;
            var selfCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Self);
            var peerCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Peer);
            var teacherCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Teacher);

            // 我的最新评价查询(不包括自评)
            var evaluations = myevaluations
                .Where(e => e.StudentCadreInfo != null && e.StudentCadreInfo.Role == UserRole.StudentCadre).OrderByDescending(r => r.UpdateOn)
                .ToList();

            var studentEvaluationSummaries = evaluations
                .GroupBy(e => e.StudentCadreInfo.Username)
                .Select(g => new StudentEvaluationSummaryDto
                {
                    Id = g.First().StudentCadreId,
                    Username = g.Key,
                    Fullname = g.First().StudentCadreInfo?.Fullname ?? string.Empty,
                    AverageScore = g.Average(e => e.Score.HasValue ? e.Score.Value : 0),
                    EvaluationDate = g.First().EvaluationDate.HasValue ? g.First().EvaluationDate.Value : DateTime.MinValue,
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
                StudentEvaluationSummaryDtos = studentEvaluationSummaries,
                TotalCount = totalCount,
                EvaluationDtos = _mapper.Map<List<EvaluationDto>>(myevaluations),
                HourList = hourList,
                UserList = nameList,

            };
            return getDashboardInfo;
        }


        /// <summary>
        /// 获取个人的统计信息（学生）
        /// </summary>
        public async Task<GetDashboardInfo> GetStudentStatisticsInStudent(int userId)
        {
            var currentUser = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            if (currentUser == null)
            {
                return null;
            }
            // 我的历史评价查询
            var myevaluations = await _context.Evaluations.Where(e => e.EvaluatorId == userId && e.IsDeleted == false).OrderByDescending(r=>r.CreateOn).Include(r => r.StudentCadreInfo).Include(r => r.User).ToListAsync();

            //学生总数
            var totalStudents = await _context.Users.CountAsync(r => !r.IsDeleted && r.Role == UserRole.StudentCadre && r.Id!=userId);

            // 我已评价的学生总数
            var evaluatedStudents = myevaluations.Where(e => e.EvaluationType == EvaluationType.Peer).Select(e => e.StudentCadreId).Distinct().Count();

            // 我评价的平均分数
            var averageScore = myevaluations
                .Where(e => e.EvaluationType == EvaluationType.Peer && e.Score.HasValue)
                .Average(e => e.Score.Value);

            //我的评价类别占比
            var totalCount = myevaluations.Count;
            var selfCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Self);
            var peerCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Peer);
            var teacherCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Teacher);

            // 我的最新评价查询(不包括自评)
            var evaluations = myevaluations
                .Where(r => r.EvaluationType==EvaluationType.Peer)
                .Where(e => e.StudentCadreInfo != null && e.StudentCadreInfo.Role == UserRole.StudentCadre).OrderByDescending(r=>r.UpdateOn)
                .ToList();

            var studentEvaluationSummaries = evaluations
                .GroupBy(e => e.StudentCadreInfo.Username)
                .Select(g => new StudentEvaluationSummaryDto
                {
                    Id = g.First().StudentCadreId,
                    Username = g.Key,
                    Fullname = g.First().StudentCadreInfo?.Fullname ?? string.Empty,
                    AverageScore = g.Average(e => e.Score.HasValue ? e.Score.Value : 0),
                    EvaluationDate = g.First().EvaluationDate.HasValue ? g.First().EvaluationDate.Value :DateTime.MinValue ,
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
                StudentEvaluationSummaryDtos = studentEvaluationSummaries,
                TotalCount = totalCount,
                EvaluationDtos = _mapper.Map<List<EvaluationDto>>(myevaluations),
                ClassHourCount = currentUser.ClassHour!=null? currentUser.ClassHour.Value:0,

            };

            return getDashboardInfo;
        }


        /// <summary>
        /// 获取管理员的统计信息（所有）
        /// </summary>
        public async Task<GetDashboardInfo> GetAll()
        {
            // 历史评价查询
            var myevaluations = await _context.Evaluations.Where(e => e.IsDeleted == false).OrderByDescending(r => r.CreateOn).Include(r => r.StudentCadreInfo).Include(r => r.User).ToListAsync();

            //学生总数
            var totalStudents = await _context.Users.CountAsync(r => !r.IsDeleted && r.Role == UserRole.StudentCadre);

            //学时查询
            var suerList = await _context.Users.Where(e => e.IsDeleted == false && e.Role == UserRole.StudentCadre).ToListAsync();
            var nameList = suerList.OrderByDescending(r => r.Id).Select(r => r.Fullname).ToList();
            var hourList = suerList.OrderByDescending(r => r.Id).Select(r => r.ClassHour ?? 0).ToList();

            // 已评价的学生总数
            var evaluatedStudents = myevaluations.Where(e => e.EvaluationType == EvaluationType.Teacher).Select(e => e.StudentCadreId).Distinct().Count();

            // 评价的平均分数
            var averageScore = myevaluations
                .Where(e => e.EvaluationType == EvaluationType.Teacher && e.Score.HasValue)
                .Average(e => e.Score.Value);

            //评价类别占比
            var totalCount = myevaluations.Count;
            var selfCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Self);
            var peerCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Peer);
            var teacherCount = myevaluations.Count(e => e.EvaluationType == EvaluationType.Teacher);

            // 最新评价查询
            var evaluations = myevaluations
                .Where(e => e.StudentCadreInfo != null && e.StudentCadreInfo.Role == UserRole.StudentCadre).OrderByDescending(r => r.UpdateOn)
                .ToList();

            var studentEvaluationSummaries = evaluations
                .GroupBy(e => e.StudentCadreInfo.Username)
                .Select(g => new StudentEvaluationSummaryDto
                {
                    Id = g.First().StudentCadreId,
                    Username = g.Key,
                    Fullname = g.First().StudentCadreInfo?.Fullname ?? string.Empty,
                    AverageScore = g.Average(e => e.Score.HasValue ? e.Score.Value : 0),
                    EvaluationDate = g.First().EvaluationDate.HasValue ? g.First().EvaluationDate.Value : DateTime.MinValue,
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
                StudentEvaluationSummaryDtos = studentEvaluationSummaries,
                TotalCount = totalCount,
                EvaluationDtos = _mapper.Map<List<EvaluationDto>>(myevaluations),
                HourList = hourList,
                UserList = nameList,
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
