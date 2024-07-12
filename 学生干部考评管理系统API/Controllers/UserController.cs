using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.RegularExpressions;
using 学生干部考评管理系统API.Interface;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.StudentCadreEvaluation.Models;
using AutoMapper;
using 学生干部考评管理系统API.Helper;
using 学生干部考评管理系统模型.Enum;

namespace 学生干部考评管理系统API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public class SelectUserRoleRequest
        {
            public int SelectUserRole { get; set; }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        [HttpPost("GetUsersInRole")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromBody] SelectUserRoleRequest request)
        {
            UserRole userRole = (UserRole)request.SelectUserRole;
            var users = await _userService.GetAllUsersAsync(userRole);
            return Ok(users);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">用户ID</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id">用户ID</param>
        [HttpGet("GetUserAndEvaluation/{id}")]
        public async Task<ActionResult<UserDto>> GetUserAndEvaluation(string id)
        {
            var user = await _userService.GetUserAndEvaluationAsync(int.Parse(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="user">用户对象</param>
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser([FromForm] CreateUserDto user, IFormFile? avatarFile)
        {
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "user_images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatarFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(fileStream);
                }

                user.AvatarPath = $"/user_images/{uniqueFileName}";
            }
            var result = await _userService.CreateUserAsync(user);

            if (result != null)
            {
                return Ok(result);
            }
            else 
            {
                return BadRequest("创建用户失败");
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="user">用户对象</param>
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UserDto user, IFormFile? avatarFile)
        {
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "user_images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatarFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(fileStream);
                }

                user.AvatarPath = $"/user_images/{uniqueFileName}";
            }

            var userEntity = _mapper.Map<User>(user);
            var existingUser = await _userService.GetUserByIdAsync(userEntity.Id);
            if (existingUser == null)
            {
                return NotFound("没有找到该用户");
            }

            var updateUser =  await _userService.UpdateUserAsync(user);

            var userDto = _mapper.Map<UserDto>(updateUser);
            // 读取头像文件并转换为Base64编码
            if (!string.IsNullOrEmpty(updateUser.AvatarPath))
            {
                string avatarFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", updateUser.AvatarPath.TrimStart('/'));
                userDto.Avatar = CodeHelper.ConvertImageToBase64(avatarFullPath);
            }
            return Ok(new { user = userDto });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            bool result= await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return BadRequest("删除失败或者没有找到删除的用户");
            }
            else 
            {
                return Ok("删除成功");
            }
            
        }

      
    }
}
