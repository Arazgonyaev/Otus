using System;

namespace Otus_9_factory.Sorters;

public class InsertionSorter : ISorter
{
    public T[] Sort<T>(T[] items) where T : IComparable
    {
        for (int i = 0; i < items.Length - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (items[j - 1].CompareTo(items[j]) > 0)
                {
                    (items[j], items[j - 1]) = (items[j - 1], items[j]);
                }
            }
        }
        
        return items;
    }
}