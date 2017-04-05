using System;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.ActionResults;
using WebApi.Models;
using WebApi.Models.Reports;

namespace WebApi.Controllers.Abstract
{
    public abstract class FilesController : ApiController
    {
        protected RangeContentInfo GetRangeContentInfo()
        {
            RangeContentInfo rangeContentInfo = new RangeContentInfo
            {
                IsPartial = false
            };

            RangeHeaderValue rangeHeaderValue = Request.Headers.Range;
            if (rangeHeaderValue != null)
            {
                if (rangeHeaderValue.Ranges.Count > 1 || !string.Equals(rangeHeaderValue.Unit, ActionResults.FileResult.RangeUnit, StringComparison.InvariantCultureIgnoreCase))
                    throw new HttpResponseException(HttpStatusCode.RequestedRangeNotSatisfiable);

                rangeContentInfo.IsPartial = true;
                rangeContentInfo.RangeHeaderValue = rangeHeaderValue;
            }

            return rangeContentInfo;
        }

        protected IHttpActionResult FileResult(IFileViewModel fileViewModel, RangeContentInfo rangeContentInfo)
        {
            return new FileResult(fileViewModel, rangeContentInfo);
        }
    }
}