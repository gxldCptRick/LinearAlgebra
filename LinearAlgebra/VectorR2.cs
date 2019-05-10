using LinearAlgebra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class VectorR2 : Vector
    {
        private const double DegreesInRadians = 180;

        public VectorR2(IEnumerable<double> values) : base(values)
        {
            if (values.Count() != 2) throw new VectorException("Vector must have exactly two elements");
        }

        public VectorR2(params double[] values) : this(values as IEnumerable<double>)
        {
        }
        public VectorR2() : base(2)
        {
        }

        public VectorR2 RotateDegrees(double theta)
        {
            return RotateRadians(theta * Math.PI / DegreesInRadians);
        }

        public VectorR2 RotateRadians(double theta)
        {
            var matrix = new Matrix(
                new Vector(Math.Cos(theta), -Math.Sin(theta)),
                new Vector(Math.Sin(theta), Math.Cos(theta)));
            return this.Transform(matrix);
        }

        public VectorR2 Scale(double xScale, double yScale)
        {
            var matrix = new Matrix(
                new Vector(xScale, 0),
                new Vector(0, yScale));
            return this.Transform(matrix);
        }

        public new VectorR2 Transform(Matrix m)
        {
            var transformed = base.Transform(m);
            return new VectorR2(transformed);
        }


    }
}
