using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enity
{
    /// <summary>
    /// 学生干部信息表
    /// 存储学生干部的基本信息和评估信息。
    /// </summary>
    public class StudentCadreInfo : BaseEnity
    {
        /// <summary>
        /// 关联的用户ID
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// 学生干部的基本信息
        /// </summary>
        public string BasicInfo { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 所属组织
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// 自评
        /// </summary>
        public string SelfEvaluation { get; set; }

        /// <summary>
        /// 互评
        /// </summary>
        public string PeerEvaluation { get; set; }

        /// <summary>
        /// 教师评价
        /// </summary>
        public string TeacherEvaluation { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public float TotalScore { get; set; }
    }
}
