using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus_9_factory.Factories;
using Otus_9_factory.Sorters;

// Реализация на основе абстрактной фабрики

MainUsingFactory(args);

// Дополнительное решение: реализация на основе IoC

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SelectionSorter>();
builder.Services.AddSingleton<InsertionSorter>();
builder.Services.AddSingleton<MergeSorter>();

using IHost host = builder.Build();

MainUsingIoc(host.Services, args);

//

static void MainUsingFactory(string[] args)
{
    (string sortType, string inputFileName, string outputFileName) = ParseArgsExitOnIncorrect(args);
    
    int[] inputArray = GetArrayFromFile(inputFileName);

    ISorter sorter = GetSorterFromFactory(sortType);

    int[] outputArray = sorter.Sort(inputArray);

    SaveArrayToFile(outputFileName, sortType, outputArray);
}

static void MainUsingIoc(IServiceProvider hostProvider, string[] args)
{
    (string sortType, string inputFileName, string outputFileName) = ParseArgsExitOnIncorrect(args);
    
    int[] inputArray = GetArrayFromFile(inputFileName);

    ISorter sorter = GetSorterFromIoc(hostProvider, sortType);

    int[] outputArray = sorter.Sort(inputArray);

    SaveArrayToFile(outputFileName, sortType, outputArray);
}

static (string sortType, string inputFileName, string outputFileName) ParseArgsExitOnIncorrect(string[] args)
{
    if (args.Length == 3)
        return (args[0], args[1], args[2]);
        
    Console.WriteLine("Three arguments must be provided: sort type (selectionSort, insertionSort, mergeSort), " + 
        "input file name, output file name.");
    
    Environment.Exit(0);
    return (null, null, null); // fake return
}

static ISorter GetSorterFromIoc(IServiceProvider hostProvider, string sortType)
{
    return sortType switch
    {
        "selectionSort" => hostProvider.GetRequiredService<SelectionSorter>(),
        "insertionSort" => hostProvider.GetRequiredService<InsertionSorter>(),
        "mergeSort" => hostProvider.GetRequiredService<MergeSorter>(),
        _ => throw new InvalidOperationException("Incorrect sort type."),
    };
}

static ISorter GetSorterFromFactory(string sortType)
{
    ISorterFactory factory = new SorterFactory();
    
    return sortType switch
    {
        "selectionSort" => factory.CreateSelectionSorter(),
        "insertionSort" => factory.CreateInsertionSorter(),
        "mergeSort" => factory.CreateMergeSorter(),
        _ => throw new InvalidOperationException("Incorrect sort type."),
    };
}

static int[] GetArrayFromFile(string fileName)
{
    return File.ReadAllText(fileName).Split("\n").Select(x=> Convert.ToInt32(x)).ToArray();
}

static void SaveArrayToFile(string fileName, string title, int[] intArray)
{
    File.WriteAllText(fileName, $"{title}\n\n{string.Join("\n", intArray)}");
}