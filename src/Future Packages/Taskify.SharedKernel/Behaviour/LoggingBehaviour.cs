namespace Taskify.SharedKernel.Behaviour;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly ILogger<Mediator> _logger;

#pragma warning disable S6672 // Generic logger injection should match enclosing type
    public LoggingBehaviour(ILogger<Mediator> logger)
#pragma warning restore S6672 // Generic logger injection should match enclosing type
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);
        if (!_logger.IsEnabled(LogLevel.Information))
            return await next();

        _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

        // Reflection! Could be a performance concern
        Type myType = request.GetType();
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        foreach (PropertyInfo prop in props)
        {
            object? propValue = prop?.GetValue(request, null);
            _logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
        }

        var sw = Stopwatch.StartNew();

        var response = await next();

        _logger.LogInformation("Handled {RequestName} with {Response} in {Ms} ms", typeof(TRequest).Name, response, sw.ElapsedMilliseconds);
        sw.Stop();
        return response;
    }
}
