using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler41
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] composites = new bool[10000000];
            List<int> primes = new List<int>();
            for (int x = 2; x < 10000000; x++)
            {
                if (!composites[x])
                {
                    primes.Add(x);
                    for (int z = x + x; z < 10000000; z += x)
                    {
                        composites[z] = true;
                    }
                }
            }
            int panprime = 0;
            for (int n = 0; n < primes.Count; n++)
            {
                string num = primes[n].ToString();
                bool pan = true;
                for (int i = 0; i < num.Length; i++)
                {
                    if (!num.Contains((i + 1).ToString()))
                    {
                        pan = false;
                        break;
                    }
                }
                if (pan)
                    panprime = primes[n];
            }
            Console.WriteLine(panprime);
            Console.ReadLine();
        }
    }
}
