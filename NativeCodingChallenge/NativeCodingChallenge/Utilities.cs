using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativeCodingChallenge.Utils
{
    public class Utilities
    {
        private static object s_lock = new object();

        // Create a function to sum up all the even numbers in a supplied List parameter and return the result.
        public static long SumEven(IEnumerable<long> numbers)
        {
            return numbers.Where(x => x % 2 == 0).Sum();
        }

        // Create a function that will make an http GET request to a given URL and dump out the result in Console. (Be ready to discuss what external tools you can use to validate the behavior, that your program is indeed making the request for ex: fiddler, wireshark...)
        public static async Task PrintHttpGET(string URI)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(URI);

                Console.WriteLine(result);
            }
        }

        // Create a function which will print out the numbers in a List to the console in a loop with a configurable delay (print the number out every 500ms, or every 1000ms ). Make sure the function can be called from a thread.

        // I would normally do this with an async/await but the instructions said Thread specifically so I went with an older school
        // threading metholody
        public static void PrintList(string threadName, IEnumerable<long> numbers, int delay, ManualResetEvent resetEvent, List<long> winnerList)
        {
            foreach (var number in numbers)
            {
                lock(s_lock)
                {
                    if(!winnerList.Contains(number))
                    {
                        winnerList.Add(number);
                        Console.Write($"{number}:{threadName} ");
                    }
                }
                
                Thread.Sleep(delay);
            }

            resetEvent.Set();

        }

        public static List<long> PrimeList()
        {
            var result = new List<long>();

            for (var x = 1; x < 100; x++)
            {
                result.Add(x);
            }

            return result;
        }

    }
}
