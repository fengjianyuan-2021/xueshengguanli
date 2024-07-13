using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统模型.Enity
{
    /// <summary>
    /// 公告阅读状态表
    /// 用于存储用户阅读公告的状态信息。
    /// </summary>
    public class AnnouncementReadStatus : BaseEntity
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public int AnnouncementId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        
        /// <summary>
        /// 关联的用户对象
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 关联的公告对象
        /// </summary>
        public virtual Announcement Announcement { get; set; }
    }

}
