// modules/MainField/models/Elem.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class Elem
{
    [Key]
    public int elem_id { get; set; }
    public required string elem_name { get; set; }
    public ICollection<FieldElem>? field_elems { get; set; }
}
