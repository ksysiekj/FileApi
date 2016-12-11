using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;
using WebApi.Models.Reports;

namespace WebApi.ActionResults
{
    public class FileResult : IHttpActionResult
    {
        private readonly IFileViewModel _fileViewModel;
        private readonly RangeContentInfo _rangeContentInfo;
        public const string RangeUnit = "bytes";

        public FileResult(IFileViewModel fileViewModel, RangeContentInfo rangeContentInfo)
        {
            _fileViewModel = fileViewModel;
            _rangeContentInfo = rangeContentInfo;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            string contentType = MimeMapping.GetMimeMapping(Path.GetExtension(_fileViewModel.FileName));

            HttpResponseMessage response =
                _rangeContentInfo.IsPartial
                    ? new HttpResponseMessage(HttpStatusCode.PartialContent)
                    {
                        Content = new ByteRangeStreamContent(new MemoryStream(_fileViewModel.Content), _rangeContentInfo.RangeHeaderValue,
                        contentType)
                    }
                    : new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new ByteArrayContent(_fileViewModel.Content)
                    };

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _fileViewModel.FileName
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            response.Headers.AcceptRanges.Add(RangeUnit);

            return Task.FromResult(response);
        }
    }
}