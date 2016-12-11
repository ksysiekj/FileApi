namespace Infrastructure.Services
{
    public interface IApplicationSettingService
    {
        string this[string appKey] { get; }
    }
}
