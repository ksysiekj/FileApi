using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApi.DependencyResolver;
using WebApi.Handlers;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            RegisterRoutes(config);

            RegisterDependencyResolver(config);

            RegisterFormatters(config);
        }

        private static void RegisterFormatters(HttpConfiguration config)
        {
            MediaTypeFormatterCollection formatters = config.Formatters;
            JsonMediaTypeFormatter jsonFormatter = formatters.JsonFormatter;
            JsonSerializerSettings settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private static void RegisterDependencyResolver(HttpConfiguration config)
        {
            config.DependencyResolver = UnityDependencyResolver.BuildDependencyResolver();
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new MethodOverrideHandler());
            config.MessageHandlers.Add(new ETagMessageHandler());

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            // config.SuppressDefaultHostAuthentication();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
