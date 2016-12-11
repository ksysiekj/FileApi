using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Reports;

namespace WebApi.Handlers
{
    public class ETagMessageHandler : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            if (request.Method == HttpMethod.Get)
            {
                //if this resource has been updated, let the request proceed to the operation and set a new etag on the return
                return base.SendAsync(request, cancellationToken).ContinueWith(task =>
                {
                    var resp = task.Result;
                    if (resp.IsSuccessStatusCode && resp.Content is ObjectContent)
                    {
                        IViewModelAuditable viewModelAuditable = resp.Content.ReadAsAsync<object>(cancellationToken).Result as IViewModelAuditable;
                        if (viewModelAuditable != null)
                        {
                            resp.Headers.CacheControl = new CacheControlHeaderValue { Public = true };
                            resp.Headers.ETag = new EntityTagHeaderValue(ETagGenerator.GeneratorETag(viewModelAuditable.LastUpdatedDateUtc));
                            resp.Content.Headers.LastModified = viewModelAuditable.LastUpdatedDateUtc;
                        }
                    }
                    return resp;
                }, cancellationToken);
            }

            //if (request.Method == HttpMethod.Put || request.Method == HttpMethod.Post)
            //    //let the request processing continue; new etag value for resource; update response header
            //    if (IfMatchContainsStoredEtagValue(request))
            //        return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            //        {
            //            var resp = task.Result;
            //            resp.Headers.ETag = new EntityTagHeaderValue(_eTagStore.UpdateETagFor(request.RequestUri));
            //            return resp;
            //        }, cancellationToken);

            //    //stop processing and return a 412/precondition failed; update response header
            //    else
            //        return taskFactory.StartNew(() =>
            //        {
            //            var resp = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
            //            resp.Headers.ETag = new EntityTagHeaderValue(_eTagStore.Fetch(request.RequestUri));
            //            return resp;
            //        }, cancellationToken);

            //by default, let the request keep moving up the message handler stack
            return base.SendAsync(request, cancellationToken);
        }
    }
}