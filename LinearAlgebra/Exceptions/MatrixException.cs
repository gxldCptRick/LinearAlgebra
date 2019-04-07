using System;

namespace LinearAlgebra.Exceptions
{
    /// <summary>
    /// Represents general matrix exceptions.
    /// </summary>
    [Serializable]
    public class MatrixException : ArgumentException
    {
        public MatrixException(string message) : base(message)
        {
        }

        public MatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}