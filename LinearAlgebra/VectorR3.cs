﻿using LinearAlgebra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace LinearAlgebra
{
    public class VectorR3 : Vector
    {
        public static Matrix CreateRotationMatrixDegX(double deg)
        {
            return CreateRotationMatrixRadsX(deg * Math.PI / 180);
        }

        public static Matrix CreateRotationMatrixRadsX(double rads)
        {
            return new Matrix(
                new Vector(1, 0, 0),
                new Vector(0, Math.Cos(rads), -Math.Sin(rads)),
                new Vector(0, Math.Sin(rads), Math.Cos(rads)));
        }
        public static Matrix CreateRotationMatrixDegY(double deg)
        {
            return CreateRotationMatrixRadsY(deg * Math.PI / 180);
        }

        public static Matrix CreateProjectionMatrix(VectorR3 n)
        {
            return new Matrix(
                new Vector(1 - n.X * n.X, -n.X * n.Y, -n.X * n.Z),
                new Vector(-n.Y * n.X, 1 - n.Y * n.Y, -n.Y * n.Z),
                new Vector(-n.Z * n.X, -n.Z * n.Y, 1 - n.Z * n.Z));
        }

        public static Matrix CreateReflection(VectorR3 n)
        {
            return new Matrix(
                new Vector(1 - 2 * n.X * n.X, -2 * n.X * n.Y, -2 * n.X * n.Z),
                new Vector(-2 * n.Y * n.X, 1 - 2 * n.Y * n.Y, -2 * n.Y * n.Z),
                new Vector(-2 * n.Z * n.X, -2 * n.Z * n.Y, 1 - 2 * n.Z * n.Z)
                );
        }

        public static Matrix CreateRotationMatrixRadsY(double rads)
        {
            return new Matrix(
                new Vector(Math.Cos(rads), 0, Math.Sin(rads)),
                new Vector(0, 1, 0),
                new Vector(-Math.Sin(rads), 0, Math.Cos(rads)));
        }

        public static Matrix CreateRotationMatrixDegZ(double deg)
        {
            return CreateRotationMatrixRadsZ(deg * Math.PI / 180);
        }

        public static Matrix CreateRotationMatrixRadsZ(double rads)
        {
            return new Matrix(
                new Vector(Cos(rads), -Sin(rads), 0),
                new Vector(Sin(rads), Cos(rads), 0),
                new Vector(0, 0, 1));
        }

        public static Matrix CreateRotationMatrixAboutVectorDegs(double deg, VectorR3 about)
        {
            return CreateRotationMatrixAboutVectorRads(deg * PI / 180, about);
        }

        private static Matrix CreateRotationMatrixAboutVectorRads(double rads, VectorR3 about)
        {
            var cosineOfAngle = Cos(rads);
            var differenceFromOne = 1 - cosineOfAngle;
            var sineOfAngle = Sin(rads);

            return new Matrix(
                new Vector(
                    about.X * about.X * differenceFromOne + cosineOfAngle,
                    about.X * about.Y * differenceFromOne - about.Z * sineOfAngle,
                    about.X * about.Z * differenceFromOne + about.Y * sineOfAngle),
                new Vector(
                    about.X * about.Y * differenceFromOne + about.Z * sineOfAngle,
                    about.Y * about.Y * differenceFromOne + cosineOfAngle,
                    about.Y * about.Z * differenceFromOne - about.X * sineOfAngle),
                new Vector(
                    about.X * about.Z * differenceFromOne - about.Y * sineOfAngle,
                    about.Z * about.Y * differenceFromOne + about.X * sineOfAngle,
                    about.Z * about.Z * differenceFromOne + cosineOfAngle));
        }

        public static Matrix CreateSkewMatrixXAndY(double x, double y)
        {
            return new Matrix(
                new Vector(1, 0, x),
                new Vector(0, 1, y),
                new Vector(0, 0, 1)
                );
        }

        public static Matrix CreateSkewMatrixXAndZ(double x, double z)
        {
            return new Matrix(
                new Vector(1, x, 0),
                new Vector(0, 1, 0),
                new Vector(0, z, 1)
                );
        }

        public static Matrix CreateSkewMatrixZAndY(double y, double z)
        {
            return new Matrix(
                new Vector(1, 0, 0),
                new Vector(y, 1, 0),
                new Vector(z, 0, 1)
                );
        }

        /// <summary>
        /// This is the first element in the Vector called X
        /// </summary>
        public double X { get => this[0]; set => this[0] = value; }
        /// <summary>
        /// This is the second element in the Vector called Y
        /// </summary>
        public double Y { get => this[1]; set => this[1] = value; }
        /// <summary>
        /// This is the third element in the Vector called Z
        /// </summary>
        public double Z { get => this[2]; set => this[2] = value; }

        /// <summary>
        /// Creates the initial Vector as the zero vector in R3
        /// </summary>
        public VectorR3() : base(3)
        {
        }

        /// <summary>
        /// This Creates a Vector with the given values. It throws a VectorException if you don't specify exactly three values.
        /// </summary>
        /// <param name="args">the values that the vector will populate with</param>
        public VectorR3(params double[] args) : this(args as IEnumerable<double>)
        {
        }

        /// <summary>
        /// This Creates a Vector with the given values. It throws a VectorException if you don't specify exactly three values.
        /// </summary>
        /// <param name="values">the initial values of the vector</param>
        public VectorR3(IEnumerable<double> values) : base(values)
        {
            if (values.Count() != 3)
            {
                throw new VectorException("The VectorR3 must have exactly 3 values");
            }
        }


        public VectorR3 CrossProduct(VectorR3 third)
        {
            ValidateVector(third);
            return new VectorR3
            {
                X = Y * third.Z - third.Y * Z,
                Y = Z * third.X - third.Z * X,
                Z = X * third.Y - third.X * Y
            };
        }

        public static Matrix CreateAffineMatrix(int changeInX, int changeInY, int changeInZ)
        {
            return new Matrix(
                new Vector(1, 0, 0, changeInX),
                new Vector(0, 1, 0, changeInY),
                new Vector(0, 0, 1, changeInZ),
                new Vector(0, 0, 0, 1));
        }

        public VectorR3 AffineTranformation(int changeInX, int changeInY, int changeInZ)
        {
            return new VectorR3(RankUp().Transform(CreateAffineMatrix(changeInX, changeInY, changeInZ)).RankDown());
        }

        public VectorR3 ChainAffineTransformation(int changeInX, int changeInY, int changeInZ, params Matrix[] chaining)
        {
            var matrix = chaining.Select(m => m.Cushion())
                .Append(CreateAffineMatrix(changeInX, changeInY, changeInZ))
                .Reverse()
                .Aggregate((a, o) => a.DotMatrix(o));
            return new VectorR3(RankUp().Transform(matrix).RankDown());
        }
        public double AngleBetweenVectorsSin(VectorR3 otherVector)
        {
            return Math.Asin(CrossProduct(otherVector).Length / (Length * otherVector.Length));
        }
    }
}
