// modules/MainField/repositories/FillFieldRepository.cs

using CatatoniaServer.Modules.MainField.Dbr;
using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Modules.MainField.Requests;

namespace CatatoniaServer.Modules.MainField.Repositories;
public class FillFieldRepository
{
    private readonly ApplicationDbContext db;

    public FillFieldRepository(ApplicationDbContext dbPar)
    {
        db = dbPar;
    }
    public async Task<List<FillFieldDbr>> index(){
        return await db.field_elem
            .Where(fe => fe.field_id == 2)
            .Select(fe => new FillFieldDbr
            {
                elem_name = fe.elem.elem_name,
                x = fe.x,
                y = fe.y
            })
            .ToListAsync();
    }
    public async Task<int> update(FillFieldRequest request){
        var fieldElem = await db.field_elem
                .FirstOrDefaultAsync(fe => fe.x == request.x && fe.y == request.y);

        if (fieldElem == null){
            throw new KeyNotFoundException($"Элемент с координатами ({request.x}, {request.y}) не найден");
        }

        fieldElem.elem_id = request.elem_id;
        return await db.SaveChangesAsync();
    }
}
