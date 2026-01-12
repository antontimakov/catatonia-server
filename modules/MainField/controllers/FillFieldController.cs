// modules/MainField/controllers/FillFieldController.cs

using Microsoft.AspNetCore.Mvc;
using CatatoniaServer.Modules.MainField.Services;
using CatatoniaServer.Modules.MainField.Dbr;
using CatatoniaServer.Modules.MainField.Requests;
using CatatoniaServer.Modules.Common.Result;

namespace CatatoniaServer.Modules.MainField.Controllers;
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
            List<FillFieldDbr> result = await fillFieldService.index();

            return Ok(new MainResult<FillFieldDbr>
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
            await fillFieldService.update(request);
            return Ok(new CatatoniaServer.Modules.Common.Result.EmptyResult());
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
