// modules/MainField/models/FieldModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class FieldModel
{
    [Key]
    public int field_id { get; set; }
    public string? field_name { get; set; }
    public ICollection<FieldElemModel>? field_elems { get; set; }
}
