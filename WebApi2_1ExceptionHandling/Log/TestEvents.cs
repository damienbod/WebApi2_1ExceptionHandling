using System.Diagnostics.Tracing;
using System.Web.Http.ExceptionHandling;

namespace WebApi2_1ExceptionHandling.Log
{
    [EventSource(Name = "WebApi")]
    public class TestEvents : EventSource
    {
        public static readonly TestEvents Log = new TestEvents();

        [Event(1, Message = "TestEvents Critical: {0}", Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            if (IsEnabled()) WriteEvent(1, message);
        }

        [Event(2, Message = "Unhandled exception processing {0} for {1}: {2}", Level = EventLevel.Critical)]
        public void UnhandledException(string method, string requestUrl, string exception)
        {
            if (IsEnabled()) 
            {
                WriteEvent(2, method,  requestUrl, exception);
            }              
        }
    }
}

