// Controllers/SetElemByCoordinatesController.cs

using Microsoft.AspNetCore.Mvc;
using CatatoniaServer.Services;
using CatatoniaServer.Result;
using CatatoniaServer.Requests;

namespace CatatoniaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetElemByCoordinatesController : ControllerBase
    {
        // POST api/FillField/index
        [HttpPost("setdb")]
        public async Task<IActionResult> SetDb([FromBody] FillFieldRequest request)
        {
            if (request == null)
                return BadRequest("Некорректные данные");

            try
            {
                return Ok(new
                {
                    message = "Обновлено",
                });
            }
            catch (Exception ex)
            {
                return Problem($"Ошибка: {ex.Message}", statusCode: 500);
            }
        }
    }
}
