using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecruitmentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IRecruitment_Repository recruitmentRepository;
        public RecruitmentController(AppDbContext appDbContext, IRecruitment_Repository recruitmentRepository)
        {
            this.appDbContext = appDbContext;
            this.recruitmentRepository = recruitmentRepository;
        }

        [HttpPost ("Post")]
        public async Task<IActionResult> Post (int job_id)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var cv = await appDbContext.Seeker.FirstOrDefaultAsync(s => s.UID == userId);
                if (cv!.Name == null || cv.Email == null || cv.PhoneNumber == null || cv.birthday == null || cv.address == null || cv.education == null || cv.major == null || cv.experience == null || cv.skills == null)
                {
                    return BadRequest ("Hãy cập nhật thông tin trước khi xin việc!");
                }
                var jobDomain = await appDbContext.Recruitment.FirstOrDefaultAsync( j=> j.job_id == job_id && j.UID == userId);
                if (jobDomain != null) { return Ok("Bạn đã đăng kí công việc này"); }
                var create = await recruitmentRepository.Post(userId!, job_id);
                if (create == null) { return NotFound("Không tìm thấy công việc này"); }
                return Ok("Đăng kí thành công");
            }
            catch { return BadRequest ("Đăng kí không thành công"); }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete (string userId, int job_id)
        {
            try
            {
                var delete = await recruitmentRepository.Delete(userId, job_id);
                if (delete == null) { return NotFound ("Không tìm thấy đăng kí công việc"); }
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpGet("Seeker")]
        public async Task<IActionResult> Seeker(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var listRecruitment = await recruitmentRepository.Seeker(userId!, pageNumber, pageSize);
                var respone = new
                {
                    countHistory = listRecruitment.Item1,
                    listHistory = listRecruitment.Item2
                };
                return Ok(respone);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut ("Status")]
        public async Task<IActionResult> Status (string userId, int job_id)
        {
            try
            {
                var updateStatus = await recruitmentRepository.Status(userId, job_id);
                if (updateStatus == null) { return NotFound("Không tìm thấy công việc hoặc người dùng đã ứng tuyển"); }
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }
    }
}
