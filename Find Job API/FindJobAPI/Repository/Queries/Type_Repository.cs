using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;

namespace FindJobAPI.Repository.Queries
{
    public class Type_Repository : IType_Repository
    {
        private readonly AppDbContext _context;

        public Type_Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TypeDTO>> GetAll()
        {
            var AllType = _context.Type.AsQueryable();
            var ListType = await AllType.Select(type => new TypeDTO
            {
                type_id = type.type_id,
                type_name = type.type_name
            }).ToListAsync();
            return ListType.OrderBy(t=>t.type_name).ToList();
        }

        public async Task<TypeNoId> CreateType(TypeNoId typeNoId)
        {
            var existingType = await _context.Type.FirstOrDefaultAsync(t => t.type_name == typeNoId.type_name);
            if (existingType != null)
            {
                return null!;
            }
            var AddType = new type
            {
                type_name = typeNoId.type_name
            };

            await _context.Type.AddAsync(AddType);
            await _context.SaveChangesAsync();
            return typeNoId;
        }

        public async Task<TypeNoId> UpdateType(int id, TypeNoId typeNoId)
        {
            var TypeDomain = await _context.Type.FirstOrDefaultAsync(t => t.type_id == id);
            if (TypeDomain != null)
            {
                TypeDomain.type_name = typeNoId.type_name;
                await _context.SaveChangesAsync();
                return typeNoId;
            }
            return null!;
        }

        public async Task<type> DeleteType(int id)
        {
            var TypeDomain = await _context.Type.FirstOrDefaultAsync(t => t.type_id == id);
            if (TypeDomain != null)
            {
                _context.Type.Remove(TypeDomain);
                await _context.SaveChangesAsync();
            }
            else return null!;
            return TypeDomain;
        }
    }
}
