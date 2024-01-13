using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IRecruitment_Repository
    {
        Task<recruitment> Post(string userId, int job_id);
        Task<recruitment> Delete(string userId, int job_id);
        Task<(int, List<Seeker>)> Seeker(string userId, int pageNumber, int pageSize);
        Task<recruitment> Status(string userId, int job_id);
    }
}
