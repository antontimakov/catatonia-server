// modules/MainField/models/UserModel.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatatoniaServer.Modules.MainField.Models;
public class UserModel
{
    [Key]
    [JsonPropertyName("Id")]
    public required int Id { get; set; }
    [JsonPropertyName("Gold")]
    public required int Gold { get; set; }
}
