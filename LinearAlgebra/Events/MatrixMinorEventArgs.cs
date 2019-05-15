namespace LinearAlgebra
{
    public class MatrixSubEventArgs
    {
        public Matrix Minor { get; }
        public MatrixSubEventArgs(Matrix minor)
        {
            Minor = minor;
        }
    }
}