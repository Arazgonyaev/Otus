using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Otus_3_module_tests.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void NoRoots()
        {
            // Arrange
            double a = 1;
            double b = 0;
            double c = 1;

            // Act
            var roots = QuadraticEquationSolver.Solve(a, b, c);

            // Assert
            Assert.AreEqual(0, roots.Length, "Incorrect roots count");
        }

        [TestMethod]
        public void TwoRoots()
        {
            // Arrange
            double a = 1;
            double b = 0;
            double c = -1;

            // Act
            var roots = QuadraticEquationSolver.Solve(a, b, c);

            // Assert
            Assert.AreEqual(2, roots.Length, "Incorrect roots count");
            Assert.AreEqual(1, roots[0], "Incorrect root x1");
            Assert.AreEqual(-1, roots[1], "Incorrect root x2");
        }

        [TestMethod]
        public void OneRoot()
        {
            // Arrange
            double a = 1;
            double b = 2;
            double c = 1 + 1e-7;

            // Act
            var roots = QuadraticEquationSolver.Solve(a, b, c);

            // Assert
            Assert.AreEqual(2, roots.Length, "Incorrect roots count");
            Assert.AreEqual(-1, roots[0], "Incorrect root x1");
            Assert.AreEqual(-1, roots[1], "Incorrect root x2");
        }

        [TestMethod]
        public void AZero()
        {
            // Arrange
            double a = 1e-7;
            double b = 1;
            double c = 1;

            // Act
            Assert.ThrowsException<ArgumentException>(() =>
                QuadraticEquationSolver.Solve(a, b, c));
        }

        [TestMethod]
        public void IncorrectDoubles()
        {
            // Arrange
            double a = double.NaN;
            double b = 1;
            double c = 1;

            // Act
            Assert.ThrowsException<ArgumentException>(() =>
                QuadraticEquationSolver.Solve(a, b, c));

            // Arrange
            a = 1;
            b = double.NegativeInfinity;
            c = 1;

            // Act
            Assert.ThrowsException<ArgumentException>(() =>
                QuadraticEquationSolver.Solve(a, b, c));

            // Arrange
            a = 1;
            b = 1;
            c = double.PositiveInfinity;

            // Act
            Assert.ThrowsException<ArgumentException>(() =>
                QuadraticEquationSolver.Solve(a, b, c));
        }
    }
}
