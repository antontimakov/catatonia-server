// modules/MainField/models/FieldModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class FieldModel
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<FieldElemModel>? FieldElems { get; set; }
}
