using System.Security.Cryptography;
using System.Text;

namespace 学生干部考评管理系统API.Helper
{
    public static class CodeHelper
    {
        /// <summary>
        /// 将图像文件转换为Base64编码的字符串
        /// </summary>
        /// <param name="imagePath">图像文件路径</param>
        /// <returns>Base64编码的图像字符串</returns>
        public static string ConvertImageToBase64(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageArray);
            }
            return string.Empty;
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password)
        {
            // 使用适当的方法创建密码哈希
            // 这里仅作为示例，实际生产环境请使用更安全的哈希算法
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
