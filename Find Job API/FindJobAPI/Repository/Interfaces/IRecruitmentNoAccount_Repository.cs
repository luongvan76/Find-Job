using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
namespace FindJobAPI.Repository.Interfaces
{
    public interface IRecruitmentNoAccount_Repository
    {
        Task<Create> Post (Create create);  
        Task<Get> Get (int id);
        Task<recruitment_no_account> Delete (int id);
        Task<recruitment_no_account> Status(int id, int job_id);

    }
}
