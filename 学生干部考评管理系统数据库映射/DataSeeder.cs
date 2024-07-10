using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统数据库映射
{
    public static class DataSeeder
    {
        public static void SeedInitialUser(AppDbContext context, ILogger logger)
        {
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var user = new User
                {
                    Username = "admin",
                    PasswordHash = CreatePasswordHash("123"),
                    Role = UserRole.Admin,
                    Fullname = "管理员",
                    CreateOn = DateTime.UtcNow,
                    UpdateOn = DateTime.UtcNow
                };

                context.Users.Add(user);
                context.SaveChanges();

                logger.LogInformation("Initial admin user created.");
            }
        }

        private static string CreatePasswordHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                for (var i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
