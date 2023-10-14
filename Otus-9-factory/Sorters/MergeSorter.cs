using System;
using System.Collections.Generic;
using System.Linq;

namespace Otus_9_factory.Sorters;

public class MergeSorter : ISorter
{
    public T[] Sort<T>(T[] items) where T : IComparable
    {
        if (items.Length <= 1)
            return items;

        List<T> left = new List<T>();
        List<T> right = new List<T>();

        int middle = items.Length / 2;
        for (int i = 0; i < middle; i++)
        {
            left.Add(items[i]);
        }
        for (int i = middle; i < items.Length; i++)
        {
            right.Add(items[i]);
        }

        left = Sort<T>(left.ToArray()).ToList();
        right = Sort<T>(right.ToArray()).ToList();

        return Merge(left, right).ToArray();
    }

    private static List<T> Merge<T>(List<T> left, List<T> right) where T : IComparable
    {
        List<T> result = new List<T>();

        while(left.Count > 0 || right.Count>0)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                if (left.First().CompareTo(right.First()) <= 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            else if(left.Count > 0)
            {
                result.Add(left.First());
                left.Remove(left.First());
            }
            else if (right.Count > 0)
            {
                result.Add(right.First());

                right.Remove(right.First());    
            }    
        }

        return result;
    }
}
