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
            // TODO вынести в константу
            .Where(fe => fe.field_id == 2)
            .Select(fe => new FillFieldDbr
            {
                field_elem_id = fe.field_elem_id,
                elem_id = fe.elem_id,
                elem_name = fe.elem.elem_name,
                x = fe.x,
                y = fe.y,
                elem_plantable = fe.elem.elem_plantable,
                elem_harvestable = fe.elem.elem_harvestable,
                elem_weed = fe.elem.elem_weed
            })
            .ToListAsync();
    }
    public async Task<int> update(FillFieldRequest request){
        // TODO убрать анонимный объект
        var fieldElem = await db.field_elem
            .Where(fe => fe.x == request.x && fe.y == request.y)
            .Select(fe => new
            {
                field_elem_id = fe.field_elem_id,
                elem_id = fe.elem_id,
                elem_weed = fe.elem.elem_weed
            })
            .FirstOrDefaultAsync();

        if (fieldElem == null){
            throw new KeyNotFoundException($"Элемент с координатами ({request.x}, {request.y}) не найден");
        }
        // TODO вынести логику в сервис
        if (fieldElem.elem_weed){

            var fieldElemUpdate = await db.field_elem
                .FirstOrDefaultAsync(fe => fe.field_elem_id == fieldElem.field_elem_id);

            if (fieldElemUpdate == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            // TODO убрать анонимный объект
            var newElem = await db.elem
                .Where(e => e.elem_name == "ground")
                .Select(e => new
                {
                    elem_id = e.elem_id
                })
                .FirstOrDefaultAsync();

            if (newElem == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            fieldElemUpdate.elem_id = newElem.elem_id;
            return await db.SaveChangesAsync();
        }
        else{
            throw new Exception("Нельзя посадить траву на траве");
        }
    }
}
