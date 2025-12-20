// Repositories/FillFieldRepository.cs

using CatatoniaServer.Result;
using Microsoft.EntityFrameworkCore;

namespace CatatoniaServer.Repositories
{
    public class FillFieldRepository
    {
        private readonly ApplicationDbContext _db;

        public FillFieldRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<FillFieldResult>> index(){
            return await _db.field_elem
                .Where(fe => fe.field_id == 2)
                .Select(fe => new FillFieldResult
                {
                    elem_name = fe.elem.elem_name,
                    x = fe.x,
                    y = fe.y
                })
                .ToListAsync();
        }
    }
}