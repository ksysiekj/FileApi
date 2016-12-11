using System;
using System.Runtime.Serialization;

namespace WebApi.Models.Reports
{
    [DataContract]
    public class ReportViewModel : FileViewModel
    {
        [DataMember(EmitDefaultValue = false)]
        public DateTime? GeneratedDateUtc { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GeneratedBy { get; set; }
    }
}