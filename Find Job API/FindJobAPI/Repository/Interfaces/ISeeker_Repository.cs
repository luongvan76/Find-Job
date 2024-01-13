using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface ISeeker_Repository
    {
        Task<CV> CV(string userId);
        Task<CV> CVUpdate(string userId, CV cV);
        Task<InforSeeker> Infor(string userId);
        Task<CV> InforApply(string userId);
    }
}
