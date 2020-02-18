using System;

namespace Columbus.InnerSource.Core.Commands
{
    /// <summary cref="ICommand">
    /// Определение команды для шины-команда <see cref="ICommandBus"/>
    /// Команда, для которой не требуется результат операции
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Иденифифкатор команды
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// Дата создания команды
        /// </summary>
        DateTime CreatedOnUtc { get; }
    }
}
