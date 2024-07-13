using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 学生干部考评管理系统模型.Enum;

namespace 学生干部考评管理系统模型.DTO
{
    /// <summary>
    /// 学生评价表
    /// </summary>
    public class EvaluationDto
    {
        /// <summary>
        /// 被评价的学生干部
        /// </summary>
        public string StudentCadreName { get; set; }

        /// <summary>
        /// 评价者
        /// </summary>
        public string EvaluatorName { get; set; }

        /// <summary>
        /// 评价类型
        /// </summary>
        public EvaluationType EvaluationType { get; set; } = EvaluationType.Self;

        /// <summary>
        /// 评分
        /// </summary>
        public float? Score { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? Comments { get; set; } = string.Empty;

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime? EvaluationDate { get; set; }
    }
}
