namespace LinearAlgebra
{
    public class MatrixChangedEventArgs
    {
        public Vector Changed { get; }
        public int RowId { get; }

        public MatrixChangedEventArgs(int index, Vector m)
        {
            RowId = index;
            Changed = new Vector(m);
        }

    }
}