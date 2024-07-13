using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统数据库映射;
using 学生干部考评管理系统模型.DTO;
using 学生干部考评管理系统模型.Enity;

namespace 学生干部考评管理系统API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public AnnouncementsController(AppDbContext context)
        {
            _appContext = context;
        }   

        /// <summary>
        /// 读取公告
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements(int userId)
        {
            var announcements = await _appContext.Announcements
                .Include(a => a.ReadStatuses)
                .ToListAsync();

            var announcementDtos = announcements.Select(a => new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                IsReadByCurrentUser = a.ReadStatuses.Any(rs => rs.UserId == userId)
            }).ToList();

            return Ok(announcementDtos);
        }


        /// <summary>
        /// 创建公告
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Announcement>> CreateAnnouncement(Announcement announcement)
        {
            _appContext.Announcements.Add(announcement);
            await _appContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAnnouncements), new { id = announcement.Id }, announcement);
        }

        /// <summary>
        /// 查询公告
        /// </summary>
        /// <param name="id"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }

            _appContext.Entry(announcement).State = EntityState.Modified;
            await _appContext.SaveChangesAsync();
            return NoContent();
        }

        public class MarkAsReadParmart() 
        {
            public int userId { get; set; }

            public int announcementId { get; set; }
        }

        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("mark-as-read/{announcementId}")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadParmart markAsReadParmart)
        {
            try
            {
                var announcementReadStatus = await _appContext.AnnouncementReadStatuses
                .FirstOrDefaultAsync(ars => ars.AnnouncementId == markAsReadParmart.announcementId && ars.UserId == markAsReadParmart.userId);

                if (announcementReadStatus == null)
                {
                    announcementReadStatus = new AnnouncementReadStatus
                    {
                        AnnouncementId = markAsReadParmart.announcementId,
                        UserId = markAsReadParmart.userId
                    };
                    _appContext.AnnouncementReadStatuses.Add(announcementReadStatus);
                    await _appContext.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// 读取当前用户未读的公告
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("unread")]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetUnreadAnnouncements(int userId)
        {
            var announcements = await _appContext.Announcements
                .Include(a => a.ReadStatuses)
                .Where(a => !a.ReadStatuses.Any(rs => rs.UserId == userId))
                .ToListAsync();

            var announcementDtos = announcements.Select(a => new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                IsReadByCurrentUser = false
            }).ToList();

            return Ok(announcementDtos);
        }

    }

}
