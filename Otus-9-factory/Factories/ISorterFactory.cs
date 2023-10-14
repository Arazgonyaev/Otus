using Otus_9_factory.Sorters;

namespace Otus_9_factory.Factories;

public interface ISorterFactory
{
    ISorter CreateInsertionSorter(); 
    ISorter CreateMergeSorter(); 
    ISorter CreateSelectionSorter(); 
}
