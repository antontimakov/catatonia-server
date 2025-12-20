// Services/FillFieldService.cs

using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Result;
using CatatoniaServer.Repositories;

namespace CatatoniaServer.Services
{
    public class FillFieldService
    {
        private readonly FillFieldRepository _fillFieldRepository;

        public FillFieldService(FillFieldRepository fillFieldRepository)
        {
            _fillFieldRepository = fillFieldRepository;
        }
        public async Task<MainResult<FillFieldResult>> index(){

            var result = await _fillFieldRepository.index();

            return new MainResult<FillFieldResult>
            {
                time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
                status = "ok",
                received = result
            };
        }
    }
}