using Infrastructure.Consts;
using Infrastructure.Logger;
using Infrastructure.Services;
using Microsoft.Practices.Unity;
using Model.Reports;
using System.Web.Http.Dependencies;

namespace WebApi.DependencyResolver
{
    public static class UnityDependencyResolver
    {
        public static IDependencyResolver BuildDependencyResolver()
        {
            IUnityContainer container = new UnityContainer();

            RegisterServices(container);
            RegisterRepositories(container);

            return new UnityResolver(container);
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IApplicationSettingService, ApplicationSettingService>();
            container.RegisterType<ILoggerFactory, Log4NetLoggerFactory>();
            container.RegisterType(typeof(ILogger<>), typeof(Logger<>));
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            IApplicationSettingService applicationSettingService = container.Resolve<IApplicationSettingService>();

            container.RegisterType<IReportsRepository, ReportsRepository>(new InjectionConstructor(applicationSettingService[Consts.AppSettings.ReportsSourceDirectoryPath]));
        }
    }
}