using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.DTO
{
    public class GetDashboardInfo
    {
        /// <summary>
        /// 学生总数
        /// </summary>
        public int StudentSums{ get; set; } 

        /// <summary>
        /// 已评价的学生总数
        /// </summary>
        public int EvaluatedStudentCount{ get; set; }

        /// <summary>
        /// 未评价的学生总数
        /// </summary>
        public int UnevaluatedStudentCount { get; set; }

        /// <summary>
        /// 平均分数
        /// </summary>
        public string AverageScore { get; set; }

        /// <summary>
        /// 已评价学生占比
        /// </summary>
        public string EvaluatedStudentPercentage { get; set; }

        /// <summary>
        /// 未评价学生占比
        /// </summary>
        public string UnevaluatedStudentPercentage { get; set; }

        /// <summary>
        /// 自评数量
        /// </summary>
        public int SelfPercentage { get; set; }

        /// <summary>
        /// 互评数量
        /// </summary>
        public int PeerPercentage { get; set; }

        /// <summary>
        /// 教师评价数量
        /// </summary>
        public int TeacherPercentage { get; set; }

        /// <summary>
        /// 历史评价总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 学时统计
        /// </summary>
        public float ClassHourCount { get; set; } = 0;

        /// <summary>
        /// 教师对学生的评价查询
        /// </summary>
        public List<StudentEvaluationSummaryDto> StudentEvaluationSummaryDtos { get; set; }

        /// <summary>
        /// 我的历史评价
        /// </summary>
        public List<EvaluationDto>? EvaluationDtos { get; set; }

        /// <summary>
        /// 用户时长
        /// </summary>
        public List<float>? HourList { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<string>? UserList { get; set; }


    }
}
