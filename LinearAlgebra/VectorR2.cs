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
        public static Matrix CreateRotationMatrixDeg(double deg)
        {
            return CreateRotationMatrixRads(deg * Math.PI / 180);
        }

        public static Matrix CreateRotationMatrixRads(double rads)
        {
            return new Matrix(
                new Vector(Math.Cos(rads), -Math.Sin(rads)), 
                new Vector(Math.Sin(rads), Math.Cos(rads)));
        }

        public static Matrix CreateSkewMatrixX(double x)
        {
            return new Matrix(
                new Vector(1, x),
                new Vector(0, 1)
                );
        }

        public static Matrix CreateSkewMatrixY(double y)
        {
            return new Matrix(
                new Vector(1, 0),
                new Vector(y, 1)
                );
        }

        private const double DegreesInRadians = 180;
        public double Y => this[1];
        public double X => this[0];

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
            return this.Transform(CreateRotationMatrixRads(theta));
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
        public Matrix CreateAffineMatrix(int changeInX, int changeInY)
        {
            return new Matrix(
                new Vector(1, 0, changeInX),
                new Vector(0, 1, changeInY),
                new Vector(0, 0, 1));
        }

        public VectorR2 AffineTranslation(int changeInX, int changeInY)
        {
            return new VectorR2(RankUp().Transform(CreateAffineMatrix(changeInX, changeInY)).RankDown());
        }

        public VectorR2 ChainWithAffineTranslation(int changeInX, int changeInY, params Matrix[] m) 
        {
            var transformedMatrix = m.Select(matrix => matrix.Cushion())
                .Append(CreateAffineMatrix(changeInX, changeInY))
                .Reverse()
                .Aggregate((a,o) => a.DotMatrix(o));
            return new VectorR2(RankUp().Transform(transformedMatrix).RankDown());
        }

        
    }
}
