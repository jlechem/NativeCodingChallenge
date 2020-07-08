using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NativeCodingChallenge.Utils;

namespace NativeCodingChallenge
{
    class Program
    {
        static List<long> numbers = new List<long>();

        static async Task Main(string[] args)
        {
            await Utilities.PrintHttpGET("https://localhost:44329/api/time");

            numbers.AddRange(Utilities.PrimeList());

            Console.WriteLine(Utilities.SumEven(numbers));

            var events = new ManualResetEvent[2];

            events[0] = new ManualResetEvent(false);
            var threadOne = new Thread(() => Utilities.PrintList("t1", numbers, 500, events[0]));
            threadOne.Start();

            events[1] = new ManualResetEvent(false);
            var threadTwo = new Thread(() => Utilities.PrintList("t2", numbers, 1000, events[1]));
            threadTwo.Start();

            var result = WaitHandle.WaitAll(events);

            Console.ReadKey();

        }

    }
}