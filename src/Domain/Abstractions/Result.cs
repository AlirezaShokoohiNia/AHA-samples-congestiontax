namespace AHA.CongestionTax.Domain.Abstractions
{
    namespace AHA.CongestionTax.Domain.Common
    {
        /// <summary>
        /// Represents the outcome of an operation, encapsulating success or failure state.
        /// </summary>
        public class Result
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
            /// Initializes a new instance of the <see cref="Result"/> class.
            /// </summary>
            /// <param name="isSuccess">True if the operation succeeded.</param>
            /// <param name="error">Error message if failed, otherwise null.</param>
            protected Result(bool isSuccess, string? error)
            {
                IsSuccess = isSuccess;
                Error = error;
            }

            /// <summary>
            /// Creates a successful result without a value.
            /// </summary>
            public static Result Success() => new(true, null);

            /// <summary>
            /// Creates a failed result with the specified error message.
            /// </summary>
            /// <param name="error">Error message describing the failure.</param>
            public static Result Failure(string error) => new(false, error);

            /// <summary>
            /// Creates a successful result with a value.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value returned by the operation.</param>
            public static Result<T> Success<T>(T value) => new(value, true, null);

            /// <summary>
            /// Creates a failed result with the specified error message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="error">Error message describing the failure.</param>
            public static Result<T> Failure<T>(string error) => new(default, false, error);
        }

        /// <summary>
        /// Represents the outcome of an operation that returns a value.
        /// </summary>
        /// <typeparam name="T">Type of the value returned.</typeparam>
        public class Result<T> : Result
        {
            /// <summary>
            /// The value returned by the operation if successful; otherwise default.
            /// </summary>
            public T? Value { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Result{T}"/> class.
            /// </summary>
            /// <param name="value">The value returned by the operation.</param>
            /// <param name="isSuccess">True if the operation succeeded.</param>
            /// <param name="error">Error message if failed, otherwise null.</param>
            internal Result(T? value, bool isSuccess, string? error)
                : base(isSuccess, error)
            {
                Value = value;
            }
        }
    }

}