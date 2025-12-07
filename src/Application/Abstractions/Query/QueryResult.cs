namespace AHA.CongestionTax.Application.Abstractions.Query
{
    /// <summary>
    /// Represents the outcome of a query operation in the application layer.
    /// </summary>
    public class QueryResult
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
        /// Initializes a new instance of the <see cref="QueryResult"/> class.
        /// </summary>
        /// <param name="isSuccess">True if the operation succeeded.</param>
        /// <param name="error">Error message if failed, otherwise null.</param>
        protected QueryResult(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Creates a successful result without a value.
        /// </summary>
        public static QueryResult Success() => new(true, null);

        /// <summary>
        /// Creates a failed result with the specified error message.
        /// </summary>
        /// <param name="error">Error message describing the failure.</param>
        public static QueryResult Failure(string error) => new(false, error);

        /// <summary>
        /// Creates a successful result with a value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">The value returned by the operation.</param>
        public static QueryResult<T> Success<T>(T value) => new(value, true, null);

        /// <summary>
        /// Creates a failed result with the specified error message.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="error">Error message describing the failure.</param>
        public static QueryResult<T> Failure<T>(string error) => new(default, false, error);
    }

    /// <summary>
    /// Represents the outcome of a query operation in the application layer.
    /// </summary>
    /// <typeparam name="T">Type of the read model returned.</typeparam>
    public class QueryResult<T> : QueryResult
    {
        /// <summary>
        /// The value returned by the operation if successful; otherwise default.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult{T}"/> class.
        /// </summary>
        /// <param name="value">The value returned by the operation.</param>
        /// <param name="isSuccess">True if the operation succeeded.</param>
        /// <param name="error">Error message if failed, otherwise null.</param>
        internal QueryResult(T? value, bool isSuccess, string? error)
            : base(isSuccess, error)
        {
            Value = value;
        }
    }
}