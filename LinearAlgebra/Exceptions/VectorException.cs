using System;

namespace LinearAlgebra.Exceptions
{
    /// <summary>
    /// Represents general vector exceptions.
    /// </summary>
    [Serializable]
    public class VectorException : ArgumentException
    {
        public VectorException(string message) : base(message)
        {
        }

        public VectorException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
