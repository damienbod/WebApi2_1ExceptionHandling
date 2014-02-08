using System.Web.Http.ExceptionHandling;
using WebApi2_1ExceptionHandling.Log;

namespace WebApi2_1ExceptionHandling
{
    public class SlabLogExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            TestEvents.Log.UnhandledException(
                context.Request.Method.ToString(),  
                context.Request.RequestUri.ToString(), 
                context.Exception.Message);
        }
    }
}