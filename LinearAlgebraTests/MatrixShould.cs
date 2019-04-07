using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LinearAlgebraTests
{
    [TestClass]
    public class MatrixShould
    {
        [TestMethod]
        public void add_another_matrix_correctly()
        {
            var firstMatrix = new Matrix(
                new Vector(0, 3, 0),
                new Vector(0, 2, 0),
                new Vector(0, 0, 0));

            var secondMatrix = new Matrix(
                new Vector(1, 0, 0),
                new Vector(0, 1, 0),
                new Vector(0, 0, 1));

            var expected = new Matrix(
                new Vector(1, 3, 0),
                new Vector(0, 3, 0),
                new Vector(0, 0, 1));

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
            Assert.ThrowsException<MatrixArithmeticException>(() => sut.AddMatrix(otherMatrix));
        }

        [TestMethod]
        public void scale_up_by_two_when_scale_called_with_two()
        {
            var sut = new Matrix(
                new Vector(1, 0, 0),
                new Vector(0, 1, 0),
                new Vector(0, 0, 1));

            var expected = new Matrix(
                new Vector(2, 0, 0),
                new Vector(0, 2, 0),
                new Vector(0, 0, 2));

            var actual = sut.Scale(2);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void multiply_two_square_matrices_correctly()
        {
            var firstMatrix = new Matrix(
                new Vector(1, 2),
                new Vector(3, 4));

            var secondMatrix = new Matrix(
                new Vector(3, 4),
                new Vector(1, 2));

            var expected = new Matrix(
                new Vector(5, 8),
                new Vector(13, 20));

            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_three_by_two_correctly_with_a_two_by_two()
        {
            var firstMatrix = new Matrix(
                new Vector(1, 2),
                new Vector(3, 4),
                new Vector(5, 6));

            var secondMatrix = new Matrix(
                new Vector(3, 4),
                new Vector(1, 2));

            var expected = new Matrix(
                new Vector(5, 8),
                new Vector(13, 20),
                new Vector(21, 32));

            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_three_by_two_correctly_with_a_two_by_three()
        {
            var firstMatrix = new Matrix(
                new Vector(1, 2),
                new Vector(3, 4),
                new Vector(5, 6));

            var secondMatrix = new Matrix(
                new Vector(3, 4, 5),
                new Vector(1, 2, 6));

            var expected = new Matrix(
                new Vector(5, 8, 17),
                new Vector(13, 20, 39),
                new Vector(21, 32, 61));

            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multiply_a_two_by_two_correctly_with_a_two_by_three()
        {
            var firstMatrix = new Matrix(
                new Vector(1, 2),
                new Vector(3, 4));

            var secondMatrix = new Matrix(
                new Vector(3, 4, 5),
                new Vector(1, 2, 6));

            var expected = new Matrix(
                new Vector(5, 8, 17),
                new Vector(13, 20, 39));

            var actual = firstMatrix.DotMatrix(secondMatrix);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void throw_matrix_arithmetic_exception_when_dot_called_with_matrix_that_has_a_height_different_then_our_width()
        {

            var firstMatrix = new Matrix(
                new Vector(1, 2, 3),
                new Vector(2, 3, 4));

            var secondMatrix = new Matrix(
                new Vector(1, 2),
                new Vector(2, 3));

            Assert.ThrowsException<MatrixArithmeticException>(() => firstMatrix.DotMatrix(secondMatrix));
        }

        [TestMethod]
        public void be_inconsistent_if_last_row_is_false_statement_after_reduced_row_echelon_form()
        {
            var sut = new Matrix(
                new Vector(1, 0, 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 0, 10));

            var expected = SystemState.Inconsistent;
            var actual = SystemState.Consistent;

            actual = sut.CalculateSystemState();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void be_dependent_if_last_row_is_all_zeros_after_reduced_row_echelon_form()
        {
            var sut = new Matrix(
                new Vector(1, 0, 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 0, 0));

            var expected = SystemState.Dependent;
            var actual = SystemState.Inconsistent;

            // for debugging purposes only
            sut.MatrixChanged += (s, e) => Debug.WriteLine($"{e.Changed} - {DateTime.Now.ToString("MM/dd/yyyy")}");

            actual = sut.CalculateSystemState();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void be_consistent_if_last_row_is_not_all_zeros_after_reduced_row_echelon_form()
        {
            var sut = new Matrix(
                new Vector(1, 0, 0, 2),
                new Vector(0, 1, 0, 3),
                new Vector(0, 0, 1, 0));

            var expected = SystemState.Consistent;
            var actual = SystemState.Dependent;

            actual = sut.CalculateSystemState();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void rotate_all_the_rows_and_columns_correctly_with_square_matrix()
        {
            var sut = new Matrix(
                new Vector(1, 2, 3),
                new Vector(1, 2, 3),
                new Vector(1, 2, 3));

            var expected = new Matrix(
                new Vector(1, 1, 1),
                new Vector(2, 2, 2),
                new Vector(3, 3, 3));

            var actual = sut.RotateMatrix();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void rotate_matrix_correctly_when_width_is_greater_then_height()
        {
            var sut = new Matrix(
                new Vector(1, 2, 3),
                new Vector(3, 2, 1));

            var expected = new Matrix(
                new Vector(1, 3),
                new Vector(2, 2),
                new Vector(3, 1));

            var actual = sut.RotateMatrix();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void rotate_matrix_correctly_when_height_is_greater_than_width()
        {
            var sut = new Matrix(
                new Vector(1, 2, 3),
                new Vector(3, 2, 1),
                new Vector(4, 5, 6),
                new Vector(6, 5, 4));

            var expected = new Matrix(
                new Vector(1, 3, 4, 6),
                new Vector(2, 2, 5, 5),
                new Vector(3, 1, 6, 4));

            var actual = sut.RotateMatrix();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_argument_null_exception_when_passed_in_null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Matrix(null));
        }

        [TestMethod]
        public void throw_matrix_exception_when_passed_in_an_enumerable_with_no_elements()
        {
            Assert.ThrowsException<MatrixException>(() => new Matrix(new List<Vector>()));
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
        public void throw_matrix_exception_when_vectors_passed_in_contains_null()
        {
            Assert.ThrowsException<MatrixException>(() => new Matrix(
                null,
                new Vector(1, 2, 3)));
        }

        [TestMethod]
        public void have_correct_message_when_matrix_exception_is_thrown_for_null_vectors()
        {
            var expected = "No vector can be null.";
            var actual = "";

            try
            {
                new Matrix(
                    null,
                    new Vector(1d, 2d));
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
            Assert.ThrowsException<MatrixException>(() => new Matrix(
                new Vector(1d),
                new Vector(1d, 2d)));
        }

        [TestMethod]
        public void have_correct_message_when_matrix_exception_is_thrown_for_different_lengths()
        {
            var expected = "All the vectors must be the same size.";
            var actual = "";

            try
            {
                new Matrix(
                    new Vector(1d),
                    new Vector(1d, 2d));
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
            var matrix = new Matrix(
                new Vector(1, 2, 3),
                new Vector(3, 2, 1));

            Assert.ThrowsException<ArgumentNullException>(() => matrix.AddMatrix(null));
        }

        [TestMethod]
        public void should_correctly_do_a_single_step_matrix()
        {
            var sut = new Matrix(
                new Vector(1, 0, 2),
                new Vector(1, 1, 2));

            var expected = new Matrix(
                new Vector(1, 0, 2),
                new Vector(0, 1, 0));

            sut.TranformIntoReducedRowEchelonForm();

            Assert.AreEqual(expected, sut);
        }

        [TestMethod]
        public void should_correctly_do_a_solve_the_matrix_with_()
        {
            var sut = new Matrix(
                new Vector(1, 2, 2),
                new Vector(1, 1, 3));

            var expected = new Matrix(
                new Vector(1, 0, 4),
                new Vector(0, 1, -1));

            sut.TranformIntoReducedRowEchelonForm();

            Assert.AreEqual(expected, sut);
        }
    }
}
