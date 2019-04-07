using System;

namespace LinearAlgebra.Exceptions
{
    /// <summary>
    /// Represents exceptions from matrix arithmetic.
    /// </summary> 
    [Serializable]
    public class MatrixArithmeticException : MatrixException
    {
        public MatrixArithmeticException(string message) : base(message)
        {
        }

        public MatrixArithmeticException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
