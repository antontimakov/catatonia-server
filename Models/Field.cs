// Models/Field.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Models
{
    public class Field
    {
        [Key]
        public int field_id { get; set; }
        public string? field_name { get; set; }
        public ICollection<FieldElem>? field_elems { get; set; }
    }
}
