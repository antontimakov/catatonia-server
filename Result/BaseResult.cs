// Result/BaseResult.cs

namespace CatatoniaServer.Result
{
    public abstract class BaseResult
    {
        public string time { get; set; }
        public string status { get; set; }

        public BaseResult()
        {
            time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z");
            status = "ok";
        }
    }

}