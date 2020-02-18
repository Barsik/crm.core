using System.Threading;
using System.Threading.Tasks;

namespace Columbus.InnerSource.Core.Commands
{
    /// <summary>
    ///  Асинхронный обработчик команды с поддержкой возврата результата
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Метод обработки команды
        /// </summary>
        /// <param name="command">Команда на исполнение</param>
        /// <param name="token">Токен отмены операции</param>
        /// <returns>Результат команды</returns>
        Task<ICommandResult> HandleAsync(TCommand command, CancellationToken token = default);
    }
}
