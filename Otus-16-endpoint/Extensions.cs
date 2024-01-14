namespace Otus_16_endpoint
{
    public static class Extensions
    {
        public static int[] Plus(this int[] a, int[] b) =>
            a.Zip(b, (x, y) => x + y).ToArray();
    }
}
