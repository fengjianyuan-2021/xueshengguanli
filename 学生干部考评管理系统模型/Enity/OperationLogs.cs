using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enity
{
    /// <summary>
    /// 操作日志表
    /// 存储系统操作日志，用于记录用户的操作行为。
    /// </summary>
    public class OperationLog : BaseEntity
    {
        /// <summary>
        /// 关联的用户ID
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperationDate { get; set; }
    }
}
