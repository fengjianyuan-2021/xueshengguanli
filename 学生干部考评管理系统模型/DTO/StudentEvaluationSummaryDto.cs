using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.DTO
{
    /// <summary>
    /// 教师对学生评价统计
    /// </summary>
    public class StudentEvaluationSummaryDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生账号
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// 评价平均分
        /// </summary>
        public float AverageScore { get; set; }
    }

}
