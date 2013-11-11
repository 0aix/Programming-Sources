using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler46
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] num = new bool[1000000];
            bool[] composite = new bool[1000000];
            List<int> squares = new List<int>();
            List<int> primes = new List<int>();

            for (int i = 0; i < 1000; i++)
                squares.Add(i * i);
            for (int x = 2; x < 1000000; x++)
            {
                if (!composite[x])
                {
                    primes.Add(x);
                    for (int z = x + x; z < 1000000; z += x)
                    {
                        composite[z] = true;
                    }
                }
            }
            for (int i = 0; i < squares.Count; i++)
            {
                for (int n = 0; n < primes.Count; n++)
                {
                    int x = 2 * squares[i] + primes[n];
                    if (x < 0 || x >= 1000000)
                        break;
                    else
                        num[x] = true;
                }
            }
            for (int i = 3; i < 1000000; i += 2)
            {
                if (!num[i])
                {
                    Console.WriteLine(i);
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
