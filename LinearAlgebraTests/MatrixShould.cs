using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinearAlgebraTests
{
    [TestClass]
    public class MatrixShould
    {
        [TestMethod]
        public void add_another_matrix_correctly()
        {
            var firstMatrix = CreateBaseMatrix(3, 3);
            firstMatrix[0][1] = 3;
            firstMatrix[1][1] = 2;
            var secondMatrix = CreateIdentityMatrix(3);
            var expectedValues = new List<Vector>()
            {
                new Vector(1, 3, 0),
                new Vector(0, 3, 0),
                new Vector(0, 0, 1)
            };
            var expected = new Matrix(expectedValues);
            var actual = firstMatrix.AddMatrix(secondMatrix);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_matrix_arithmetic_exception_if_add_is_called_with_matrix_that_is_differently_sized()
        {
            var values = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(3, 4)
            };
            var otherValues = new List<Vector>()
            {
                new Vector(1, 2, 3),
                new Vector(3, 4, 5)
            };
            var sut = new Matrix(values);
            var otherMatrix = new Matrix(otherValues);
            Assert.ThrowsException<MatrixArithmeticException>(() =>
            {
                sut.AddMatrix(otherMatrix);
            });
        }

        [TestMethod]
        public void scale_up_by_two_when_scale_called_with_two()
        {
            var sut = CreateIdentityMatrix(3);
            var expectedValues = new List<Vector>()
            {
                new Vector(2, 0, 0),
                new Vector(0, 2, 0),
                new Vector(0, 0, 2)
            };
            var expected = new Matrix(expectedValues);
            var actual = sut.Scale(2);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void multiply_two_square_matrices_correctly()
        {
            var firstSetValue = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(3, 4)
            };
            var secondSetValue = new List<Vector>()
            {
                new Vector(3, 4),
                new Vector(1, 2)
            };
            var firstMatrix = new Matrix(firstSetValue);
            var secondMatrix = new Matrix(secondSetValue);

            var expectedValues = new List<Vector>()
            {
                new Vector(5, 8),
                new Vector(13, 20)
            };
            var expected = new Matrix(expectedValues);
            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_three_by_two_correctly_with_a_two_by_two()
        {
            var firstSetValue = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(3, 4),
                new Vector(5, 6)
            };
            var secondSetValue = new List<Vector>()
            {
                new Vector(3, 4),
                new Vector(1, 2)
            };
            var firstMatrix = new Matrix(firstSetValue);
            var secondMatrix = new Matrix(secondSetValue);

            var expectedValues = new List<Vector>()
            {
                new Vector(5, 8),
                new Vector(13, 20),
                new Vector(21, 32)
            };
            var expected = new Matrix(expectedValues);
            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_three_by_two_correctly_with_a_two_by_three()
        {
            var firstSetValue = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(3, 4),
                new Vector(5, 6)
            };
            var secondSetValue = new List<Vector>()
            {
                new Vector(3, 4, 5),
                new Vector(1, 2, 6)
            };
            var firstMatrix = new Matrix(firstSetValue);
            var secondMatrix = new Matrix(secondSetValue);

            var expectedValues = new List<Vector>()
            {
                new Vector(5, 8, 17),
                new Vector(13, 20, 39),
                new Vector(21, 32, 61)
            };
            var expected = new Matrix(expectedValues);
            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_two_by_two_correctly_with_a_two_by_three()
        {
            var firstSetValue = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(3, 4),
            };
            var secondSetValue = new List<Vector>()
            {
                new Vector(3, 4, 5),
                new Vector(1, 2, 6)
            };
            var firstMatrix = new Matrix(firstSetValue);
            var secondMatrix = new Matrix(secondSetValue);

            var expectedValues = new List<Vector>()
            {
                new Vector(5, 8, 17),
                new Vector(13, 20, 39)
            };
            var expected = new Matrix(expectedValues);
            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void throw_matrix_arithmetic_exception_when_dot_called_with_matrix_that_has_a_height_different_then_our_width()
        {
            var firstValues = new List<Vector>()
            {
                new Vector(1, 2, 3),
                new Vector(2, 3, 4)
            };
            var secondValues = new List<Vector>()
            {
                new Vector(1, 2),
                new Vector(2, 3)
            };
            var firstMatrix = new Matrix(firstValues);
            var secondMatrix = new Matrix(secondValues);

            Assert.ThrowsException<MatrixArithmeticException>(() =>
            {
                firstMatrix.DotMatrix(secondMatrix);
            });
        }

        [TestMethod]
        public void be_inconsistent_if_last_row_is_false_statement_after_reduced_row_echelon_form()
        {
            var matrixData = new List<Vector>()
            {
                new Vector(1 ,0 , 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 0, 10)
            };
            var sut = new Matrix(matrixData);
            var expected = SystemState.Inconsistent;
            var actual = SystemState.Consistent;
            actual = sut.CalculateSystemState();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void be_dependent_if_last_row_is_all_zeros_after_reduced_row_echelon_form()
        {
            var matrixData = new List<Vector>()
            {
                new Vector(1, 0, 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 0, 0)
            };
            var sut = new Matrix(matrixData);
            var expected = SystemState.Dependent;
            var actual = SystemState.Inconsistent;
            sut.MatrixChanged += (s, e) => Console.WriteLine($"{e.Changed} - {DateTime.Now.ToString("MM/dd/yyyy")}");
            actual = sut.CalculateSystemState();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void be_consistent_if_last_row_is_not_all_zeros_after_reduced_row_echelon_form()
        {
            var matrixData = new List<Vector>()
            {
                new Vector(1, 0, 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 1, 0)
            };
            var sut = new Matrix(matrixData);
            var expected = SystemState.Consistent;
            var actual = SystemState.Dependent;
            actual = sut.CalculateSystemState();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void rotate_all_the_rows_and_columns_correctly_with_square_matrix()
        {
            var matrixData = new List<Vector>()
            {
                new Vector(1, 2, 3),
                new Vector(1, 2, 3),
                new Vector(1, 2, 3)
            };
            var expectedData = new List<Vector>()
            {
                new Vector(1, 1, 1),
                new Vector(2, 2, 2),
                new Vector(3, 3, 3)
            };
            var sut = new Matrix(matrixData);
            var expected = new Matrix(expectedData);
            var actual = sut.RotateMatrix();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_argument_null_exception_when_passed_in_null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new Matrix(null);
            });
        }

        [TestMethod]
        public void throw_matrix_exception_when_passed_in_an_enumerable_with_no_elements()
        {
            Assert.ThrowsException<MatrixException>(() =>
            {
                new Matrix(new List<Vector>());
            });
        }

        [TestMethod]
        public void have_correct_message_when_matrix_exception_is_thrown_for_no_vectors()
        {
            var exampleVectors = new List<Vector>();
            var expected = "You must pass in at least one vector.";
            var actual = "";

            try
            {
                new Matrix(exampleVectors);
            }
            catch (MatrixException e)
            {
                actual = e.Message;
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_matrix_exception_when_vectors_passed_in_conatains_null()
        {
            var exampleVectors = new List<Vector>()
            {
                null,
                new Vector(1, 2, 3)
            };

            Assert.ThrowsException<MatrixException>(() =>
            {
                new Matrix(exampleVectors);
            });
        }

        [TestMethod]
        public void have_correct_message_when_matrix_exception_is_thrown_for_null_vectors()
        {
            var exampleVectors = new List<Vector>()
            {
                null,
                new Vector(1d, 2d)
            };
            var expected = "No vector can be null.";
            var actual = "";

            try
            {
                new Matrix(exampleVectors);
            }
            catch (MatrixException e)
            {
                actual = e.Message;
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_matrix_exception_when_vectors_passed_in_are_different_lengths()
        {
            var exampleVectors = new List<Vector>()
            {
                new Vector(1d),
                new Vector(1d, 2d)
            };

            Assert.ThrowsException<MatrixException>(() =>
            {
                new Matrix(exampleVectors);
            });
        }

        [TestMethod]
        public void have_correct_message_when_matrix_exception_is_thrown_for_different_lengths()
        {
            var exampleVectors = new List<Vector>()
            {
                new Vector(1d),
                new Vector(1d, 2d)
            };
            var expected = "All the vectors must be the same size.";
            var actual = "";

            try
            {
                new Matrix(exampleVectors);
            }
            catch (MatrixException e)
            {
                actual = e.Message;
            }

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_argument_null_exception_when_null_passed_in_to_add()
        {
            var matrix = CreateBaseMatrix(3, 3);
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                matrix.AddMatrix(null);
            });
        }

        [TestMethod]
        public void should_correctly_do_a_single_step_matrix()
        {
            var initialValues = new List<Vector>()
            {
                new Vector(1, 0, 2),
                new Vector(1, 1, 2)
            };
            var sut = new Matrix(initialValues);
            var expectedValues = new List<Vector>()
            {
                new Vector(1, 0, 2),
                new Vector(0, 1, 0)
            };
            var expected = new Matrix(expectedValues);
            sut.TranformIntoReducedRowEchelonForm();

            Assert.AreEqual(expected, sut);
        }

        [TestMethod]
        public void should_correctly_do_a_solve_the_matrix_with_()
        {
            var initialValues = new List<Vector>()
            {
                new Vector(1, 2, 2),
                new Vector(1, 1, 3)
            };
            var sut = new Matrix(initialValues);
            var expectedValues = new List<Vector>()
            {
                new Vector(1, 0, 4),
                new Vector(0, 1, -1)
            };
            var expected = new Matrix(expectedValues);
            sut.TranformIntoReducedRowEchelonForm();

            Assert.AreEqual(expected, sut);
        }

        /// <summary>
        /// Creates a Matrix that is height x width large and full of nothing but zeros.
        /// </summary>
        /// <param name="width">The amount of columns in the vector</param>
        /// <param name="height">The amount of vectors in the matrix</param>
        /// <returns>Matrix that is width x height</returns>
        private Matrix CreateBaseMatrix(int width, int height)
        {
            var vectors = new List<Vector>();

            for (var i = 0; i < height; i++)
            {
                var vector = new Vector(width);
                vectors.Add(vector);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Creates an identity matrix of the given size.
        /// </summary>
        /// <param name="size">a postive integer</param>
        /// <returns>An identity matrix of size x size</returns>
        private Matrix CreateIdentityMatrix(int size)
        {
            var matrix = CreateBaseMatrix(size, size);

            for (var i = 0; i < size; i++)
            {
                matrix[i][i] = 1;
            }

            return matrix;
        }
    }
}
