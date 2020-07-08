using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            numbers.AddRange(Utilities.PrimeList());

            Console.WriteLine(Utilities.SumEven(numbers));

            var events = new ManualResetEvent[2];

            var winnerList = new List<long>();

            events[0] = new ManualResetEvent(false);
            var threadOne = new Thread(() => Utilities.PrintList("t1", numbers, 500, events[0], winnerList));

            events[1] = new ManualResetEvent(false);
            var threadTwo = new Thread(() => Utilities.PrintList("t2", numbers, 500, events[1], winnerList));

            threadOne.Start();
            threadTwo.Start();

            var result = WaitHandle.WaitAll(events);

            Console.ReadKey();

        }

    }
}