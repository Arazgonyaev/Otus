﻿using System;

namespace Otus_3_module_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var roots = QuadraticEquationSolver.Solve(1, 2, 1+1e-7);
            Console.WriteLine(roots.Length == 0
                ? "Корней нет"
                : $"x1: {roots[0]}, x2: {roots[1]}");
        }
    }
}
