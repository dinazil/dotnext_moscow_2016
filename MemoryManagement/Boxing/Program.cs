using System;
using System.Diagnostics;
using System.Linq;

namespace Boxing
{
    class Program
    {
        private const int Repetitions = 100;
        private const int Elements = 10000000;

        private static void Measure<T>(string description, Func<T> initialization, Action<T> action)
        {
            var t = initialization();
            action(t); // warmup

            var sw = Stopwatch.StartNew();

            for (int i = 0; i < Repetitions; ++i)
            {
                action(t);
            }

            sw.Stop();
            Console.WriteLine($"{description} took {sw.ElapsedMilliseconds / Repetitions}ms");
        }

        static void Main(string[] args)
        {
            Measure("Find struct",
                () => Enumerable.Range(0, Elements)
                .Select(i => new Number { Value = i })
                .ToList(),
                list => list.IndexOf(list.Last())
            );

            Measure("Find equatable struct",
                () => Enumerable.Range(0, Elements)
                .Select(i => new ENumber { Value = i })
                .ToList(),
                list => list.IndexOf(list.Last())
            );
        }
    }
}
