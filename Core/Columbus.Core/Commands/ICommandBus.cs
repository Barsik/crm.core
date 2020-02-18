using System.Threading.Tasks;

namespace Columbus.InnerSource.Core.Commands
{
    /// <summary>
    /// Шина-команда. Предназначена для маршрутизации команд с последующим их выполнением
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Выполнить команду в асинхронном режиме
        /// </summary>
        /// <typeparam name="TCommand">Тип команды</typeparam>
        /// <typeparam name="TResult">Тип ожидаемого результата команды</typeparam>
        /// <param name="command">Команда для выполнения</param>
        /// <returns>Результат обработки команды</returns>
        Task<ICommandResult> SendAsync<TCommand>(TCommand command) where TCommand : ICommand;

        /// <summary>
        /// Выполнить команду в синхронном режиме
        /// </summary>
        /// <typeparam name="TCommand">Тип команды</typeparam>
        /// <param name="command">Команда для выполнения</param>
        /// <returns>Результат обработки команды</returns>
        ICommandResult Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
