namespace AHA.CongestionTax.Application.Abstractions.Commands
{
    /// <summary>
    /// Represents the outcome of a command operation in the application layer.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Indicates whether the operation succeeded.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Contains the error message if the operation failed; otherwise null.
        /// </summary>
        public string? Error { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult"/> class.
        /// </summary>
        /// <param name="isSuccess">True if the operation succeeded.</param>
        /// <param name="error">Error message if failed, otherwise null.</param>
        protected CommandResult(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Creates a successful result without a value.
        /// </summary>
        public static CommandResult Success() => new(true, null);

        /// <summary>
        /// Creates a failed result with the specified error message.
        /// </summary>
        /// <param name="error">Error message describing the failure.</param>
        public static CommandResult Failure(string error) => new(false, error);

        /// <summary>
        /// Creates a successful result with a value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value returned by the operation.</param>
        public static CommandResult<T> Success<T>(T value) => new(value, true, null);

        /// <summary>
        /// Creates a failed result with the specified error message.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="error">Error message describing the failure.</param>
        public static CommandResult<T> Failure<T>(string error) => new(default, false, error);
    }

    /// <summary>
    /// Represents the outcome of a command operation in the application layer.
    /// </summary>
    /// <typeparam name="T">Type of the value returned.</typeparam>
    public class CommandResult<T> : CommandResult
    {
        /// <summary>
        /// The value returned by the operation if successful; otherwise default.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult{T}"/> class.
        /// </summary>
        /// <param name="value">The value returned by the operation.</param>
        /// <param name="isSuccess">True if the operation succeeded.</param>
        /// <param name="error">Error message if failed, otherwise null.</param>
        internal CommandResult(T? value, bool isSuccess, string? error)
            : base(isSuccess, error)
        {
            Value = value;
        }
    }
}