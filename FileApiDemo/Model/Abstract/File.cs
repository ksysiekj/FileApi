using System;
using Infrastructure.Model;

namespace Model.Abstract
{
    public abstract class File<T> : Entity<T>
    {
        public DateTime LastUpdatedDateUtc { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedBy { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}