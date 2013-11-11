using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler50
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composite = new bool[1000000];
            List<int> primes = new List<int>();
            for (int x = 2; x < 1000000; x++)
            {
                if (!composite[x])
                {
                    primes.Add(x);
                    for (int n = x + x; n < 1000000; n += x)
                        composite[n] = true;
                }
            }
            int value = 0;
            int streak = -1;
            for (int i = 0; i < primes.Count; i++)
            {
                int total = 0;
                for (int x = 0; i + x < primes.Count; x++)
                {
                    total += primes[i + x];
                    if (total >= 1000000)
                        break;
                    if (x < streak)
                        continue;
                    if (primes.Contains(total) && x > streak)
                    {
                        value = total;
                        streak = x;
                    }
                }
            }
            Console.WriteLine(value);
            Console.ReadLine();
        }
    }
}
