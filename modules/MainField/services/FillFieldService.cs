// modules/MainField/services/FillFieldService.cs

using CatatoniaServer.Modules.MainField.Dbr;
using CatatoniaServer.Modules.MainField.Repositories;
using CatatoniaServer.Modules.MainField.Requests;

namespace CatatoniaServer.Modules.MainField.Services;
public class FillFieldService
{
    private readonly FillFieldRepository fillFieldRepository;

    public FillFieldService(FillFieldRepository fillFieldRepositoryPar)
    {
        fillFieldRepository = fillFieldRepositoryPar;
    }
    public async Task<List<FillFieldDbr>> index(){
        return await fillFieldRepository.index();
    }
    public async Task<int> update(FillFieldRequest request){
        return await fillFieldRepository.update(request);
    }
}
