using System.Net.Http.Headers;

namespace WebApi.Models
{
    public struct RangeContentInfo
    {
        public bool IsPartial { get; set; }

        public RangeHeaderValue RangeHeaderValue { get; set; }
    }
}