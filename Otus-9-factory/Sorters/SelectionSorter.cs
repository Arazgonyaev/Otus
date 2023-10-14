using System;

namespace Otus_9_factory.Sorters;

public class SelectionSorter : ISorter
{
    public T[] Sort<T>(T[] items) where T : IComparable 
    {
        for (int i = 0; i < items.Length - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < items.Length; j++)
            {
                if (items[j].CompareTo(items[min]) < 0)
                {
                    min = j;
                }
            }

            (items[i], items[min]) = (items[min], items[i]);
        }

        return items;
    }
}
