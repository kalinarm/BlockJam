using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kimeria.Nyx
{
    namespace Ext
    {
        public static class ListExtensions
        {
            public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
            {
                System.Random rnd = new System.Random();
                return source.OrderBy<T, int>((item) => rnd.Next());
            }
        }
    }
}
