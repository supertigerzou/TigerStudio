using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using Owin;
using TigerStudio.API.Middlewares;
using TigerStudio.Books.Data;
using TigerStudio.Books.Services;
using TigerStudio.Framework.Caching;
using TigerStudio.Framework.Data;
using TigerStudio.Framework.Helpers;
using TigerStudio.Framework.Services;
using TigerStudio.Users.Data;

//[assembly: OwinStartup(typeof(TigerStudio.API.Startup))]

namespace TigerStudio.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            WebApiConfig.Register(config, builder);

            // Register a logger service to be used by the controller and middleware.
            builder.Register(c => new SimpleLogger()).As<ILogger>().InstancePerRequest();
            builder.Register(c => new WebHelper()).As<IWebHelper>().InstancePerRequest();
            builder.Register(c => new BookContext()).As<IDbContext>().InstancePerRequest();
            builder.RegisterType<AuthRepository>().As<IAuthRepository>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerRequest();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerRequest();

            // Autofac will add middleware to IAppBuilder in the order registered.
            // The middleware will execute in the order added to IAppBuilder.
            builder.RegisterType<Logger>().InstancePerRequest();

            var container = builder.Build();

            // Create an assign a dependency resolver for Web API to use.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // This should be the first middleware added to the IAppBuilder.
            app.UseAutofacMiddleware(container);

            // Make sure the Autofac lifetime scope is passed to Web API.
            app.UseAutofacWebApi(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookContext, Configuration>());
        }
    }
}