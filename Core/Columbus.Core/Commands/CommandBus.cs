using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Columbus.InnerSource.Core.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _resolver;

        public CommandBus(IServiceProvider resolver)
        {
            _resolver = resolver;
        }

        public Task<ICommandResult> SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(typeof(TCommand));
            var handler = _resolver.GetService<ICommandHandlerAsync<TCommand>>();
            return (handler ?? throw new NullReferenceException($"Handler {handlerType} is null")).HandleAsync(command);
        }

        public ICommandResult Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            var handler = _resolver.GetService<ICommandHandler<TCommand>>();
            return (handler ?? throw new NullReferenceException($"Handler {handlerType} is null")).Handle(command);
        }
    }
}
