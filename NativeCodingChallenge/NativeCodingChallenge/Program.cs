using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativeCodingChallenge
{
    class Program
    {
        static List<long> numbers = new List<long>();

        static Random random = new Random();

        static async Task Main(string[] args)
        {
            await PrintHttpGET("https://localhost:44329/api/time");

            numbers.AddRange(PrimeList());

            var events = new ManualResetEvent[2];

            events[0] = new ManualResetEvent(false);
            var threadOne = new Thread(() => PrintList("t1", numbers, 500, events[0]));
            threadOne.Start();

            events[1] = new ManualResetEvent(false);
            var threadTwo = new Thread(() => PrintList("t2", numbers, 1000, events[1]));
            threadTwo.Start();

            var result = WaitHandle.WaitAll(events);

            Console.ReadKey();

        }

        // Create a function to sum up all the even numbers in a supplied List parameter and return the result.
        static long SumEven(IEnumerable<long> numbers)
        {
            return numbers.Where(x => x % 2 == 0).Sum();
        }

        // Create a function that will make an http GET request to a given URL and dump out the result in Console. (Be ready to discuss what external tools you can use to validate the behavior, that your program is indeed making the request for ex: fiddler, wireshark...)
        static async Task PrintHttpGET(string URI)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(URI);

                Console.WriteLine(result);
            }
        }

        // Create a function which will print out the numbers in a List to the console in a loop with a configurable delay (print the number out every 500ms, or every 1000ms ). Make sure the function can be called from a thread.
        static void PrintList(string threadName, IEnumerable<long> numbers, int delay, ManualResetEvent resetEvent)
        {
            foreach( var number in numbers)
            {
                Console.WriteLine($"{number}:{threadName}");
                Thread.Sleep(delay);
            }
            
            resetEvent.Set();

        }

        static List<long> PrimeList()
        {
            var result = new List<long>();
            
            for( var x = 0; x < 10000; x++)
            {
                result.Add(x);
            }

            return result;
        }


    }
}
