
namespace AHA.CongestionTax.Application.Abstractions
{
    /// <summary>
    /// Represents the standard response returned by command handlers.
    /// </summary>
    public sealed class CommandResult
    {
        /// <summary>
        /// Indicates whether the command succeeded.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Optional message describing the result (error or info).
        /// </summary>
        public string? Message { get; }

        private CommandResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public static CommandResult Ok(string? message = null)
            => new(true, message);

        public static CommandResult Fail(string message)
            => new(false, message);
    }
}

