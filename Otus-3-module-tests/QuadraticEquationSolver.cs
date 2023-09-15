using System;
namespace Otus_3_module_tests
{
    public static class QuadraticEquationSolver
    {
        public static double[] Solve(double a, double b, double c, double e = 1e-5)
        {
            /*if (Math.Abs(a) <= e)
            {
                throw new ArgumentException("a равно 0");
            }

            var D = b * b - 4 * a * c;

            if (D < -e)
            {
                return new double[0];
            }
            else if (Math.Abs(D) <= e)
            {
                return new double[] { -b / (2 * a), -b / (2 * a) };
            }
            else // D > e
            {
                return new double[] { -b + Math.Sqrt(D)/ (2 * a), (-b - Math.Sqrt(D)) / (2 * a) };
            }*/
            return new double[0];
        }
    }
}
