using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FindJobAPI.Repository.Queries
{
    public class Industry_Repository : IIndustry_Repository
    {
        private readonly AppDbContext _appDbContext;

        public Industry_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<IndustryDTO>> GetAll()
        {
            var AllIndustry = _appDbContext.Industry.AsQueryable();
            var ListIndustry = await AllIndustry.Select(industry => new IndustryDTO
            {
                industry_id = industry.industry_id,
                industry = industry.industry_name
            }).ToListAsync();
            return ListIndustry.OrderBy(i=>i.industry).ToList();
        }

        public async Task<IndustryNoId> CreateIndustry(IndustryNoId industryNoId)
        {
            var existingIndustry = await _appDbContext.Industry.FirstOrDefaultAsync(i => i.industry_name == industryNoId.industry);
            if (existingIndustry == null)
            {
                var IndustryDomain = new industry
                {
                    industry_name = industryNoId.industry
                };
                await _appDbContext.Industry.AddAsync(IndustryDomain);
                await _appDbContext.SaveChangesAsync();
                return industryNoId;
            }
            return null!;
        }

        public async Task<IndustryNoId> UpdateIndustry(int id, IndustryNoId industryNoId)
        {
            var IndustryDomain = _appDbContext.Industry.FirstOrDefault(i => i.industry_id == id);
            if (IndustryDomain == null)
                return null!;
            IndustryDomain.industry_name = industryNoId.industry;
            await _appDbContext.SaveChangesAsync();
            return industryNoId;
        }

        public async Task<industry> DeleteIndustry(int id)
        {
            var IndustryDomain = _appDbContext.Industry.SingleOrDefault(i => i.industry_id == id);
            if (IndustryDomain != null)
            {
                _appDbContext.Industry.Remove(IndustryDomain);
                await _appDbContext.SaveChangesAsync();
                return IndustryDomain;
            }
            return null!;
        }
    }
}
