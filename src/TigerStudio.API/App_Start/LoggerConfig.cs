using TigerStudio.API.Middlewares;

namespace TigerStudio.API
{
    public class LoggerConfig
    {
        public static LoggerOptions Init()
        {
            return new LoggerOptions
                {
                    RequestKeys = new[] {"owin.RequestPath", "owin.RequestMethod"},
                    ResponseKeys = new[] {"owin.ResponseStatusCode"}
                };
        }
    }
}