using Model.Abstract;
using System;

namespace Model.Reports
{
    public class Report : File<string>
    {
        public DateTime? GeneratedDateUtc { get; set; }
        public string GeneratedBy { get; set; }
    }
}
