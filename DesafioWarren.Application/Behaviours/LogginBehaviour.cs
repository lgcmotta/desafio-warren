using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioWarren.Application.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var commandName = request.GetGenericTypeName();

            _logger.LogInformation("Handling command '{CommandName}'", commandName);

            var response = await next();

            _logger.LogInformation("Command '{CommandName}' was handled successfully.", commandName);

            return response;
        }
    }
}