using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 学生干部考评管理系统模型.Enity;
using 学生干部考评管理系统模型.Enum;

namespace 学生干部考评管理系统模型.DTO
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    public class UserDto 
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? Fullname { get; set; } = string.Empty;

        /// <summary>
        /// 密码验证
        /// </summary>
        public string? PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 学时
        /// </summary>
        public float? ClassHour { get; set; } = 0;

        /// <summary>
        /// 性别
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// 用户的电子邮箱
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole? Role { get; set; }

        /// <summary>
        /// 头像文件路径
        /// </summary>
        public string? AvatarPath { get; set; }=string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string? Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 学生干部在学生组织中的职位。
        /// </summary>
        public string? Position { get; set; } = string.Empty;

        /// <summary>
        /// 学生干部所属的部门或组织。
        /// </summary>
        public string? Organization { get; set; } = string.Empty;

        /// <summary>
        /// 评分(自我评价)
        /// </summary>
        public string? SelfScore { get; set; } = string.Empty;

        /// <summary>
        /// 评价者分数
        /// </summary>
        public string? Evaluatorsorce {  get; set; }

        /// <summary>
        /// 评价者ID
        /// </summary>
        public string? EvaluatorId { get; set; }

        /// <summary>
        /// 互评平均分
        /// </summary>
        public string? PeerAverageScore { get; set; } =string.Empty;

        /// <summary>
        /// 教师评分
        /// </summary>
        public string? TeacherAverageScore { get; set; } = string.Empty;

    }

}
