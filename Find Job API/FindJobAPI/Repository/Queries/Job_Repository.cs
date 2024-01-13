using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace FindJobAPI.Repository.Queries
{
    public class Job_Repository : IJob_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth _firebaseAuth;

        public Job_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<List<All>> GetAll()
        {
            var allJobDomain = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJobDomain
                .Skip(0)
                .Take(10)
                .Select(job => new All()
                {
                    id = job.job_id,
                    employer_name = job.employer!.employer_name,
                    title = job.job_title,
                    posted_date = job.posted_date.ToString("dd-MM-yyyy"),
                    status = job.status ? "Approved" : "Waiting"
                }).ToListAsync(); 
            return listJob;
        }

        public async Task<List<AllJob>> AllJobPost(int pageNumber, int pageSize)
        {
            var allJobDomain =  _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJobDomain
                .Where(job => job.status == true && job.deadline >= DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new AllJob(){
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<AllJob>> AllJobWait(int pageNumber, int pageSize)
        {
            var allJobDomain = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJobDomain
                .Where(job => job.status == false && job.deadline >= DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new AllJob()
            {
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<AllJob>> AllJobTimeOut(int pageNumber, int pageSize)
        {
            var allJobDomain = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJobDomain
                .Where(job => job.deadline.Date < DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new AllJob()
            {
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<JobDetail> JobDetail(int jobId)
        {
            var jobDomain = _appDbContext.Job.AsQueryable();
            var jobDetail =  await jobDomain.Where(j => j.job_id == jobId).Select(job => new JobDetail()
            {
                jobTitle = job.job_title,
                jobDescription = job.job_description,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                requirement = job.requirement,
                location = job.location,
                deadline = job.deadline.ToString("dd-MM-yyyy"),
                industry = job.industry,
                type = job.type,
                posted_date = job.posted_date.ToString("dd-MM-yyyy"),
                employer_name = job.employer!.employer_name,
                email = job.employer!.email,
                website = job.employer!.employer_website,
                contact = job.employer!.contact_phone,
                address = job.employer!.employer_address,
                logo = job.employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                status = job.status ? "Approved" : "Waiting"
            }).FirstOrDefaultAsync();
            if (jobDetail == null) { return null!; }
            return jobDetail;
        }

        public async Task<job> Status(int job_id)
        {
            var jobDomain = await _appDbContext.Job.FindAsync(job_id);
            if (jobDomain == null) { return null!; }
            jobDomain.status = true;
            await _appDbContext.SaveChangesAsync();
            return jobDomain;
        }

        public async Task<List<ListJob>> AllJob(string userId, int pageNumber, int pageSize)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employer == null) { throw new Exception(message: "Không tìm thấy nhà tuyển dụng"); }
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j => j.posted_date);
            var listJob = await allJob
                 .Where(j => j.UID == userId && j.deadline >= DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new ListJob()
                {
                    id = job.job_id,
                    jobTitle = job.job_title,
                    minimum_salary = job.minimum_salary,
                    maximum_salary = job.maximum_salary,
                    location = job.location,
                    industry = job.industry!.industry_name,
                    type = job.type!.type_name,
                    logo = employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                    deadline = job.deadline.ToString("dd-MM-yyyy"),
                    status = job.status ? "Approved" : "Waiting"  
                }).ToListAsync();
            return listJob;
        }

        public async Task<List<ListJob>> JobPostList(string userId, int pageNumber, int pageSize)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employer == null) throw new Exception(message:"Không tìm thấy nhà tuyển dụng");
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJob
                .Where(j => j.status == true && j.UID == userId && j.deadline >= DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new ListJob()
            {
                id = job.job_id,
                jobTitle = job.job_title,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                location = job.location,
                industry = job.industry!.industry_name,
                type = job.type!.type_name,
                logo = employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                deadline = job.deadline.ToString("dd-MM-yyyy"),
                status = job.status ? "Approved" : "Waiting"
                }).ToListAsync();
            return listJob;
        }

        public async Task<List<ListJob>> JobWaitList(string userId, int pageNumber, int pageSize)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employer == null) throw new Exception(message:"Không tìm thấy nhà tuyển dụng");
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJob
                .Where(j => j.status == false && j.UID == userId && j.deadline >= DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new ListJob()
            {
                id = job.job_id,
                jobTitle = job.job_title,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                location = job.location,
                industry = job.industry!.industry_name,
                type = job.type!.type_name,
                logo = employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                deadline = job.deadline.ToString("dd-MM-yyyy"),
                status = job.status ? "Approved" : "Waiting"
                }).ToListAsync();
            return listJob;
        }

        public async Task<List<ListJob>> JobTimeoutList(string userId, int pageNumber, int pageSize)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employer == null) throw new Exception(message:"Không tìm thấy nhà tuyển dụng");
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j=>j.posted_date);
            var listJob = await allJob
                .Where(j => j.status == false && j.UID == userId && j.deadline < DateTime.Now.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(job => new ListJob()
            {
                id = job.job_id,
                jobTitle = job.job_title,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                location = job.location,
                industry = job.industry!.industry_name,
                type = job.type!.type_name,
                logo = employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                deadline = job.deadline.ToString("dd-MM-yyyy"),
                status = job.status ? "Approved" : "Waiting"
                }).ToListAsync();
            return listJob;
        }

        public async Task<List<ApplyList>> ApplyList(int job_id, int pageNumber, int pageSize)
        {
            var recruitmentAccount = _appDbContext.Recruitment.AsQueryable().OrderByDescending(r=>r.registation_date);
            var recruimentNoAccount = _appDbContext.Recruitment_No_Accounts.AsQueryable();
            var accountRecruitment = await recruitmentAccount.Where(r => r.job_id == job_id && r.status == false)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.UID,
                    Name = recruitment.seeker!.Name,
                    Email = recruitment.seeker.Email,
                    PhoneNumber = recruitment.seeker.PhoneNumber
                }).ToListAsync() ;
            foreach (var apply in accountRecruitment)
            {
                if (apply.Name == null || apply.Email == null || apply.PhoneNumber == null)
                {
                    var accountFB = await GetUserDataFromFirebase(apply.UID!);

                    if (apply.Name == null)
                        apply.Name = accountFB?.DisplayName;

                    if (apply.Email == null)
                        apply.Email = accountFB?.Email;

                    if (apply.PhoneNumber == null)
                        apply.PhoneNumber = accountFB?.PhoneNumber;
                }
            }
            var noAccountRecruitment = await recruimentNoAccount.Where(r => r.job_id == job_id && r.status == false)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.recruitment_ID.ToString(),
                    Name = recruitment.fullname,
                    Email = recruitment.email,
                    PhoneNumber = recruitment.phone_number
                }).ToListAsync();
           var listRecruitment = accountRecruitment.Concat(noAccountRecruitment).ToList(); ;
            return listRecruitment.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        private async Task<UserRecord> GetUserDataFromFirebase(string uid)
        {
            try
            {
                var userRecord = await _firebaseAuth.GetUserAsync(uid);
                return userRecord;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<List<ApplyList>> Receive(int job_id, int pageNumber, int pageSize)
        {
            var recruitmentAccount = _appDbContext.Recruitment.AsQueryable().OrderByDescending(r=>r.registation_date);
            var recruimentNoAccount = _appDbContext.Recruitment_No_Accounts.AsQueryable();
            var accountRecruitment = await recruitmentAccount.Where(r => r.job_id == job_id && r.status == true)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.UID,
                    Name = recruitment.seeker!.Name,
                    Email = recruitment.seeker.Email,
                    PhoneNumber = recruitment.seeker.PhoneNumber
                }).ToListAsync();
            foreach (var apply in accountRecruitment)
            {
                if (apply.Name == null || apply.Email == null || apply.PhoneNumber == null)
                {
                    var accountFB = await GetUserDataFromFirebase(apply.UID!);

                    if (apply.Name == null)
                        apply.Name = accountFB?.DisplayName;

                    if (apply.Email == null)
                        apply.Email = accountFB?.Email;

                    if (apply.PhoneNumber == null)
                        apply.PhoneNumber = accountFB?.PhoneNumber;
                }
            }
            var noAccountRecruitment = await recruimentNoAccount.Where(r => r.job_id == job_id && r.status == true)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.recruitment_ID.ToString(),
                    Name = recruitment.fullname,
                    Email = recruitment.email,
                    PhoneNumber = recruitment.phone_number
                }).ToListAsync();
            var listRecruitment = accountRecruitment.Concat(noAccountRecruitment).ToList();
            return listRecruitment.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }

        public async Task<CreateJob> CreateJob(string userId, CreateJob createJob)
        {
            var employerDomain = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employerDomain == null) { return null!; }
            var Job = new job()
            {
                UID = userId,
                job_title = createJob.JobTitle,
                minimum_salary = createJob.Minimum_Salary,
                maximum_salary = createJob.Maximum_Salary,
                location = createJob.Location,
                industry_id = createJob.Industry_id,
                type_id = createJob.Type_id,
                deadline = DateTime.Parse(createJob.Deadline!),
                posted_date = DateTime.Now.Date,
                status = false
            };
            if(!string.IsNullOrEmpty(createJob.JobDescription)) 
                Job.job_description = createJob.JobDescription;
            if (!string.IsNullOrEmpty(createJob.Requirement))
                Job.requirement = createJob.Requirement;
            _appDbContext.Job.Add(Job);
            await _appDbContext.SaveChangesAsync();
            return createJob;
        }

        public async Task<(int, List<ListJob>)> Search(Search search, int pageNumber, int pageSize)
        {
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j => j.posted_date);

            var query = allJob.AsQueryable();
            query = query.Where(j => j.status == true && j.deadline >= DateTime.Now.Date);

            if (search?.industry_id != null && search.industry_id != 0)
            {
                query = query.Where(j => j.industry_id == search.industry_id);
            }

            if (search?.type_id != null && search.type_id != 0)
            {
                query = query.Where(j => j.type_id == search.type_id);
            }

            if(search?.salary != null && search.salary != 0)
            {
                query = query.Where(j => j.minimum_salary <= search.salary && search.salary <= j.maximum_salary);
            }

            if (!string.IsNullOrEmpty(search?.location))
            {
                query = query.Where(j => j.location == search.location);
            }

            var searchJob = await query
                .Select(job => new ListJob()
                {
                    id = job.job_id,
                    jobTitle = job.job_title,
                    minimum_salary = job.minimum_salary,
                    maximum_salary = job.maximum_salary,
                    location = job.location,
                    industry = job.industry!.industry_name,
                    type = job.type!.type_name,
                    logo = job.employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                    deadline = job.deadline.ToString("dd-MM-yyyy")
                })
                .ToListAsync();
            int jobQuantity = searchJob.Count;
            return (jobQuantity, searchJob.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<UpdateJob> Update(int job_id, UpdateJob updateJob)
        {
            var jobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == job_id);
            if (jobDomain == null) { return null!; }
            if (!string.IsNullOrEmpty(updateJob.JobTitle)) 
                jobDomain.job_title = updateJob.JobTitle;
            if(!string.IsNullOrEmpty(updateJob.JobDescription))
                jobDomain.job_description = updateJob.JobDescription;
            if(updateJob.Minimum_Salary != 0 && updateJob.Minimum_Salary != jobDomain.minimum_salary)
                jobDomain.minimum_salary = updateJob.Minimum_Salary;
            if (updateJob.Maximum_Salary != 0 && updateJob.Maximum_Salary != jobDomain.maximum_salary)
                jobDomain.maximum_salary = updateJob.Maximum_Salary;
            if (!string.IsNullOrEmpty(updateJob.Requirement))
                jobDomain.requirement = updateJob.Requirement;
            if(!string.IsNullOrEmpty(updateJob.Location))
                jobDomain.location = updateJob.Location;
            if(updateJob.Industry_id != 0)
                jobDomain.industry_id = updateJob.Industry_id;
            if(updateJob.Type_id != 0)
                jobDomain.type_id = updateJob.Type_id;
            if (!string.IsNullOrEmpty(updateJob.Deadline) && 
                (DateTime.Parse(updateJob.Deadline) != jobDomain.deadline && DateTime.Parse(updateJob.Deadline) != DateTime.Now.Date))
                    jobDomain.deadline = DateTime.Parse(updateJob.Deadline);
            await _appDbContext.SaveChangesAsync();
            return updateJob;
        }

        public async Task<job> Delete(int jobId)
        {
            var job = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == jobId);
            if (job != null)
            {
                _appDbContext.Job.Remove(job);
                await _appDbContext.SaveChangesAsync();
                return job;
            }
            return null!;
        }

        public async Task<int> CountJob()
        {
            return await _appDbContext.Job.CountAsync(j=>j.status==true);
        }

        public async Task<(int, List<ListJob>)> FindJob(int pageNumber, int pageSize)
        {
            var allJob = _appDbContext.Job.AsQueryable().OrderByDescending(j => j.posted_date);
            var searchJob = await allJob
                .Where(j => j.status == true && j.deadline >= DateTime.Now.Date)
                .Select(job => new ListJob()
                {
                    id = job.job_id,
                    jobTitle = job.job_title,
                    minimum_salary = job.minimum_salary,
                    maximum_salary = job.maximum_salary,
                    location = job.location,
                    industry = job.industry!.industry_name,
                    type = job.type!.type_name,
                    logo = job.employer!.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                    deadline = job.deadline.ToString("dd-MM-yyyy"),
                }).ToListAsync();
            int jobQuantity = searchJob.Count;
            return (jobQuantity,searchJob.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }
    }
}
