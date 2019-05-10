using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LinearAlgebraTests
{
    [TestClass]
    public class VectorR3Should
    {
        [TestMethod]
        public void ThrowVectorExceptionIfParamsPassedInAreLessThanThree()
        {
            Assert.ThrowsException<VectorException>(() => 
            {
                new VectorR3(1,2);
            });
        }

        [TestMethod]
        public void ThrowVectorExceptionIfParamsPassedInAreMoreThanThree()
        {
            Assert.ThrowsException<VectorException>(() =>
            {
                new VectorR3(1, 2, 3, 4);
            });
        }

        [TestMethod]
        public void BuildFineIfThreeValuesArePassedIn()
        {
            var expected = new int[] { 1, 2, 3 };
            var actual = new VectorR3(1, 2, 3);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void CompareVectorsCorrectly()
        {
            var expected = new VectorR3(1, 2, 3);
            var actual = new VectorR3(1, 2, 3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateCorrectCrossVector()
        {
            var first = new VectorR3(-5, 1, 3);
            var second = new VectorR3(4, -13, 9);
            var cross = first.CrossProduct(second);
            Assert.AreEqual(0, first.Dot(cross), "The cross was not perpendicular to the first.");
            Assert.AreEqual(0, second.Dot(cross), "The cross was not perpendicular to the second.");
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var first = new VectorR3(2, 1, 0);
            var second = new VectorR3(2, 1, 1);
            System.Console.WriteLine(first.AngleBetweenVectorsSin(second));
        }
    }
}
