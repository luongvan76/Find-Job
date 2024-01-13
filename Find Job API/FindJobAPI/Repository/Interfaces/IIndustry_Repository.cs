using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IIndustry_Repository
    {
        Task<List<IndustryDTO>> GetAll();
        Task<IndustryNoId> CreateIndustry(IndustryNoId industryNoId);
        Task<IndustryNoId> UpdateIndustry(int id, IndustryNoId industryNoId);
        Task<industry> DeleteIndustry(int id);
    }
}
