using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApi.Models.Reports
{
    public interface IViewModelAuditable
    {
        string LastUpdatedBy { get; set; }
        DateTime LastUpdatedDateUtc { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDateUtc { get; set; }
    }

    [DataContract]
    public abstract class FileViewModel : IFileViewModel, IViewModelAuditable
    {
        [DataMember]
        public DateTime LastUpdatedDateUtc { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string LastUpdatedBy { get; set; }
        [Required]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public DateTime CreatedDateUtc { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string CreatedBy { get; set; }
        [Required]
        [DataMember]
        public string FileName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public byte[] Content { get; set; }
    }
}