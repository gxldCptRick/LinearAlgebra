using LinearAlgebra.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    /// <summary>
    /// Represents a mathematical vector of any size.
    /// </summary>
    public class Vector : IEquatable<Vector>, IEnumerable<double>
    {
        private readonly double[] values;

        /// <summary>
        /// The length or magnitude of the current vector.
        /// </summary>
        public double Length => Math.Sqrt(Dot(this));

        /// <summary>
        /// The amount of elements this vector can hold.
        /// </summary>
        public int MemberLength => values.Length;

        /// <summary>
        /// Creates the a vector with the given initial values.
        /// </summary>
        /// <param name="values">the starting values for the vector.</param>
        /// <exception cref="ArgumentNullException">Throws if values is null</exception>
        /// <exception cref="VectorException">Throws if values does not contain any elements</exception>
        public Vector(IEnumerable<double> values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            if (!values.Any()) throw new VectorException("values must contain at least one element"); 
            this.values = values.ToArray();
        }

        /// <summary>
        /// Creates a vector that can hold the given amount of values.
        /// </summary>
        /// <param name="size">The number of elements in the vector.</param>
        /// <exception cref="VectorException">Throws if size is not positive</exception>
        public Vector(int size)
        {
            if (size < 1) throw new VectorException("size must be a positive integer value.");
            values = new double[size];
        }

        /// <summary>
        /// Creates a vector with the given arbitrary amount of values.
        /// </summary>
        /// <param name="values">the initial values for the vector.</param>
        /// <exception cref="ArgumentNullException">Throws if values is null.   </exception>
        public Vector(params double[] values): this(values as IEnumerable<double>)
        {
        }

        /// <summary>
        /// The indexer for the vector to get the value at the given position
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index is out of range of vector</exception>
        public double this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }


        /// <summary>
        /// Creates a Vector that is the result of adding the current vector with the one passed in.
        /// </summary>
        /// <param name="v">vector to add with.</param>
        /// <returns>new vector that represents the addtion.</returns>
        /// <exception cref="ArgumentNullException">Throws if v is null</exception>
        /// <exception cref="VectorArithmeticException">Throws if MemberLength of v is different then this</exception>
        public Vector AddVector(Vector v)
        {
            ValidateVector(v);
            return new Vector(this.Zip(v, (v1, v2) => v1 + v2));
        }

        /// <summary>
        /// Creates a new vector that is scaled up by the given scalar value.
        /// </summary> 
        /// <param name="scalar">the value to scale by.</param>
        /// <returns>The vector that has been scaled.</returns>
        public Vector Scale(double scalar)
        {
            return new Vector(this.Select(v => v * scalar));
        }


        /// <summary>
        /// Calculates the dot product between two vectors.
        /// </summary>
        /// <param name="vector">other vector to calculate it</param>
        /// <returns>the dot product result</returns>
        /// <exception cref="VectorArithmeticException">Throws when the member length of the vector passed is diffenrent then this</exception>
        /// <exception cref="ArgumentNullException">Throws when vector is null</exception>
        public double Dot(Vector vector)
        {
            ValidateVector(vector);
            double product = 0;
            for (var i = 0; i < MemberLength; i++)
            {
                product += this[i] * vector[i];
            }
            return product;
        }

        /// <summary>
        /// Validate a vector parameter to meet the basic requirments and throws the appropriate exception if not.
        /// </summary>
        /// <param name="vector">vector paramter to check</param>
        private void ValidateVector(Vector vector)
        {
            if (vector is null) throw new ArgumentNullException(nameof(vector));
            if (vector.MemberLength != MemberLength) throw new VectorArithmeticException("Vectors must have a matching amount of members.");
        }

        /// <summary>
        /// Creates a string representation of the values in the vector in order and comma seperated.
        /// </summary>
        /// <returns>string of values in vector</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            var firstPassed = false;
            foreach (var item in this)
            {
                if (!firstPassed)
                {
                    builder.Append(item.ToString());
                    firstPassed = true;
                }
                else
                {
                    builder.Append($", {item}");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Calculates the hashcode by using all the values stored within the vector.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return values.Select(v => v.GetHashCode()).Aggregate(0, (a, next) => a ^ next);
        }

        /// <summary>
        /// Checks for equality between vectors by checking the values for a one to one match.
        /// </summary>
        /// <param name="obj">object to compare against</param>
        /// <returns>whether given object is a vector and its values match one to one</returns>
        public override bool Equals(object obj)
        {
            var isEqual = false;
            if (obj != null && obj is Vector v)
            {
                isEqual = Equals(v);
            }

            return isEqual;
        }

        /// <summary>
        /// Checks for equality between vector objects by checking the values for a one to one match.
        /// </summary>
        /// <param name="other">other vector to compare against.</param>
        /// <returns>whether a given vector has the same values as the current vector</returns>
        public bool Equals(Vector other)
        {
            if (other == this) return true;
            var isEqual = false;
            if (other != null && other.MemberLength == MemberLength)
            {
                var i = 0;
                for (; i < MemberLength && this[i] == other[i]; i++) ;
                isEqual = i == MemberLength;
            }
            return isEqual;
        }

        /// <summary>
        /// Creates an enumerator to enumerate the current vectors values in order.
        /// </summary>
        /// <returns>Ienumerator object that traverses vector in order.</returns>
        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>) values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}