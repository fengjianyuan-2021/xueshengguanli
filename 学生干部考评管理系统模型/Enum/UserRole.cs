using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enum
{
    public enum UserRole
    {
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin,
        /// <summary>
        /// 教师
        /// </summary>
        [Description("教师")]
        Teacher,
        /// <summary>
        /// 学生干部
        /// </summary>
        [Description("学生干部")]
        StudentCadre
    }
}
