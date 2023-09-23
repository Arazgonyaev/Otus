using System.Linq;

namespace Otus_5_stable_abstractions
{
    public static class Extensions
    {
        public static int[] Plus(this int[] a, int[] b) =>
            a.Zip(b, (x, y) => x + y).ToArray();
    }
}
