using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 学生干部考评管理系统模型.Enum
{
    /// <summary>
    /// 表示评价类型的枚举。
    /// </summary>
    public enum EvaluationType
    {
        /// <summary>
        /// 自评
        /// </summary>
        [Description("自评")]
        Self,

        /// <summary>
        /// 互评
        /// </summary>
        [Description("互评")]
        Peer,

        /// <summary>
        /// 教师评价
        /// </summary>
        [Description("教师评价")]
        Teacher
    }
}
