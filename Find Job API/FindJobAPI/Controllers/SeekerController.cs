using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeekerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISeeker_Repository seeker_Repository;
        public SeekerController(AppDbContext appDbContext, ISeeker_Repository seeker_repository)
        {
            _appDbContext = appDbContext;
            seeker_Repository = seeker_repository;
        }

        [HttpGet("CV")]
        public async Task<IActionResult> CV()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var seekerCV = await seeker_Repository.CV(userId!);
                if (seekerCV != null)
                {
                    return Ok(seekerCV);
                }
                else { return BadRequest("Không tìm thấy seeker"); }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateCV")]
        public async Task<IActionResult> CV(CV cV)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var updateCv = await seeker_Repository.CVUpdate(userId!, cV);
                if (updateCv != null)
                {
                    return Ok("Cập nhật thành công");
                }
                else { return BadRequest("Cập nhật thất bại"); }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Infor")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> Infor(string userId)
        {
            try
            {
                var seekerInfor = await seeker_Repository.Infor(userId);
                if (seekerInfor == null)
                    return BadRequest("Không tìm thấy tài khoản");
                return Ok(seekerInfor);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CVInfor")]
        public async Task<IActionResult> CVInfor(string userId)
        {
            try
            {
                var seekerCVInfor = await seeker_Repository.InforApply(userId);
                if (seekerCVInfor == null) return BadRequest("Không tìm thấy thông tin này");
                return Ok(seekerCVInfor);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
