using System.IO;
using WebApi.Models.Reports;

namespace WebApi.Extensions
{
    public static class FileViewModelExtensions
    {
        public static string GetExtension(this IFileViewModel fileViewModel)
        {
            return Path.GetExtension(fileViewModel.FileName);
        }
    }
}