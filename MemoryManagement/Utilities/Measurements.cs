using System;
using System.Diagnostics;

namespace Utilities
{
    public static class Measurements
    {
        private const int Repetitions = 100;

        public static void Measure<T>(string description, Func<T> initialization, Action<T> action)
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
    }
}
