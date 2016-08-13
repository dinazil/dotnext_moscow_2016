using System.Linq;
using Utilities;

namespace Boxing
{
    class Program
    {
        private const int Elements = 10000000;

        static void Main(string[] args)
        {
            Measurements.Measure("Find struct",
                () => Enumerable.Range(0, Elements)
                .Select(i => new Number { Value = i })
                .ToList(),
                list => list.IndexOf(list.Last())
            );

            Measurements.Measure("Find equatable struct",
                () => Enumerable.Range(0, Elements)
                .Select(i => new ENumber { Value = i })
                .ToList(),
                list => list.IndexOf(list.Last())
            );
        }
    }
}
