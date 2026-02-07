// modules/MainField/repositories/FillFieldRepository.cs

using CatatoniaServer.Modules.MainField.Dbr;
using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Modules.MainField.Requests;
using CatatoniaServer.Modules.MainField.Models;

namespace CatatoniaServer.Modules.MainField.Repositories;
public class FillFieldRepository
{
    private readonly ApplicationDbContext db;

    public FillFieldRepository(ApplicationDbContext dbPar)
    {
        db = dbPar;
    }
    /// <summary>
    /// Выберает все элементы поля
    /// </summary>
    /// <returns></returns>
    public async Task<List<FillFieldDbr>> Index(){
        return await db.FieldElem
            // TODO вынести в константу
            .Where(fe => fe.FieldId == 2)
            .Select(fe => new FillFieldDbr
            {
                FieldElemId = fe.Id,
                ElemId = fe.ElemId,
                Name = fe.Elem.Name,
                X = fe.X,
                Y = fe.Y,
                IsPlantable = fe.Elem.IsPlantable,
                IsHarvestable = fe.Elem.IsHarvestable,
                IsWeed = fe.Elem.IsWeed,
                Lifetime = fe.Elem.Lifetime,
                UpdatedAt = fe.UpdatedAt
            })
            .ToListAsync();
    }
    /// <summary>
    /// Выберает информацию обо всех элементах
    /// </summary>
    /// <returns></returns>
    public async Task<List<ElemModel>> AllElems(){
        return await db.Elem
            // TODO вынести в константу
            .Select(e => new ElemModel
            {
                Id = e.Id,
                Name = e.Name,
                IsPlantable = e.IsPlantable,
                IsHarvestable = e.IsHarvestable,
                IsWeed = e.IsWeed,
                Lifetime = e.Lifetime,
                Cost = e.Cost
            })
            .ToListAsync();
    }
    public async Task<int> update(FillFieldRequest request){
        // TODO убрать анонимный объект
        var fieldElem = await db.FieldElem
            .Where(fe => fe.X == request.X && fe.Y == request.Y)
            .Select(fe => new
            {
                FieldElemId = fe.Id,
                ElemId = fe.ElemId,
                IsPlantable = fe.Elem.IsPlantable,
                IsHarvestable = fe.Elem.IsHarvestable,
                IsWeed = fe.Elem.IsWeed
            })
            .FirstOrDefaultAsync();

        if (fieldElem == null){
            throw new KeyNotFoundException($"Элемент с координатами ({request.X}, {request.Y}) не найден");
        }
        // TODO вынести логику в сервис
        if (fieldElem.IsWeed){
            var fieldElemUpdate = await db.FieldElem
                .FirstOrDefaultAsync(fe => fe.Id == fieldElem.FieldElemId);

            if (fieldElemUpdate == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            // TODO убрать анонимный объект
            var newElem = await db.Elem
                .Where(e => e.Name == "ground")
                .Select(e => new
                {
                    Id = e.Id
                })
                .FirstOrDefaultAsync();

            if (newElem == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            fieldElemUpdate.ElemId = newElem.Id;
            return await db.SaveChangesAsync();
        }
        else if (fieldElem.IsPlantable)
        {
            var fieldElemUpdate = await db.FieldElem
                .FirstOrDefaultAsync(fe => fe.Id == fieldElem.FieldElemId);

            if (fieldElemUpdate == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            // TODO убрать анонимный объект
            var newElem = await db.Elem
                .Where(e => e.Name == request.NewElemName)
                .Select(e => new
                {
                    Id = e.Id
                })
                .FirstOrDefaultAsync();

            if (newElem == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            fieldElemUpdate.ElemId = newElem.Id;
            fieldElemUpdate.UpdatedAt = DateTime.UtcNow;
            return await db.SaveChangesAsync();
        }
        else if (fieldElem.IsHarvestable)
        {
            var fieldElemUpdate = await db.FieldElem
                .Include(fe => fe.Elem)
                .FirstOrDefaultAsync(fe => fe.Id == fieldElem.FieldElemId);
            if (fieldElemUpdate == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            var userUpdate = await db.User
                .FirstOrDefaultAsync(e => e.Id == 1);
            if (userUpdate == null){
                throw new KeyNotFoundException($"Не найден пользователь");
            }
            // TODO убрать анонимный объект
            var newElem = await db.Elem
                .Where(e => e.Name == "grass")
                .Select(e => new
                {
                    Id = e.Id
                })
                .FirstOrDefaultAsync();

            if (newElem == null){
                throw new KeyNotFoundException($"Не найден элемент");
            }
            fieldElemUpdate.ElemId = newElem.Id;

            userUpdate.Gold += fieldElemUpdate.Elem.Cost;
            return await db.SaveChangesAsync();
        }
        else{
            throw new Exception("Нельзя посадить траву на траве");
        }
    }
}
