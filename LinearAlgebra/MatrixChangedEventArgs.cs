namespace LinearAlgebra
{
    public class MatrixChangedEventArgs
    {

        private readonly Matrix changed;

        public Matrix Changed => changed;

        public MatrixChangedEventArgs(Matrix m)
        {
            changed = new Matrix(m);
        }

    }
}