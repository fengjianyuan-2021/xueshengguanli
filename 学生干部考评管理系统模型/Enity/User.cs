namespace 学生干部考评管理系统模型
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using 学生干部考评管理系统模型.Enity;
    using 学生干部考评管理系统模型.Enum;

    namespace StudentCadreEvaluation.Models
    {

        public class User:BaseEntity
        {
            /// <summary>
            /// 用户账号
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string Fullname { get; set; } = string.Empty;

            /// <summary>
            /// 性别
            /// </summary>
            public Gender? Gender { get; set; }

            /// <summary>
            /// 用户的电子邮箱
            /// </summary>
            public string? Email { get; set; } = string.Empty;

            /// <summary>
            /// 用户密码的哈希值
            /// </summary>
            public string PasswordHash { get; set; } = string.Empty;

            /// <summary>
            /// 用户角色
            /// </summary>
            public UserRole Role { get; set; }

            /// <summary>
            /// 头像文件路径
            /// </summary>
            public string AvatarPath { get; set; } = string.Empty ;


            /// <summary>
            /// 学生干部在学生组织中的职位。
            /// </summary>
            public string Position { get; set; } = string.Empty;

            /// <summary>
            /// 学生干部所属的部门或组织。
            /// </summary>
            public string Organization { get; set; } = string.Empty;

            /// <summary>
            /// 总分
            /// </summary>
            public float? TotalScore { get; set; }

            [NotMapped]
            /// <summary>
            /// 自我评价表
            /// </summary>
            public virtual ICollection<Evaluation>? SelfEvaluations { get; set; }

            [NotMapped]
            /// <summary>
            /// 其他人评价表
            /// </summary>
            public virtual ICollection<Evaluation>? OtherEvaluations { get; set; }
        }
    }

}
