using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using 学生干部考评管理系统数据库映射;

namespace 学生干部考评管理系统API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public EvaluationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPut("UpdateEvaluationScore")]
        public async Task<IActionResult> UpdateEvaluationScore([FromBody] UpdateEvaluationScoreDto dto)
        {
            var evaluation = await _appDbContext.Evaluations.FindAsync(dto.id);
            if (evaluation == null)
            {
                return NotFound("评价未找到");
            }

            evaluation.Score = dto.Score;
            evaluation.UpdateOn = DateTime.UtcNow;

            _appDbContext.Evaluations.Update(evaluation);
            await _appDbContext.SaveChangesAsync();

            return Ok("评分更新成功");
        }

        public class UpdateEvaluationScoreDto
        {
            public float Score { get; set; }

            public int id { get; set; }
        }
    }
}
