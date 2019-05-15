using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class LinearTranformationFactory
    {
        public static Matrix OrthgonalProjection(int size, int dimension)
        {
            if (dimension >= size) throw new ArgumentOutOfRangeException("dimension does not exist for the given size.");
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                if (i != dimension)
                {
                    vectors[i][i] = 1;
                }
            }
            return new Matrix(vectors);
        }

        public static Matrix Scale(int size, int scaleFactor)
        {
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                vectors[i][i] = scaleFactor;
            }
            return new Matrix(vectors);
        }

        public static Matrix Reflection(int size, int dimension)
        {
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                vectors[i][i] = dimension == i ? -1 : 1;
            }
            return new Matrix(vectors);
        }

        public static Matrix MatrixConcatenation(params Matrix[] concat)
        {
            return concat.Reverse().Aggregate((a, o) => a.DotMatrix(o));
        }
    }
}
