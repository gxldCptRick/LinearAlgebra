using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class LinearTranformationFactory
    {
        public Matrix OrthgonalProjection(int size, int dimension)
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

        public Matrix Scale(int size, int dimension, double scale)
        {
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                vectors[i][i] = dimension == i ? scale : 1;
            }
            return new Matrix(vectors);
        }

        public Matrix Shear(int size, int dimension, double shear)
        {
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                vectors[i][dimension] = shear;
                vectors[i][i] = 1;
            }
            return new Matrix(vectors);
        }

        public Matrix Reflection(int size, int dimension)
        {
            var vectors = new Vector[size];
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = new Vector(size);
                vectors[i][i] = dimension == i ? -1 : 1;
            }
            return new Matrix(vectors);
        }

        public Matrix ConcatMatrices(params Matrix[] concat)
        {
            return concat.Reverse().Aggregate((a, o) => a.DotMatrix(o));
        }
    }
}
