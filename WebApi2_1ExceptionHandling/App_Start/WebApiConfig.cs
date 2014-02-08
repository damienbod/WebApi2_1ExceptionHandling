using System.Diagnostics.Tracing;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Slab.Elasticsearch;
using WebApi2_1ExceptionHandling.Attributes;
using WebApi2_1ExceptionHandling.Log;

namespace WebApi2_1ExceptionHandling
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ObservableEventListener listener = new ObservableEventListener();
            listener.EnableEvents(TestEvents.Log, EventLevel.LogAlways, Keywords.All);

            listener.LogToConsole();
            listener.LogToElasticsearchSink("Server=localhost;Index=log;Port=9200", "slab", "WebApiEvent");
 
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            config.Filters.Add(new ValidationExceptionFilterAttribute());
            config.Services.Add(typeof(IExceptionLogger),  new SlabLogExceptionLogger());
            config.Services.Replace(typeof (IExceptionHandler), new GlobalExceptionHandler());

        }
    }
}
