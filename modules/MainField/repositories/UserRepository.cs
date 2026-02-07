// modules/MainField/repositories/UserRepository.cs

using CatatoniaServer.Modules.MainField.Models;
using Microsoft.EntityFrameworkCore;

namespace CatatoniaServer.Modules.MainField.Repositories;
public class UserRepository
{
    private readonly ApplicationDbContext db;

    public UserRepository(ApplicationDbContext dbPar)
    {
        db = dbPar;
    }
    /// <summary>
    /// Выберает данные по пользователям
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserModel>> Index(int? id = null){
        IQueryable<UserModel> query = db.User.AsQueryable();
        if (id != null){
            query = query.Where(u => u.Id == id);
        }
        return await query
            .Select(u => new UserModel
            {
                Id = u.Id,
                Gold = u.Gold
            })
            .ToListAsync();
    }
}
