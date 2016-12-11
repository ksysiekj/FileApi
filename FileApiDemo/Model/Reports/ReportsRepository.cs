using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Model.Reports
{
    /// <summary>
    /// dummy implementation of report repository - just for demo purposes
    /// </summary>
    public sealed class ReportsRepository : IReportsRepository
    {
        private readonly string _sourceDirectoryPath;

        public ReportsRepository(string sourceDirectoryPath)
        {
            _sourceDirectoryPath = sourceDirectoryPath;
        }

        private static readonly Func<FileInfo, bool, Report> FileInfoToReportProjection =
            (fileInfo, includeContent) => new Report
            {
                FileName = fileInfo.Name,
                CreatedDateUtc = fileInfo.CreationTimeUtc,
                Id = fileInfo.Name,
                LastUpdatedDateUtc = fileInfo.LastWriteTimeUtc,
                CreatedBy =
                    fileInfo.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                LastUpdatedBy =
                    fileInfo.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString(),
                Content = includeContent ? File.ReadAllBytes(fileInfo.FullName) : null
            };

        public void Delete(string id)
        {
            string deletePath = CombineToRootPath(id);
            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
            }
        }

        private string CombineToRootPath(string fileName)
        {
            return Path.Combine(_sourceDirectoryPath, fileName);
        }

        private FileInfo[] GetAllFiles()
        {
            return new DirectoryInfo(_sourceDirectoryPath)
                .GetFiles();
        }

        public IEnumerable<Report> GetAll()
        {
            return GetAllFiles()
                .Select(q => FileInfoToReportProjection(q, false));
        }

        public IEnumerable<Report> Get(Expression<Func<Report, bool>> expression)
        {
            return GetAll().Where(expression.Compile());
        }

        private Report Get(string id, bool includeContent)
        {
            string filePath = CombineToRootPath(id);
            if (File.Exists(filePath))
            {
                return FileInfoToReportProjection(new FileInfo(filePath), includeContent);
            }

            return null;
        }

        public Report GetWithContent(string id)
        {
            return Get(id, true);
        }

        public Report Get(string id)
        {
            return Get(id, false);
        }

        public void SaveOrUpdate(Report entity)
        {
            string filePath = CombineToRootPath(entity.Id);
            File.WriteAllBytes(filePath, entity.Content);
        }
    }
}