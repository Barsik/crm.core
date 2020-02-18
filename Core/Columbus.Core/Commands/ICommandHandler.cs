namespace Columbus.InnerSource.Core.Commands
{
    /// <summary>
    /// Синхронный обработчик команды, поддерживающий возврат результата
    /// </summary>
    /// <typeparam name="TCommand">Тип команды</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Метод обработки команды
        /// </summary>
        /// <param name="command">Команда на исполнение</param>
        /// <returns>Результат команды</returns>
        ICommandResult Handle(TCommand command);
    }
}
