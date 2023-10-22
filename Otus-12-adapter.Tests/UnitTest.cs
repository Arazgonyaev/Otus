using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus_12_adapter_app1;
using Otus_12_adapter_app2;

namespace Otus_12_adapter.Tests;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void MatrixSummatorTest()
    {
        // Arrange
        string inputFileName = "files\\input_test1.txt";
        string outputFileName = "files\\output_test1.txt";
        File.WriteAllText(inputFileName, "2	4	5\n3	1	0\n\n3	1	0\n2	4	5");
        string expected = "5	5	5\n5	5	5";
    
        // Act
        MatrixSummator summator = new();
        summator.ReadMatricesFromFile(inputFileName);
        summator.Sum();
        summator.SaveResultMatrixToFile(outputFileName);

        // Assert
        string actual = File.ReadAllText(outputFileName);
        Assert.AreEqual(expected, actual, "Incorrect result");
    }

    [TestMethod]
    public void MatrixGeneratorTest()
    {
        // Arrange
        string outputFileName = "files\\output_test2.txt";
        int count = 3;
    
        // Act
        MatrixGenerator generator = new();
        generator.Generate(count);
        generator.SaveMatricesToFile(outputFileName);

        // Assert
        string[] matrices = File.ReadAllText(outputFileName).Split("\n\n");
        Assert.AreEqual(count, matrices.Length, "Incorrect matrices count");

        int iCount = -1;
        int jCount = -1;

        for (int i = 0; i < count; i++)
        {
            var matrix = ReadMatrix(matrices[i]);
            if (iCount < 0) iCount = matrix.Length;
            if (jCount < 0) jCount = matrix[0].Length;

            Assert.AreEqual(iCount, matrix.Length, "Generated matrices has various row count.");
            Assert.AreEqual(jCount, matrix[0].Length, "Generated matrices has various column count.");
        }
    }

    [TestMethod]
    public void MatrixSummatorAdaptorTest()
    {
        // Arrange
        string inputFileName = "files\\input_test3.txt";
        string outputFileName = "files\\output_test3.txt";
        File.WriteAllText(inputFileName, "2	4	5\n3	1	0\n\n3	1	0\n2	4	5");
        string expected = "5	5	5\n5	5	5";

        // Act
        MatrixSummatorAdaptor adaptor = new();
        adaptor.Sum(inputFileName, outputFileName);

        // Assert
        string actual = File.ReadAllText(outputFileName);
        Assert.AreEqual(expected, actual, "Incorrect result");
    }

    private int[][] ReadMatrix(string matrix) => 
        matrix.Split("\n").Select(x => x.Split("\t").Select(y => Convert.ToInt32(y)).ToArray()).ToArray();
}