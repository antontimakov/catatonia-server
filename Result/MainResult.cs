// Result/MainResult.cs

namespace CatatoniaServer.Result
{
    public class MainResult<T> : BaseResult
    {
        public List<T> received { get; set; }
        public MainResult() => received = [];
    }
}