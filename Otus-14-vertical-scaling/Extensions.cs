using System.Linq;

namespace Otus_14_vertical_scaling
{
    public static class Extensions
    {
        public static int[] Plus(this int[] a, int[] b) =>
            a.Zip(b, (x, y) => x + y).ToArray();
    }
}
