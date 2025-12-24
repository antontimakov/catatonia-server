// Repositories/FillFieldRepository.cs

using CatatoniaServer.Result;
using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Requests;

namespace CatatoniaServer.Repositories
{
    public class FillFieldRepository
    {
        private readonly ApplicationDbContext db;

        public FillFieldRepository(ApplicationDbContext dbPar)
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
        public async void update(FillFieldRequest request){
            var fieldElem = await db.field_elem
                    .FirstOrDefaultAsync(fe => fe.x == request.x && fe.y == request.y);

            if (fieldElem == null){
                throw new KeyNotFoundException($"Элемент с координатами ({request.x}, {request.y}) не найден");
            }

            fieldElem.elem_id = request.elem_id;
            await db.SaveChangesAsync();
        }
    }
}