using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FirebaseAdmin.Messaging;
using System;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJob_Repository _jobRepository;

        public JobController(AppDbContext appDbContext, IJob_Repository jobRepository)
        {
            _appDbContext = appDbContext;
            _jobRepository = jobRepository;
        }

        [HttpGet("GetAll")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listJob = await _jobRepository.GetAll();
                return Ok(listJob);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpGet("AllJobPost")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobPost(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobPost(pageNumber, pageSize);
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobWait")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobWait(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobWait(pageNumber, pageSize);
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobTimeout")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobTimeOut(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobTimeOut(pageNumber, pageSize);
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("JobDetail")]
        public async Task<IActionResult> JobDetail(int jobId)
        {
            try
            {
                var jobDetail = await _jobRepository.JobDetail(jobId);
                if (jobDetail == null) return BadRequest("Không tìm thấy công việc");
                return Ok(jobDetail);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Status")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> Status (int jobId)
        {
            try
            {
                var updateStatus = await _jobRepository.Status(jobId);
                if (updateStatus == null) return BadRequest("Không tìm thấy công việc");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpGet("AllJob")]
        [Authorize]
        public async Task<IActionResult> AllJob(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var allJob = await _jobRepository.AllJob(userId!, pageNumber, pageSize);
                return Ok(allJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet ("JobPostList")]
        [Authorize]
        public async Task<IActionResult> JobPostList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobPostList = await _jobRepository.JobPostList(userId!, pageNumber, pageSize);
                return Ok(jobPostList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("JobWaitList")]
        [Authorize]
        public async Task<IActionResult> JobWaitList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobWaitList = await _jobRepository.JobWaitList(userId!, pageNumber, pageSize);
                return Ok(jobWaitList);
            }
            catch { return BadRequest(); }

        }

        [HttpGet("JobTimeoutList")]
        [Authorize]
        public async Task<IActionResult> JobTimeoutList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobTimeoutList = await _jobRepository.JobTimeoutList(userId!, pageNumber, pageSize);
                return Ok(jobTimeoutList);
            }
            catch { return BadRequest(); }

        }


        [HttpGet("ApplyList")]
        [Authorize]
        public async Task<IActionResult> ApplyList(int job_id, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var ApplyList = await _jobRepository.ApplyList(job_id, pageNumber, pageSize);
                return Ok(ApplyList);
            }
            catch { return BadRequest() ;}
        }

        [HttpGet("Receive")]
        [Authorize]
        public async Task<IActionResult> Receive(int job_id, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var listReceive = await _jobRepository.Receive(job_id, pageNumber, pageSize);
                return Ok(listReceive);
            }
            catch { return BadRequest(); }
        }


        [HttpPost("Post")]
        [Authorize]
        public async Task<IActionResult> Post (CreateJob createJob)
        {
            try
            {
                var industry = await _appDbContext.Industry.FindAsync(createJob.Industry_id);
                if (industry == null) return BadRequest("Không tìm thấy lĩnh vực này");
                var type = await _appDbContext.Type.FindAsync(createJob.Type_id);
                if (type == null) return BadRequest("Không tìm thấy loại công việc này");
                var userId = User.FindFirst("Id")?.Value;
                var infor = await _appDbContext.Employer.FirstOrDefaultAsync(i => i.UID == userId);
                if (infor!.employer_name == null || infor.email == null || infor.contact_phone == null || infor.employer_address == null)
                {
                    return BadRequest("Hãy cập nhật thông tin trước khi đăng tuyển công việc!");
                }

                var create = await _jobRepository.CreateJob(userId!, createJob);
                if (create == null) { return BadRequest("Không tìm thấy tài khoản nhà tuyển dụng"); }
                return Ok(create);
            }
            catch { return BadRequest(); }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(Search search, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var searchJob = await _jobRepository.Search(search, pageNumber, pageSize);
                var response = new
                {
                    jobQuantity = searchJob.Item1,
                    jobList = searchJob.Item2
                };
                return Ok(response);
            }
            catch { return BadRequest() ; }
        }

        [HttpPut ("Update")]
        [Authorize]
        public async Task<IActionResult> Update(int job_id, UpdateJob updateJob)
        {
            try
            {
                if(!string.IsNullOrEmpty(updateJob.Location) || updateJob.Type_id == 0 || updateJob.Industry_id == 0) 
                {
                    var jobUpdate = await _jobRepository.Update(job_id, updateJob);
                    if (jobUpdate == null) { return BadRequest("Không tìm thấy công việc"); }
                    return Ok("Cập nhật thành công");

                }
                else
                {
                    var industry = await _appDbContext.Industry.FindAsync(updateJob.Industry_id);
                    if (industry == null) { return BadRequest("Không tìm thấy ngành công việc"); }
                    var type = await _appDbContext.Type.FindAsync(updateJob.Type_id);
                    if (type == null) { return BadRequest("Không tìm thấy loại công việc"); }
                    var jobUpdate = await _jobRepository.Update(job_id, updateJob);
                    if (jobUpdate == null) { return BadRequest("Không tìm thấy công việc"); }
                    return Ok("Cập nhật thành công");
                }
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int jobId)
        {
            try
            {
                var deleteJob = await _jobRepository.Delete(jobId);
                if (deleteJob == null) { return BadRequest("Không tìm thấy công việc cần xóa"); }
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Xóa thất bại"); }
        }

        [HttpGet("CountJob")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> CountJob()
        {
            try
            {
                var count = await _jobRepository.CountJob();
                return Ok(count);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("FindAJob")]
        public async Task<IActionResult> FindAJob(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var allJob = await _jobRepository.FindJob( pageNumber, pageSize);
                var response = new
                {
                    jobQuantity = allJob.Item1,
                    jobList = allJob.Item2
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
