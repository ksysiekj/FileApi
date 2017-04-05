using Model.Reports;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApi.Controllers.Abstract;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportsController : FilesController
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsController(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Report> reports = _reportsRepository.GetAll();

            return Ok(reports.Select(r => r.MapToViewModel()));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            Report report = _reportsRepository.Get(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report.MapToViewModel());
        }

        [HttpGet]
        [HttpHead]
        [Route("{id}/content")]
        public IHttpActionResult GetContent(string id)
        {
            Report report = _reportsRepository.GetWithContent(id);
            if (report == null)
            {
                return NotFound();
            }

            RangeContentInfo rangeContentInfo = GetRangeContentInfo();

            return FileResult(report.MapToViewModel(), rangeContentInfo);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            Report report = _reportsRepository.Get(id);
            if (report == null)
            {
                return NotFound();
            }

            _reportsRepository.Delete(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}