using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.DTO
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base64 编码的头像
        /// </summary>
        public string Avatar { get; set; }
    }

}
