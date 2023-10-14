using Otus_9_factory.Sorters;

namespace Otus_9_factory.Factories;

public class SorterFactory : ISorterFactory
{
    public ISorter CreateInsertionSorter()
    {
        return new InsertionSorter();
    }

    public ISorter CreateMergeSorter()
    {
        return new MergeSorter();
    }

    public ISorter CreateSelectionSorter()
    {
        return new SelectionSorter();
    }
}
