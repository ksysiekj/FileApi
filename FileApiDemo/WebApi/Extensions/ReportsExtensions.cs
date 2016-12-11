using Model.Reports;
using WebApi.Models.Reports;

namespace WebApi.Extensions
{
    public static class ReportsExtensions
    {
        public static ReportViewModel MapToViewModel(this Report report)
        {
            return new ReportViewModel
            {
                LastUpdatedBy = report.LastUpdatedBy,
                CreatedBy = report.CreatedBy,
                LastUpdatedDateUtc = report.LastUpdatedDateUtc,
                FileName = report.FileName,
                Id = report.FileName,
                CreatedDateUtc = report.CreatedDateUtc,
                GeneratedBy = report.GeneratedBy,
                GeneratedDateUtc = report.GeneratedDateUtc,
                Content = report.Content
            };
        }
    }
}