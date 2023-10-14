using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otus_9_factory.Sorters;


namespace Otus_9_factory.Tests;

[TestClass]
public class UnitTest
{
    private readonly int[] _inputArray = new int[]{ 34, 26, 56, 86, 0, 2 };
    private readonly int[] _expectedArray = new int[]{ 0, 2, 26, 34, 56, 86 };

    [TestMethod]
    public void InsertionSorterTest()
    {
        // Arrange
        ISorter sorter = new InsertionSorter();
    
        // Act
        int[] outputArray = sorter.Sort(_inputArray);

        // Assert
        CollectionAssert.AreEqual(_expectedArray, outputArray, "Incorrect result");
    }

    [TestMethod]
    public void MergeSorterTest()
    {
        // Arrange
        ISorter sorter = new MergeSorter();
    
        // Act
        int[] outputArray = sorter.Sort(_inputArray);

        // Assert
        CollectionAssert.AreEqual(_expectedArray, outputArray, "Incorrect result");
    }

    [TestMethod]
    public void SelectionSorterTest()
    {
        // Arrange
        ISorter sorter = new SelectionSorter();
    
        // Act
        int[] outputArray = sorter.Sort(_inputArray);

        // Assert
        CollectionAssert.AreEqual(_expectedArray, outputArray, "Incorrect result");
    }
}