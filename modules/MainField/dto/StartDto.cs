// modules/MainField/Dto/StartDto.cs
using System.Text.Json.Serialization;
using CatatoniaServer.Modules.MainField.Dbr;
using CatatoniaServer.Modules.MainField.Models;

namespace CatatoniaServer.Modules.MainField.Dto;
public class StartDto
{
    [JsonPropertyName("FieldElements")]
    public required List<FillFieldDbr> FieldElements { get; set; }
    [JsonPropertyName("UserInfo")]
    public required List<UserModel> UserInfo { get; set; }
}
