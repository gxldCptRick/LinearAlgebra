using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LinearAlgebraTests
{
    [TestClass]
    public class VectorShould
    {
        [TestMethod]
        public void correctly_add_two_vectors_of_equal_length()
        {
            var firstVector = new Vector(1, 2, 3, 4);
            var secondVector = new Vector(4, 3, 2, 1);
            var expected = new Vector(5, 5, 5, 5);
            var actual = firstVector.AddVector(secondVector);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void throw_vector_arithemtic_exception_if_add_called_with_vector_of_different_size()
        {
            var firstVector = new Vector(1, 2, 3);
            var secondVector = new Vector(1, 2, 3, 4);

            Assert.ThrowsException<VectorArithmeticException>(() => firstVector.AddVector(secondVector));
        }

        [TestMethod]
        public void calculate_the_correct_length_of_the_vector()
        {
            var expected = 5d;
            var actual = 0d;
            var sut = new Vector(3, 4);
            actual = sut.Length;

            Assert.AreEqual(expected, actual, .001);
        }

        [TestMethod]
        public void calculate_the_correct_dot_product_between_two_vectors()
        {
            var expected = 4;
            var actual = 0d;
            var v1 = new Vector(1, 2);
            var v2 = new Vector(2, 1);
            actual = v1.Dot(v2);

            Assert.AreEqual(expected, actual, .001);
        }

        [TestMethod]
        public void throws_vector_arithmetic_exception_if_dot_product_is_called_with_vector_of_different_length()
        {
            var firstVector = new Vector(1, 2, 3);
            var secondVector = new Vector(1, 2, 3, 4);
            Assert.ThrowsException<VectorArithmeticException>(() => firstVector.Dot(secondVector));
        }

        [TestMethod]
        public void scale_up_by_two_with_scalar_of_two()
        {
            var sut = new Vector(1, 1, 1, 1);
            var expected = new Vector(2, 2, 2, 2);
            var actual = sut.Scale(2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scale_down_by_two_with_scalar_of_one_half()
        {
            var sut = new Vector(2, 2, 2);
            var expected = new Vector(1, 1, 1);
            var actual = sut.Scale(.5);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void return_true_if_equals_called_with_a_vector_with_the_same_values()
        {
            var v1 = new Vector(1, 1, 1, 1);
            var v2 = new Vector(1, 1, 1, 1);
            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_vector_of_different_length()
        {
            var v1 = new Vector(1, 1, 1, 1);
            var v2 = new Vector(1, 1, 1);
            Assert.IsFalse(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_vector_of_same_length_but_different_values()
        {
            var v1 = new Vector(1, 1, 1, 1);
            var v2 = new Vector(2, 2, 2, 2);
            Assert.IsFalse(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_null()
        {
            var vector = new Vector(1, 2, 1, 2);
            Assert.IsFalse(vector.Equals(null));
        }

        [TestMethod]
        public void return_true_if_vector_passed_in_with_same_values_is_cast_to_object()
        {
            var v1 = new Vector(1, 1, 1);
            object v2 = new Vector(1, 1, 1);
            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_any_object_other_than_vector_is_passed_in_to_equals()
        {
            var v1 = new Vector(1, 1, 1);
            var other = new object();
            Assert.IsFalse(v1.Equals(other));
        }

        [TestMethod]
        public void throws_vector_exception_if_enumeration_passed_in_is_empty()
        {
            Assert.ThrowsException<VectorException>(() => new Vector(new List<double>()));
        }

        [TestMethod]
        public void throws_argument_null_exception_if_null_passed_in()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Vector(null));
        }

        [TestMethod]
        public void throws_vector_exception_if_size_is_negative()
        {
            Assert.ThrowsException<VectorException>(() => new Vector(-1));
        }

        [TestMethod]
        public void throws_vector_exception_if_size_is_zero()
        {
            Assert.ThrowsException<VectorException>(() => new Vector(0));
        }

        [TestMethod]
        public void be_equal_to_another_vector_given_some_rounding_error()
        {
            var v1 = new Vector(0, 0, 1.0010);
            var v2 = new Vector(0, 0, 1.0011);

            Assert.AreEqual(v1, v2);
        }

        [TestMethod]
        public void not_be_equal_to_another_vector_given_some_more_tight_rounding_error()
        {
            var v1 = new Vector(0, 0, 1.0010);
            var v2 = new Vector(0, 0, 1.0012);

            Assert.AreNotEqual(v1, v2);
        }
    }
}
