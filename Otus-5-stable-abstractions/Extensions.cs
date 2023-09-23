namespace Otus_5_stable_abstractions
{
    public static class Extensions
    {
        public static (int, int) Plus(this (int, int) a, (int, int) b) =>
            (a.Item1 + b.Item1, a.Item2 + b.Item2);
    }
}
