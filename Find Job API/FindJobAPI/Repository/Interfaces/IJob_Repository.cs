using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IJob_Repository
    {
        Task<List<All>> GetAll();
        Task<CreateJob> CreateJob(string userId, CreateJob createJob);
        Task<List<AllJob>> AllJobPost(int pageNumber, int pageSize);
        Task<List<AllJob>> AllJobWait(int pageNumber, int pageSize);
        Task<List<AllJob>> AllJobTimeOut(int pageNumber, int pageSize);
        Task<JobDetail> JobDetail(int jobId);
        Task<List<ListJob>> AllJob(string userId, int pageNumber, int pageSize);
        Task<List<ListJob>> JobPostList (string userId, int pageNumber, int pageSize);
        Task<List<ListJob>> JobWaitList(string userId, int pageNumber, int pageSize);
        Task<List<ListJob>> JobTimeoutList(string userId, int pageNumber, int pageSize);
        Task<(int ,List<ListJob>)> Search(Search search, int pageNumber, int pageSize);
        Task<UpdateJob> Update(int job_id, UpdateJob updateJob);
        Task<job> Delete(int jobId);
        Task<List<ApplyList>> ApplyList(int job_id, int pageNumber, int pageSize);
        Task<List<ApplyList>> Receive(int job_id, int pageNumber, int pageSize);
        Task<job> Status(int job_id);
        Task<int> CountJob();
        Task<(int ,List<ListJob>)> FindJob(int pageNumber, int pageSize);
    }
}