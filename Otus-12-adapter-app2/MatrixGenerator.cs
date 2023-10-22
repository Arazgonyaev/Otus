using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Otus_12_adapter_app2;

public class MatrixGenerator : IMatrixGenerator
{
    private readonly List<int[][]> matrices = new();
    private readonly Random random = new Random();

    public void Generate(int count)
    {
        matrices.Clear();

        int iCount = random.Next(2, 10);
        int jCount = random.Next(2, 10);

        for (int c = 0; c < count; c++)
        {
            int[][] matrix = new int[iCount][];
            for (int i = 0; i < iCount; i++)
            {
                matrix[i] = new int[jCount];
                for (int j = 0; j < jCount; j++)
                {
                    matrix[i][j] = random.Next(-10, 100);
                }
            }
            matrices.Add(matrix);
        }
    }

    public void SaveMatricesToFile(string fileName)
    {
        List<string> strMatrices = new();
        
        foreach (var matrix in matrices)
        {
            string[] rows = matrix.Select(x => string.Join("\t", x)).ToArray();
            strMatrices.Add(string.Join("\n", rows));
        }
        
        File.WriteAllText(fileName, string.Join("\n\n", strMatrices));
    }
}
