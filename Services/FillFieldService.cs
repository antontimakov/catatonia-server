// Services/FillFieldService.cs

using CatatoniaServer.Result;
using CatatoniaServer.Repositories;
using CatatoniaServer.Requests;

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
        public void update(FillFieldRequest request){
            fillFieldRepository.update(request);
        }
    }
}