// Controllers/DataController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Models;
using CatatoniaServer.Services;
using CatatoniaServer.Result;

namespace CatatoniaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly FillFieldService fillFieldService;

        public DataController(FillFieldService fillFieldServicePar)
        {
            fillFieldService = fillFieldServicePar;
        }

        // POST api/data/getdb
        [HttpPost("getdb")]
        public async Task<IActionResult> GetDb([FromBody] RequestModel request)
        {
            try
            {
                List<FillFieldResult> result = await fillFieldService.index();

                return Ok(new MainResult<FillFieldResult>(result));
            }
            catch (Exception ex)
            {
                return Problem($"Ошибка: {ex.Message}", statusCode: 500);
            }
        }

        // POST api/data/setdb
        /*[HttpPost("setdb")]
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
        }*/
    }
}
