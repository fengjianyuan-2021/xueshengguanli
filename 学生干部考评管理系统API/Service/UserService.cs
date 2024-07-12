using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using 学生干部考评管理系统API.Helper;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统数据库映射;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enity;
using 学生干部考评管理系统模型.Enum;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;

namespace 学生干部考评管理系统API.Service
{
    /// <summary>
    /// 用户服务实现类，实现用户服务接口
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据权限获取所有用户
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(UserRole userRole)
        {
            List<UserDto> userDtos = new List<UserDto>();
            if (userRole != UserRole.Admin)
            {
                var list = await _context.Users.Where(r => r.IsDeleted == false && r.Role==UserRole.StudentCadre).ToListAsync();
                userDtos = _mapper.Map<List<UserDto>>(list);
            }
            else 
            {
                var list = await _context.Users.Where(r => r.IsDeleted == false).ToListAsync();
                userDtos = _mapper.Map<List<UserDto>>(list);
            }
           

            foreach (var userDto in userDtos) 
            {
                if (!string.IsNullOrEmpty(userDto.AvatarPath)) 
                {
                    string avatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userDto.AvatarPath.TrimStart('/'));
                    userDto.Avatar = CodeHelper.ConvertImageToBase64(avatarFullPath);
                }
                
            }
            return userDtos;
        }

        /// <summary>
        /// 通过ID获取用户
        /// </summary>
        /// <param name="id">用户ID</param>
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            UserDto userDto = _mapper.Map<UserDto>(user);
            if (!string.IsNullOrEmpty(userDto.AvatarPath))
            {
                string avatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userDto.AvatarPath.TrimStart('/'));
                userDto.Avatar = CodeHelper.ConvertImageToBase64(avatarFullPath);
            }
            return userDto;
        }

        /// <summary>
        /// 通过ID获取用户评价
        /// </summary>
        /// <param name="id">用户ID</param>
        public async Task<UserDto> GetUserAndEvaluationAsync(int id)
        {
            var user = await _context.Users.Include(r=>r.OtherEvaluations).Include(r=>r.SelfEvaluations).FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            if (user == null) 
            {
                return null;
            }
            UserDto userDto = _mapper.Map<UserDto>(user);
            if (!string.IsNullOrEmpty(userDto.AvatarPath))
            {
                string avatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userDto.AvatarPath.TrimStart('/'));
                userDto.Avatar = CodeHelper.ConvertImageToBase64(avatarFullPath);
            }
            //查询学生评价平均分
            if (user.OtherEvaluations!=null && user.OtherEvaluations.Any()) 
            {
                var peerEvaluations = user.OtherEvaluations.Where(r => r.EvaluationType == EvaluationType.Peer);
                userDto.PeerAverageScore = peerEvaluations.Any() ? peerEvaluations.Average(e => e.Score).ToString() ?? "0" : "0";
            }

            //查询教师评价平均分
            if (user.OtherEvaluations != null && user.OtherEvaluations.Any())
            {
                var teacherAverageScore = user.OtherEvaluations.Where(r => r.EvaluationType == EvaluationType.Teacher);
                userDto.PeerAverageScore = teacherAverageScore.Any() ? teacherAverageScore.Average(e => e.Score).ToString() ?? "0" : "0";
            }

            //查询自己评分 只查最新的
            if (user.SelfEvaluations != null && user.SelfEvaluations.Any())
            {
                var latestSelfEvaluation = user.SelfEvaluations .Where(e => e.EvaluationType == EvaluationType.Self).OrderByDescending(e => e.UpdateOn).ThenByDescending(e => e.CreateOn).FirstOrDefault();
                userDto.SelfScore = latestSelfEvaluation?.Score.ToString() ?? "0";
            }

            return userDto;
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="user">用户对象</param>
        public async Task<UserDto?> CreateUserAsync(CreateUserDto user)
        {
            try
            {
                User createuser = _mapper.Map<User>(user);

                createuser.PasswordHash = CodeHelper.CreatePasswordHash(createuser.PasswordHash);
                _context.Users.Add(createuser);
                await _context.SaveChangesAsync();
                var creatuser = await _context.Users.FirstOrDefaultAsync(r => r.Username == createuser.Username && !r.IsDeleted);
                UserDto userDto = _mapper.Map<UserDto>(creatuser);
                if (!string.IsNullOrEmpty(userDto.AvatarPath))
                {
                    string avatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", userDto.AvatarPath.TrimStart('/'));
                    userDto.Avatar = CodeHelper.ConvertImageToBase64(avatarFullPath);
                }
                return userDto;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户对象</param>
        public async Task<User> UpdateUserAsync(UserDto user)
        {
            var existingUser = await _context.Users.FindAsync(int.Parse(user.Id));

            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            //// 手动更新字段，只有在字段不为空时才更新
            //if (!string.IsNullOrEmpty(user.Username))
            //{
            //    existingUser.Username = user.Username;
            //}

            if (!string.IsNullOrEmpty(user.Fullname))
            {
                existingUser.Fullname = user.Fullname;
            }

            if (user.Gender != null)
            {
                existingUser.Gender = user.Gender;
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                existingUser.Email = user.Email;
            }

            if (!string.IsNullOrEmpty(user.Position))
            {
                existingUser.Position = user.Position;
            }

            if (!string.IsNullOrEmpty(user.Organization))
            {
                existingUser.Organization = user.Organization;
            }

            if (!string.IsNullOrEmpty(user.AvatarPath))
            {
                existingUser.AvatarPath = user.AvatarPath;
            }

            if (user.TotalScore != null)
            {
                existingUser.TotalScore = user.TotalScore;
            }

            // 如果有密码更新，才更新密码
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                existingUser.PasswordHash = user.PasswordHash;
            }

            await _context.SaveChangesAsync();

            //当传递的数据有评分时 追加评分表
            if (!string.IsNullOrEmpty(user.SelfScore) ) 
            {
                Evaluation evaluation = new()
                {
                    Score = float.Parse(user.SelfScore),
                    EvaluatorId = existingUser.Id,
                    StudentCadreId = existingUser.Id,
                    EvaluationDate = DateTime.UtcNow,
                    EvaluationType = EvaluationType.Self
                };
                _context.Evaluations.Add(evaluation);
                await _context.SaveChangesAsync();
            }
            if (!string.IsNullOrEmpty(user.Evaluatorsorce) && !string.IsNullOrEmpty(user.EvaluatorId) && user.Evaluatorsorce!= "null" && user.EvaluatorId != "null") 
            {
                string scoreString = user.Evaluatorsorce;
                Evaluation evaluation = new()
                {
                    EvaluatorId = int.Parse( user.EvaluatorId),
                    Score = float.Parse(user.Evaluatorsorce),
                    StudentCadreId = existingUser.Id,
                    EvaluationDate = DateTime.UtcNow,
                    EvaluationType = EvaluationType.Peer
                };
                _context.Evaluations.Add(evaluation);
                await _context.SaveChangesAsync();
            }

            return existingUser;
        }

        /// <summary>
        /// 删除用户（软删除）
        /// </summary>
        /// <param name="id">用户ID</param>
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    user.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else { return false; }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
           
        }
    }
}
