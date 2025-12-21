// Services/FillFieldService.cs

using Microsoft.EntityFrameworkCore;
using CatatoniaServer.Result;
using CatatoniaServer.Repositories;

namespace CatatoniaServer.Services
{
    public class FillFieldService
    {
        private readonly FillFieldRepository fillFieldRepository;

        public FillFieldService(FillFieldRepository fillFieldRepositoryPar)
        {
            fillFieldRepository = fillFieldRepositoryPar;
        }
        public async Task<List<FillFieldResult>> index(){

            return await fillFieldRepository.index();
        }
    }
}