using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Otus_12_adapter_app1;

public class MatrixSummator : IMatrixSummator
{
    private int[][] matrixA;
    private int[][] matrixB;
    private int[][] matrixAB;

    public void ReadMatricesFromFile(string fileName)
    {
        string[] matrices = File.ReadAllText(fileName).Split("\n\n");
        if (matrices.Length != 2) throw new InvalidOperationException("Input file must have 2 matrices.");

        List<int[][]> result = new();
        foreach (var matrix in matrices)
        {
            string[] rows = matrix.Split("\n");
            result.Add(rows.Select(x => x.Split("\t").Select(y => Convert.ToInt32(y)).ToArray()).ToArray());
        }
        
        matrixA = result[0];
        matrixB = result[1];
    }
    
    public void Sum()
    {
        if (matrixA.Length != matrixB.Length) throw new InvalidOperationException ("Matrices must be equal size.");

        List<int[]> result = new();
        for (int i = 0; i < matrixA.Length; i++)
        {
            if (matrixA[i].Length != matrixB[i].Length) throw new InvalidOperationException ("Matrices must be equal size.");
            int[] row = new int[matrixA[i].Length];

            for (int j = 0; j< matrixA[i].Length; j++)
            {
                row[j] = matrixA[i][j] + matrixB[i][j];
            }
            result.Add(row);
        }

        matrixAB = result.ToArray();
    }

    public void SaveResultMatrixToFile(string fileName)
    {
        string[] rows = matrixAB.Select(x => string.Join("\t", x)).ToArray();
        File.WriteAllText(fileName, string.Join("\n", rows));
    }
}
