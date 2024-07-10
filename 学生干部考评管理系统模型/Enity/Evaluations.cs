using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enity
{
    /// <summary>
    /// 评价表
    /// 存储具体的评价记录。
    /// </summary>
    public class Evaluation : BaseEnity
    {
        /// <summary>
        /// 关联的学生干部ID
        /// </summary>
        [ForeignKey("StudentCadreInfo")]
        public int StudentCadreId { get; set; }

        /// <summary>
        /// 评价者的用户ID
        /// </summary>
        [ForeignKey("User")]
        public int EvaluatorId { get; set; }

        /// <summary>
        /// 评价类型
        /// </summary>
        public EvaluationType EvaluationType { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime EvaluationDate { get; set; }
    }

    public enum EvaluationType
    {
        Self,
        Peer,
        Teacher
    }
}
