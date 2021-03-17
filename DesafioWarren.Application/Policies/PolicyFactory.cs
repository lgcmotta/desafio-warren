using System;
using Serilog;
using Polly;
using Polly.Retry;

namespace DesafioWarren.Application.Policies
{
    public class PolicyFactory
    {
        public static AsyncRetryPolicy CreateAsyncRetryPolicy(ILogger logger) => Policy.Handle<Exception>()
            .WaitAndRetryAsync(10, sleepDurationProvider => TimeSpan.FromSeconds(5), (exception, timeSpan, attempt, context) =>
            {
                logger.Error(exception, "----------------- An exception of type {ExceptionType} with message {Message} has occurred. Retry attempt: {Attempt}, trying again in {Seconds} StackTrace: {StackTrace}"
                    , exception.GetType().Name
                    , exception.Message
                    , attempt
                    , timeSpan.Seconds
                    , exception.StackTrace);
            });
    }
}