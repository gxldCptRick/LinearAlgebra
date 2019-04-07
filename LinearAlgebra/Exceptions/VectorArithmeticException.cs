using System;

namespace LinearAlgebra.Exceptions
{
    /// <summary>
    /// Represents exceptions that occur due to invalid vector arithmetic.
    /// </summary>
    [Serializable]
    internal class VectorArithmeticException : VectorException
    {
        public VectorArithmeticException(string message) : base(message)
        {
        }

        public VectorArithmeticException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}