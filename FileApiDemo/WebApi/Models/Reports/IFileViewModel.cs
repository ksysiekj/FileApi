namespace WebApi.Models.Reports
{
    public interface IFileViewModel
    {
        string FileName { get; set; }

        byte[] Content { get; set; }
    }
}