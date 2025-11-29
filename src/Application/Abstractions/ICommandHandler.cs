
namespace AHA.CongestionTax.Application.Abstractions
{
    /// <summary>
    /// Defines a handler capable of processing a command of type TCommand.
    /// </summary>
    /// <typeparam name="TCommand">Command type this handler processes.</typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Asynchronously handles the given command and returns a result.
        /// </summary>
        Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}

