using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.DTO
{
    /// <summary>
    /// 公告数据传输对象
    /// </summary>
    public class AnnouncementDto
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 读取该公告的用户ID列表
        /// </summary>
        public List<int> ReadByUserIds { get; set; } = new List<int>();

        /// <summary>
        /// 当前用户是否已读
        /// </summary>
        public bool IsReadByCurrentUser { get; set; }
    }


}
