using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinearAlgebraTests
{
    [TestClass]
    public class VectorShould
    {
        [TestMethod]
        public void calculate_the_correct_length_of_the_vector()
        {
            var expected = 5d;
            var actual = 0d;
            var sut = new Vector(new double[] { 3, 4 });
            actual = sut.Length;

            Assert.AreEqual(expected, actual, .001);
        }

        [TestMethod]
        public void calculate_the_correct_dot_product_between_two_vectors()
        {
            var expected = 4;
            var actual = 0d;
            var v1 = new Vector(new double[] { 1, 2 });
            var v2 = new Vector(new double[] { 2, 1 });
            actual = v1.Dot(v2);

            Assert.AreEqual(expected, actual, .001);
        }


        [TestMethod]
        public void scale_up_by_two_with_scalar_of_two()
        {
            var sut = new Vector(new double[] { 1, 1, 1, 1 });
            var expected = new Vector(new double[] { 2, 2, 2, 2 });
            var actual = sut.Scale(2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scale_down_by_two_with_scalar_of_one_half()
        {
            var sut = new Vector(new double[] { 2, 2, 2 });
            var expected = new Vector(new double[] { 1, 1, 1 });
            var actual = sut.Scale(.5);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void return_true_if_equals_called_with_a_vector_with_the_same_values()
        {
            var v1 = new Vector(new double[] { 1, 1, 1, 1 });
            var v2 = new Vector(new double[] { 1, 1, 1, 1 });
            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_vector_of_different_length()
        {
            var v1 = new Vector(new double[] { 1, 1, 1, 1 });
            var v2 = new Vector(new double[] { 1, 1, 1 });
            Assert.IsFalse(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_vector_of_same_length_but_different_values()
        {
            var v1 = new Vector(new double[] { 1, 1, 1, 1 });
            var v2 = new Vector(new double[] { 2, 2, 2, 2 });
            Assert.IsFalse(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_equals_called_with_null()
        {
            var vector = new Vector(new double[] { 1, 2, 1, 2 });
            Assert.IsFalse(vector.Equals(null));
        }

        [TestMethod]
        public void return_true_if_vector_passed_in_with_same_values_is_cast_to_object()
        {
            var v1 = new Vector(new double[] { 1, 1, 1 });
            object v2 = new Vector(new double[] { 1, 1, 1 });
            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void return_false_if_any_object_other_than_vector_is_passed_in_to_equals()
        {
            var v1 = new Vector(new double[] { 1, 1, 1 });
            var other = new object();
            Assert.IsFalse(v1.Equals(other));
        }
    }
}
