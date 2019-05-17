using FluentAssertions;
using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraTests
{
    [TestClass]
    public class LinearTransformationFactoryShould
    {
        [TestMethod]
        public void ReturnTheShearByAdjustingTheOtherValuesInColors()
        {
            var sut = new LinearTranformationFactory();
            var vectorFactory = new VectorFactory();
            sut.Shear(2, 1, 2).Should().BeEquivalentTo(new Matrix(vectorFactory.CreateVector(1, 0), vectorFactory.CreateVector(2,1)));
        }
    }
}
