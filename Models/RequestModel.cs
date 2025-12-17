// Models/RequestModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Models
{
    public class RequestModel
    {
        public string? did { get; set; }
        public string? time_fishing { get; set; }
    }
}
