using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace TigerStudio.API.Middlewares
{
    public interface ILogger
    {
        void Info(string message);
    }

    public class SimpleLogger : ILogger
    {
        public void Info(string message)
        {
            Debug.WriteLine(message);
        }
    }

    public class LoggerOptions
    {
        public IList<string> RequestKeys { get; set; }
        public IList<string> ResponseKeys { get; set; }
    }

    public class Logger : OwinMiddleware
    {
        private readonly ILogger _logger;
        private readonly LoggerOptions _options;

        public Logger(OwinMiddleware next, ILogger logger) : base(next)
        {
            _logger = logger;
            _options = LoggerConfig.Init();
        }

        public async override Task Invoke(IOwinContext context)
        {
            foreach (var key in _options.RequestKeys)
            {
                _logger.Info(string.Format("{0}: {1}", key, context.Environment[key]));
            }

            await Next.Invoke(context);

            foreach (var key in _options.ResponseKeys)
            {
                _logger.Info(string.Format("{0}: {1}", key, context.Environment[key]));
            }
        }
    }
}