// Controllers/DataController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Models;

namespace CatatoniaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DataController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST api/data/getdb
        [HttpPost("getdb")]
        public async Task<IActionResult> GetDb([FromBody] RequestModel request)
        {
            if (request == null)
                return BadRequest("Некорректные данные");

            string? did = request.did;
            string? time_fishing = request.time_fishing;

            Console.WriteLine($"Получено: Id={did}, Action={time_fishing}");
            try
            {
                var result = await _db.field_elem
                    .Where(fe => fe.field_id == 2)
                    .Select(fe => new
                    {
                        fe.elem.elem_name,
                        fe.x,
                        fe.y
                    })
                    .ToListAsync();

                return Ok(new
                {
                    time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
                    status = "ok",
                    received = result
                });
            }
            catch (Exception ex)
            {
                return Problem($"Ошибка: {ex.Message}", statusCode: 500);
            }
        }

        // POST api/data/setdb
        [HttpPost("setdb")]
        public async Task<IActionResult> SetDb([FromBody] SetDbRequest request)
        {
            if (request == null)
                return BadRequest("Некорректные данные");

            try
            {
                var fieldElem = await _db.field_elem
                    .FirstOrDefaultAsync(fe => fe.x == request.x && fe.y == request.y);

                if (fieldElem == null)
                    return NotFound($"Позиция ({request.x}, {request.y}) не найдена");

                fieldElem.elem_id = request.elem_id;
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Обновлено",
                    updated = new { request.x, request.y, request.elem_id }
                });
            }
            catch (Exception ex)
            {
                return Problem($"Ошибка: {ex.Message}", statusCode: 500);
            }
        }
    }
}
