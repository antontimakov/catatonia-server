// modules/MainField/models/UserModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class UserModel
{
    [Key]
    public required int UserId { get; set; }
    public required int Gold { get; set; }
}
