using System;

namespace Otus_9_factory.Sorters;

public interface ISorter
{
    T[] Sort<T>(T[] items) where T : IComparable;
}
