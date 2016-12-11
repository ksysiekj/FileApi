using System;

namespace Infrastructure.Model
{
    public interface IAuditable
    {
        DateTime LastUpdatedDateUtc { get; set; }
        string LastUpdatedBy { get; set; }
        DateTime CreatedDateUtc { get; set; }
        string CreatedBy { get; set; }
    }
}