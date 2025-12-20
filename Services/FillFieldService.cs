// Services/FillFieldService.cs

using CatatoniaServer.Result;
using Microsoft.EntityFrameworkCore;

namespace CatatoniaServer.Services
{
    public class FillFieldService
    {
        private readonly ApplicationDbContext _db;

        public FillFieldService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<MainResult<FillFieldResult>> index(){
            List<FillFieldResult> result = await _db.field_elem
                .Where(fe => fe.field_id == 2)
                .Select(fe => new FillFieldResult
                {
                    elem_name = fe.elem.elem_name,
                    x = fe.x,
                    y = fe.y
                })
                .ToListAsync();

            return new MainResult<FillFieldResult>
            {
                time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
                status = "ok",
                received = result
            };
        }
    }
}