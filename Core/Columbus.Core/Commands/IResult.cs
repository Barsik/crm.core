using System;

namespace Columbus.InnerSource.Core.Commands
{
    public interface IResult
    {
        bool Success { get; }
        Exception Error { get; }
    }

    public interface IResult<out T> : IResult
    {
        T Value { get; }
    }
}
