using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using TigerStudio.Framework.WebApiExtensions;

namespace TigerStudio.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, ContainerBuilder builder)
        {
            var assemblyResolver = new ExtendedDefaultAssembliesResolver(builder);
            config.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);

            // Register Web API controller in executing assembly.
            foreach (var assembly in assemblyResolver.GetAssemblies())
            {
                builder.RegisterApiControllers(assembly);
            }

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}