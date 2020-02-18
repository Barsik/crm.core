using System;

namespace Columbus.InnerSource.Core.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; }
        public Exception Error { get; }
        public object Value { get; }
        public T As<T>() where T : class => Value as T;

        protected internal CommandResult(bool success)
        {
            Success = success;
        }

        protected internal CommandResult(object value, bool success)
        {
            Success = success;
            Value = value;
        }
        protected internal CommandResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public static ICommandResult Fail(Exception error) => new CommandResult(error);
        public static ICommandResult Ok() => new CommandResult(true);
        public static ICommandResult Ok(object result) => new CommandResult(result, true);
    }
}
