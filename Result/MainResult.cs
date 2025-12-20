// Result/MainResult.cs

namespace CatatoniaServer.Result
{
    public class MainResult<Received>
    {
        public required string time { get; set; }
        public required string status { get; set; }
        public required List<Received> received { get; set; }
    }
}