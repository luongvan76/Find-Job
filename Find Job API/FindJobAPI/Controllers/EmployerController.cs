using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmployer_Repository _employer_Repository;
        public EmployerController(AppDbContext appDbContext, IEmployer_Repository employer_Repository)
        {
            _appDbContext = appDbContext;
            _employer_Repository = employer_Repository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var employer = await _employer_Repository.Get(userId!);
                if (employer == null) { return BadRequest("Không tìm thấy tài khoản"); }
                return Ok(employer);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Proflie(string userId)
        {
            try
            {
                var employer = await _employer_Repository.Get(userId);
                if (employer == null) { return BadRequest("Không tìm thấy tài khoản"); }
                return Ok(employer);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("Image")]
        public async Task<IActionResult> Image(Image image)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var Image = await _employer_Repository.Image(userId!, image);
                if (Image == null) { return BadRequest("Cập nhật thất bại"); }
                return Ok("Cập nhật thành công");
            }
            catch
            {
                return BadRequest("Cập nhật thất bại");

            }
        }

        [HttpPut("ImageCover")]
        public async Task<IActionResult> ImageCover(Cover cover)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var Image = await _employer_Repository.ImageCover(userId!, cover);
                if (Image == null) { return BadRequest("Cập nhật thất bại"); }
                return Ok("Cập nhật thành công");
            }
            catch
            {
                return BadRequest("Cập nhật thất bại");

            }
        }

        [HttpPut("Infor")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> Infor(InforEmployer infor)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var Infor = await _employer_Repository.Infor(userId!, infor);
                if (Infor == null) { return BadRequest("Cập nhật thất bại"); }
                return Ok("Cập nhật thành công");
            }
            catch
            {
                return BadRequest("Cập nhật thất bại");
            }
        }
    }
}
