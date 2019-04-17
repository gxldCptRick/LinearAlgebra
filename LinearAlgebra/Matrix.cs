using LinearAlgebra.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinearAlgebra
{
    /// <summary>
    /// Represents a mathematical matrix.
    /// </summary>
    public class Matrix : IEquatable<Matrix>, IEnumerable<Vector>
    {
        private Vector[] values;

        /// <summary>
        /// Gets the vector row at the given index in the matrix.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>vector row at index.</returns>
        /// <exception cref="IndexOutOfRangeException">Throws if index is outside the matrix's values.</exception>
        public Vector this[int index]
        {
            get => values[index];
            private set
            {
                values[index] = value;
                MatrixChanged?.Invoke(this, new MatrixChangedEventArgs(index, value));
            }
        }
        /// <summary>
        /// The amount of columns the matrix consists of.
        /// </summary>
        public int Width => this[0].MemberLength;

        /// <summary>
        /// The amount of rows the matrix consists of.
        /// </summary>
        public int Height => values.Length;

        /// <summary>
        /// Event to fire whenever change the state of the matrix.
        /// </summary>
        public event EventHandler<MatrixChangedEventArgs> MatrixChanged;
        
        /// <summary>
        /// Event to fire whenever you create a sub matrix.
        /// </summary>
        public event EventHandler<MatrixSubEventArgs> MatrixSubCreated;

        /// <summary>
        /// Creates a Matrix with the given values with row vectors of the given column count.
        /// </summary>
        /// <param name="values">the values that will be in your matrix</param>
        /// <param name="columnCount">the length of the row vector</param>
        public Matrix(IEnumerable<double> values, int columnCount)
        {
            var totalElements = values.Count();
            if (totalElements % columnCount != 0) throw new MatrixException("All the vectors must have an equal length");
            var totalRows = totalElements / columnCount;
            this.values = new Vector[totalRows];
            for (int offset = 0; offset < this.values.Length; offset++)
            {
                this.values[offset] = new Vector(values.Skip(offset * columnCount).Take(columnCount));
            }
        }

        /// <summary>
        /// Creates the matrix with the given amount of vectors.
        /// </summary>
        /// <param name="vectors">the vector rows that will make up the matrix.</param>
        /// <exception cref="MatrixException">
        /// Throws the matrix exception for a ton of reasons:
        /// If the are no vectors in the enumeration,
        /// If the vectors in the enumeration have different MemberLengths,
        /// If their are any null vectors in the enumeration.
        /// </exception>
        /// <exception cref="ArgumentNullException">Throws if vectors is null</exception>
        public Matrix(IEnumerable<Vector> vectors)
        {
            if (vectors is null) throw new ArgumentNullException(nameof(vectors));
            if (vectors.Count() == 0) throw new MatrixException("You must pass in at least one vector.");
            if (vectors.Any(v => v is null)) throw new MatrixException("No vector can be null.");
            if (vectors.GroupBy(c => c.MemberLength).Count() != 1) throw new MatrixException("All the vectors must be the same size.");
            values = vectors.ToArray();
        }

        /// <summary>
        /// Creates a matrix with the given vector values.
        /// Refer to the IEnumerable constructor for more information about cases.
        /// </summary>
        /// <param name="vectors"></param>
        public Matrix(params Vector[] vectors) : this(vectors as IEnumerable<Vector>)
        { }

        /// <summary>
        /// Creates a new matrix that is the result of adding the current matrix with the given matrix.
        /// </summary>
        /// <param name="other">the matrix to add with.</param>
        /// <returns>The result of the addition between the two matrices.</returns>
        /// <exception cref="ArgumentNullException">throws if other is null.</exception>
        /// <exception cref="MatrixArithmeticException">throws if other matrix is different size than current matrix.</exception>
        public Matrix AddMatrix(Matrix other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (other.Width != Width || other.Height != Height) throw new MatrixArithmeticException("other Matrix must be of the same size.");
            return new Matrix(this.Zip(other, (v1, v2) => v1.AddVector(v2)));
        }

        /// <summary>
        /// Creates a new matrix that is the result of scaling up the matrix with the given scalar values.
        /// </summary>
        /// <param name="scalar">the amount that you want to scale the matrix by.</param>
        /// <returns>The result of the scale operation.</returns>
        public Matrix Scale(double scalar)
        {
            return new Matrix(this.Select(v => v.Scale(scalar)));
        }

        /// <summary>
        /// Creates a matrix that is the result of multiplying the current matrix with the given matrix.
        /// </summary>
        /// <param name="other">the other matrix to multiply with.</param>
        /// <returns>A new matrix that is the result of the multiplication.</returns>
        /// <exception cref="ArgumentNullException">throws if other is null.</exception>
        /// <exception cref="MatrixArithmeticException">throws if other matrix Height is different than current matrix's Width.</exception>
        public Matrix DotMatrix(Matrix other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (Width != other.Height) throw new MatrixArithmeticException("The other matrix must have a height equal to the width of this matrix.");
            var rotated = other.RotateMatrix();
            var vectors = new List<Vector>();
            for (var i = 0; i < Height; i++)
            {
                var vector = new Vector(rotated.Height);
                for (var j = 0; j < rotated.Height; j++)
                {
                    vector[j] = this[i].Dot(rotated[j]);
                }
                vectors.Add(vector);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Creates a new Matrix in which the vectors are the columns instead of the rows.
        /// </summary>
        /// <returns>The rotated matrix.</returns>
        public Matrix RotateMatrix()
        {
            var vectors = new List<Vector>();
            for (var i = 0; i < Width; i++)
            {
                var vector = new Vector(Height);
                for (var j = 0; j < Height; j++)
                {
                    vector[j] = this[j][i];
                }
                vectors.Add(vector);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Calculates the state of the system represented by the current Matrix.
        /// </summary>
        /// <returns>The SystemState enum reprsenting the state.</returns>
        public SystemState CalculateSystemState()
        {
            TranformIntoReducedRowEchelonForm();
            var solution = SystemState.Inconsistent;
            if (this[Height - 1][Width - 2] == 1)
            {
                solution = SystemState.Consistent;
            }
            else if (this[Height - 1][Width - 1] == 0)
            {
                solution = SystemState.Dependent;
            }
            return solution;

        }

        /// <summary>
        /// Reduces each vector rows values until they fit into the reduced row echelon form.
        /// </summary>
        public void TranformIntoReducedRowEchelonForm()
        {
            for (var i = 0; i < Height && i < (Width - 1); i++)
            {
                if (this[i][i] != 0)
                {
                    this[i] = this[i].Scale(1 / this[i][i]);
                }
                var row = this[i];
                for (var j = 0; j < Height; j++)
                {
                    var item = this[j];
                    if (item != row && item[i] != 0)
                    {
                        var scaledRow = row;
                        if (item[i] < 0 && scaledRow[i] < 0 || item[i] > 0 && scaledRow[i] > 0)
                        {
                            scaledRow = scaledRow.Scale(-1);
                        }

                        this[j] = item.AddVector(scaledRow.Scale(Math.Abs(item[i])));
                    }
                }
            }
        }

        public bool IsSquare { get => Height == Width; }

        public double CalculateDeterminant()
        {
            if(!IsSquare)
                throw new MatrixArithmeticException("Matrix must be square");
            return CalculateDeterminant(this);
        }

        private double CalculateDeterminant(Matrix m)
        {
            var determinate = 0d;
            if (m.Height == 2)
            {
                determinate = m[0][0] * m[1][1] - m[0][1] * m[1][0];
            }
            else if (m.Height == 1)
            {
                determinate = m[0][0];
            }
            else
            {
                var y = 0;
                for (var x = 0; x < m.Width; x++)
                {
                    var subMatrix = m.BuildSubMatrix(x, y);
                    MatrixSubCreated?.Invoke(this, new MatrixSubEventArgs(subMatrix));
                    determinate += m[y][x] * CalculateDeterminant(subMatrix) * CalculateSecretSign(x, y, m.Width);
                }

            }


            return determinate;
        }

        private double CalculateSecretSign(int x, int y, int width)
        {
            return (x + y * width) % 2 == 0 ? 1 : -1;  
        }

        public Matrix BuildSubMatrix(int x, int y)
        {
            var matrixValues = new List<double>(Width * Height);
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    if (row != y && column != x)
                    {
                        matrixValues.Add(this[row][column]);
                    }
                }
            }
            return new Matrix(matrixValues, Width - 1);
        }

        /// <summary>
        /// Creates a new vector containing the variable answers to the matrix
        /// </summary>
        /// <param name="answers"></param>
        /// <returns>Answers to each number passed in</returns>
        public Vector SolveEquationUsingDeterminant(Vector answers)
        {
            if (answers.Length != Height)
                throw new MatrixArithmeticException("The vector must have equal numbers to the height of this matrix.");
            var changingMatrix = this.values.ToArray();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < answers.Length; j++)
                {
                    changingMatrix[j][i] = answers[j];
                }
            }
            //TODO: No solution and Infinitely many should return Nan
            throw new NotImplementedException("Waiting for method that finds the determinant to find return values");
        }

        /// <summary>
        /// Generates the hashcode using all the values stored in the matrix.
        /// </summary>
        /// <returns>the calculated hashcode.</returns>
        public override int GetHashCode()
        {
            return values.Select(v => v.GetHashCode()).Aggregate(0, (a, next) => a ^ next);
        }

        /// <summary>
        /// Compares the current matrix to the obj to make sure they both have the same values.
        /// </summary>
        /// <param name="obj">object that will be compared against</param>
        /// <returns>Whether or not the obj is a Matrix and has the same values as the current Matrix</returns>
        public override bool Equals(object obj)
        {
            var isEqual = false;
            if (obj != null && obj is Matrix m)
            {
                isEqual = Equals(m);
            }
            return isEqual;
        }

        /// <summary>
        /// Checks to see if the given matrix has the same values as the current matrix.
        /// </summary>
        /// <param name="other">other matrix to compare against</param>
        /// <returns>Whether or not both matrices have the same values.</returns>
        public bool Equals(Matrix other)
        {
            if (other == this) return true;
            var isEqual = false;
            if (other != null && other.Height == Height && other.Width == Width)
            {
                var i = 0;
                for (; i < Height && this[i].Equals(other[i]); i++) ;
                isEqual = i == Height;
            }
            return isEqual;
        }

        /// <summary>
        /// Creates a string representing the matrix and all its data.
        /// </summary>
        /// <returns>the string form of the matrix data</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var item in this)
            {
                builder.Append($"|{item}|\n");
            }
            return builder.ToString().Trim();
        }


        /// <summary>
        /// Creates an Enumerator for getting each row of the matrix.
        /// </summary>
        /// <returns>An Enumerator that will traverse the rows of the matrix in order.</returns>
        public IEnumerator<Vector> GetEnumerator()
        {
            return ((IEnumerable<Vector>)values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
