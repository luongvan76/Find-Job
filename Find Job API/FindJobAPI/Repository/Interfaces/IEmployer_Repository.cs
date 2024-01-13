using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IEmployer_Repository
    {
        Task<EmployerDTO> Get(string userId);
        Task<Image> Image(string userId, Image image);
        Task<Cover> ImageCover(string userId, Cover cover);
        Task<InforEmployer> Infor (string userId, InforEmployer inforEmployer);
    }
}
