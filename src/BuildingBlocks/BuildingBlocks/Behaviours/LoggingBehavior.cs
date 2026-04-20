using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
        MediatR.IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request = {Request} - Response = {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            var timer = new Stopwatch();

            timer.Start();

            var response = await next();

            timer.Stop();

            var elapsedTime = timer.Elapsed;
            if (elapsedTime.Seconds > 3)
            {
                logger.LogWarning("[SLOW] Handle request = {Request} - Response = {Response} - ElapsedTime = {ElapsedTime}", typeof(TRequest).Name, typeof(TResponse).Name, elapsedTime);

            }
            else
            {
                logger.LogInformation("[END] Handle request = {Request} - Response = {Response} - ElapsedTime = {ElapsedTime}", typeof(TRequest).Name, typeof(TResponse).Name, elapsedTime);
            }
            return response;
        }
    }
}
