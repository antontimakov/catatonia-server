// modules/MainField/Dto/StartDto.cs

using CatatoniaServer.Modules.MainField.Dbr;
using CatatoniaServer.Modules.MainField.Models;

namespace CatatoniaServer.Modules.MainField.Dto;
public class StartDto
{
    public required List<FillFieldDbr> FieldElements { get; set; }
    public required List<UserModel> UserInfo { get; set; }
}
