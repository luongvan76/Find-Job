using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FirebaseAdmin.Auth;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IAccount_Repository
    {
        Task<List<AllAccountDTO>> GetAll(bool isDescending, int pageNumber, int pageSize);
        Task<Login> Post(string userId);
        Task<GetUser> Login(string email, string password);
        Task<UserRecord> Info(string userId, Infor info);
        Task<UserRecord> Photo(string userId, Photo photo);
        Task<UserRecord> Password(string userId, Password password);
        Task<account> DeleteAccount(string userId);
        Task<int> AccountQuantity();
        Task<string> AddAdmin(string userId);
    }
}
