// Result/MainResult.cs

namespace CatatoniaServer.Result
{
    public class MainResult<T>
    {
        public string time { get; set; }
        public string status { get; set; }
        public List<T> received { get; set; }

        public MainResult(List<T> receivedPar)
        {
            time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            status = "ok";
            received = receivedPar;
        }
    }
}