using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enity
{
    /// <summary>
    /// 公告表
    /// 存储系统公告信息。
    /// </summary>
    public class Announcement : BaseEntity
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 已读状态
        /// </summary>
        public virtual ICollection<AnnouncementReadStatus> ReadStatuses { get; set; } = new List<AnnouncementReadStatus>();
    }
}
