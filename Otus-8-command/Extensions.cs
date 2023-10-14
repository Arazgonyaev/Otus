using System;
using System.Linq;

namespace Otus_8_command;

public static class Extensions
{
    public static int[] Plus(this int[] a, int[] b) =>
        a.Zip(b, (x, y) => x + y).ToArray();

    public static int[] GetVelocityProjections(this int velocity, int direction, int directionsCount) => 
        new int[] 
        {
            (int)Math.Round(velocity * Math.Cos(2 * Math.PI * direction / directionsCount)),
            (int)Math.Round(velocity * Math.Sin(2 * Math.PI * direction / directionsCount))
        };
}
