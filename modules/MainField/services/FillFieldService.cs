// modules/MainField/services/FillFieldService.cs

using CatatoniaServer.Modules.MainField.Models;
using CatatoniaServer.Modules.MainField.Dbr;
using CatatoniaServer.Modules.MainField.Repositories;
using CatatoniaServer.Modules.MainField.Requests;
using CatatoniaServer.Modules.MainField.Dto;

namespace CatatoniaServer.Modules.MainField.Services;
public class FillFieldService
{
    private readonly FillFieldRepository fillFieldRepository;
    private readonly UserRepository userRepository;

    public FillFieldService(
        FillFieldRepository fillFieldRepositoryPar,
        UserRepository userRepositoryPar
    )
    {
        fillFieldRepository = fillFieldRepositoryPar;
        userRepository = userRepositoryPar;
    }
    /// <summary>
    /// Логика формирования отправки данных на сервер при загрузке проекта
    /// </summary>
    /// <returns></returns>
    public async Task<StartDto> Index(){
        List<FillFieldDbr> fieldElements = await fillFieldRepository.Index();
        List<UserModel> userInfo = await userRepository.Index();

        return new StartDto
        {
            FieldElements = fieldElements,
            UserInfo = userInfo
        };
    }
    public async Task<int> update(FillFieldRequest request){
        return await fillFieldRepository.update(request);
    }
}
