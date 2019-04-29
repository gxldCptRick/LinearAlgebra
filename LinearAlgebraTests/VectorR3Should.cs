using System;
using LinearAlgebra;
using LinearAlgebra.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
