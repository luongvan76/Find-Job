using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FindJobAPI.Repository.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitmentNoAccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRecruitmentNoAccount_Repository recruitmentNoAccount_Repository;
        public RecruitmentNoAccountController(AppDbContext context, IRecruitmentNoAccount_Repository repository)
        {
            _context = context;
            recruitmentNoAccount_Repository = repository;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post (Create create)
        {
            try
            {
                var recruitment = await recruitmentNoAccount_Repository.Post(create);
                if (recruitment == null) { return  NotFound("Không tìm thấy công việc cần đăng kí"); }
                return Ok("Đăng kí thành công");
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult>Get (int id)
        {
            try
            {
                var get = await recruitmentNoAccount_Repository.Get(id);
                if (get == null) { return NotFound("Không tìm thấy người dùng"); }
                return Ok(get);
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delete = await recruitmentNoAccount_Repository.Delete(id);
                if (delete == null) { return NotFound("Không tìm thấy người dùng"); }
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Xóa thất bại"); }
        }

        [HttpPut("Status")]
        [Authorize]
        public async Task<IActionResult> Status(int id, int job_id)
        {
            try
            {
                var updateStatus = await recruitmentNoAccount_Repository.Status(id, job_id);
                if (updateStatus == null) { return NotFound("Không tìm thấy công việc hoặc người dùng đã ứng tuyển"); }
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

    }
}
