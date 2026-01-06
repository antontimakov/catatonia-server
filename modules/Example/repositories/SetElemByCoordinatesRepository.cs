// Repositories/SetElemByCoordinatesRepository.cs

/*using CatatoniaServer.Result;
using Microsoft.EntityFrameworkCore;

namespace CatatoniaServer.Repositories
{
    public class SetElemByCoordinatesRepository
    {
        private readonly ApplicationDbContext db;

        public SetElemByCoordinatesRepository(ApplicationDbContext dbPar)
        {
            db = dbPar;
        }
        public async Task<List<FillFieldResult>> index(){
            return await db.field_elem
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
}*/
