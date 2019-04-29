using LinearAlgebra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class VectorR3 : Vector
    {
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
            if (values.Count() != 3) throw new VectorException("The VectorR3 must have exactly 3 values");
        }


        public VectorR3 CrossProduct(VectorR3 third)
        {
            ValidateVector(third);
            return new VectorR3
            {
                X = this.Y * third.Z - third.Y * this.Z,
                Y = this.Z * third.X - third.Z * this.X,
                Z = this.Y * third.X - third.Y * this.X
            };
        }

        public double AngleBetweenVectorsSin(VectorR3 otherVector) => Math.Asin(this.CrossProduct(otherVector).Length/(this.Length * otherVector.Length));
          
        
    }
}
