using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace LINQ
{
    class Program
    {
        private const int Minimum = 1000000;
        private const int Maximum = 9999999 + 1;

        // Find average number of unique digits in numbers between Minimum and Maximum

        private static double FindWithLoops()
        {
            int sum = 0;

            for (int i = Minimum; i < Maximum; ++i)
            {
                var digits = new int[10];
                var number = i;
                while (number > 0)
                {
                    digits[number % 10] += 1;
                    number /= 10;
                }

                for (int d = 0; d < digits.Length; ++d)
                    if (digits[d] == 1) ++sum;
            }

            return (double)sum / (Maximum - Minimum);
        }

        private static double FindWithLoopsAndString()
        {
            int sum = 0;
            for (int i = Minimum; i < Maximum; ++i)
            {
                var digits = new int[10];
                var s = i.ToString();
                for (var k = 0; k < s.Length; ++k)
                    digits[s[k] - '0'] += 1;
                for (int d = 0; d < digits.Length; ++d)
                    if (digits[d] == 1) // then this is a unique digit
                        ++sum;
            }
            return (double)sum / (Maximum - Minimum);
        }

        private static double FindWithLinq()
        {
            return Enumerable.Range(Minimum, Maximum - Minimum)
                .Select(i => i.ToString()
                                .AsEnumerable()
                                .GroupBy(
                                c => c,
                                c => c,
                                (k, g) => new
                                {
                                    Character = k,
                                    Count = g.Count()
                                })
                                .Count(g => g.Count == 1))
                .Average();
        }

        static void Main(string[] args)
        {
            Measurements.Repetitions = 5;

            //Console.WriteLine(FindWithLinq());
            //Console.WriteLine(FindWithLoops());
            //Console.WriteLine(FindWithLoopsAndString());

            Measurements.Measure("Using LINQ", FindWithLinq);
            Measurements.Measure("Using Loops", FindWithLoops);
            Measurements.Measure("Using Loops and String", FindWithLoopsAndString);
        }
    }
}
