namespace 学生干部考评管理系统模型
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using 学生干部考评管理系统模型.Enity;

    namespace StudentCadreEvaluation.Models
    {
        public enum UserRole
        {
            Admin,        // 管理员
            Teacher,      // 教师
            StudentCadre  // 学生干部
        }

        public class User:BaseEnity
        {
            /// <summary>
            /// 用户账号
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string Fullname { get; set; }

            /// <summary>
            /// 用户密码的哈希值
            /// </summary>
            public string PasswordHash { get; set; }

            /// <summary>
            /// 用户角色
            /// </summary>
            public UserRole Role { get; set; }

            /// <summary>
            /// 头像
            /// </summary>
            public byte[] Avatar { get; set; } = new byte[0];

        }
    }

}
