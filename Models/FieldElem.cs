// Models/FieldElem.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Models
{
    public class FieldElem
    {
        [Key]
        public int field_elem_id { get; set; }
        public required int elem_id { get; set; }
        public required int field_id { get; set; }
        public required int x { get; set; }
        public required int y { get; set; }
        public required int field_order { get; set; }

        // Навигационные свойства
        public required Field field { get; set; }
        public required Elem elem { get; set; }
    }
}
