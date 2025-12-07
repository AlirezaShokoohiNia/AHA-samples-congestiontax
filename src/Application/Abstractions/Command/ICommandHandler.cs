namespace AHA.CongestionTax.Application.Abstractions.Command
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler capable of processing a command of type <typeparamref name="TCommand"/>
    /// and returning a <see cref="CommandResult{TResult}"/>.
    /// </summary>
    /// <typeparam name="TCommand">Command type this handler processes.</typeparam>
    /// <typeparam name="TResult">Type of the payload returned by the command.</typeparam>
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand
    {
        /// <summary>
        /// Asynchronously handles the given command and returns a <see cref="CommandResult{TResult}"/>.
        /// </summary>
        /// <param name="command">The command to process.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A <see cref="CommandResult{TResult}"/> indicating success or failure.</returns>
        Task<CommandResult<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}

