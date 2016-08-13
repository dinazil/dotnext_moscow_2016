using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace Closures
{
    class Program
    {
        private const int Elements = 100000;

        private static int globalSum = 0;

        private static void AddFunction()
        {
            globalSum += Data.Default.Value;
        }

        static void Main(string[] args)
        {
            Measurements.Measure("Task with closure",
                () => new List<Task>(),
                tasks =>
                {
                    globalSum = 0;
                    for (int i = 0; i < Elements; ++i)
                    {
                        var data = new Data { Value = i };
                        tasks.Add(Task.Factory.StartNew(() => { globalSum += data.Value; }));
                    }
                },
                tasks =>
                {
                    Task.WaitAll(tasks.ToArray());
                    GC.Collect();
                }
            );

            Measurements.Measure("Task with passing state",
                () => new List<Task>(),
                tasks =>
                {
                    globalSum = 0;
                    for (int i = 0; i < Elements; ++i)
                    {
                        var data = new Data { Value = i };
                        tasks.Add(Task.Factory.StartNew(d => { globalSum += (d as Data).Value; }, data));
                    }
                },
                tasks =>
                {
                    Task.WaitAll(tasks.ToArray());
                    GC.Collect();
                }
            );

            Measurements.Measure("Task with no closure",
                () => new List<Task>(),
                tasks =>
                {
                    globalSum = 0;
                    for (int i = 0; i < Elements; ++i)
                    {
                        tasks.Add(Task.Factory.StartNew(() => { globalSum += Data.Default.Value; }));
                    }
                },
                tasks =>
                {
                    Task.WaitAll(tasks.ToArray());
                    GC.Collect();
                }
            );

            Measurements.Measure("Task with no closure and method group",
               () => new List<Task>(),
               tasks =>
               {
                   globalSum = 0;
                   for (int i = 0; i < Elements; ++i)
                   {
                       tasks.Add(Task.Factory.StartNew(AddFunction));
                   }
               },
               tasks =>
               {
                   Task.WaitAll(tasks.ToArray());
                   GC.Collect();
               }
           );
        }
    }
}
