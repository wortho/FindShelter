using System.Web.Http;
using FindShelter.Web;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace FindShelter.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            httpConfiguration.Routes.MapHttpRoute( 
 				name: "Html", 
 				routeTemplate: "view*", 
 				defaults: new {view = "index.html", controller = "default"} 
 				); 

            app.UseWebApi(httpConfiguration);

            if (!app.Properties.ContainsKey("Test"))
            {
                // Make ./public the default root of the static files in our Web Application.
                const string root = ".";
                app.UseFileServer(new FileServerOptions
                {
                    RequestPath = new PathString(string.Empty),
                    FileSystem = new PhysicalFileSystem(root),
                    EnableDirectoryBrowsing = true,
                });
            }

            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
