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

        public MatrixException()
        {
        }

        protected MatrixException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}