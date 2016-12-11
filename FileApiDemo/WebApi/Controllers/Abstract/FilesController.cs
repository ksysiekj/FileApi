using System;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.ActionResults;
using WebApi.Models;

namespace WebApi.Controllers.Abstract
{
    public abstract class FilesController : ApiController
    {
        protected RangeContentInfo SetRangeContentInfo()
        {
            RangeContentInfo rangeContentInfo = new RangeContentInfo
            {
                IsPartial = false
            };

            RangeHeaderValue rangeHeaderValue = Request.Headers.Range;
            if (rangeHeaderValue != null)
            {
                if (rangeHeaderValue.Ranges.Count > 1 || !string.Equals(rangeHeaderValue.Unit, FileResult.RangeUnit, StringComparison.InvariantCultureIgnoreCase))
                    throw new HttpResponseException(HttpStatusCode.RequestedRangeNotSatisfiable);

                rangeContentInfo.IsPartial = true;
                rangeContentInfo.RangeHeaderValue = rangeHeaderValue;
            }

            return rangeContentInfo;
        }
    }
}