using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IType_Repository
    {
        Task<List<TypeDTO>> GetAll();
        Task<TypeNoId> CreateType(TypeNoId typeNoId);
        Task<TypeNoId> UpdateType(int id, TypeNoId typeNoId);
        Task<type> DeleteType(int id);
    }
}
