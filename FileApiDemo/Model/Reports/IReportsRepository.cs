using Infrastructure.Repository;

namespace Model.Reports
{
    public interface IReportsRepository : IRepository<Report, string>
    {
        Report GetWithContent(string id);
    }
}