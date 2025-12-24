// Controllers/FillFieldController.cs

using Microsoft.AspNetCore.Mvc;
using CatatoniaServer.Services;
using CatatoniaServer.Result;
using CatatoniaServer.Requests;

namespace CatatoniaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FillFieldController : ControllerBase
    {
        private readonly FillFieldService fillFieldService;

        public FillFieldController(FillFieldService fillFieldServicePar)
        {
            fillFieldService = fillFieldServicePar;
        }

        // POST api/FillField/index
        [HttpPost("index")]
        public async Task<IActionResult> index()
        {
            try
            {
                List<FillFieldResult> result = await fillFieldService.index();

                return Ok(new MainResult<FillFieldResult>
                {
                    received = result
                });
            }
            catch (Exception ex)
            {
                string detail = $"Ошибка получения : {ex.Message}";
                return base.Problem(
                    detail: detail,
                    statusCode: 500);
            }
        }

        // POST api/FillField/update
        [HttpPost("update")]
        public async Task<IActionResult> update([FromBody] FillFieldRequest request)
        {
            if (request == null){
                return BadRequest("Некорректные данные");
            }

            try
            {
                fillFieldService.update(request);
                return Ok(new CatatoniaServer.Result.EmptyResult());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                string detail = $"Ошибка: {ex.Message}";
                return base.Problem(
                    detail: detail,
                    statusCode: 500);
            }
        }
    }
}
