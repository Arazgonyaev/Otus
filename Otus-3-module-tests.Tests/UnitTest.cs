using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_3_module_tests.Tests
{
    private double[] coefficients = new double[0];

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void NoRoots()
        {
            //Arrange
            coefficients = new double []{ 1, 0, 1};
        }
    }
}
