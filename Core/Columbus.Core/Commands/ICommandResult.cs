using System;

namespace Columbus.InnerSource.Core.Commands
{
    /// <summary>
    /// Результат исполнения команды
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// Успешно или нет
        /// </summary>
        bool Success { get; }
        /// <summary>
        /// Информация об ошибке
        /// </summary>
        Exception Error { get; }

        object Value { get; }

        T As<T>() where T : class;
    }
}
